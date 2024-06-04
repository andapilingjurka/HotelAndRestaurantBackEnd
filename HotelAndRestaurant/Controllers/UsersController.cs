using HotelAndRestaurant.Data;
using HotelAndRestaurant.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace HotelAndRestaurant.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ApplicationDbContext _db;

        public UsersController(ApplicationDbContext db)
        {
            _db = db;
        }

        //ListAll
        [HttpGet]
        [Route("GetAllList")]
        public async Task<IActionResult> GetAsync()
        {
            var users = await _db.Users.Include(q => q.Role).ToListAsync();
            return Ok(users);
        }

        //GetById
        [HttpGet]
        [Route("GetUserById")]
        public async Task<IActionResult> GetUserById(int Id)
        {
            var user = await _db.Users.Include(q => q.Role).FirstOrDefaultAsync(q => q.Id == Id);
            return Ok(user);
        }

        //Add
        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> PostAsync(User user)
        {
            // Kontrollo nëse shteti ekziston
            var existingState = await _db.Roles.FindAsync(user.RoleId);
            if (existingState == null)
            {
                return NotFound($"Roli me ID {user.RoleId} nuk ekziston.");
            }

            user.Role = existingState;

            _db.Users.Add(user);
            await _db.SaveChangesAsync();
            return Created($"/GetUserById/{user.Id}", user);
        }

        //Update
        [HttpPut]
        [Route("Update/{id}")]
        public async Task<IActionResult> PutAsync(User user)
        {
            // Kontrollo nëse shteti ekziston shteti-role   qyteti-user
            var existingState = await _db.Roles.FindAsync(user.RoleId);
            if (existingState == null)
            {
                return NotFound($"Roli me ID {user.RoleId} nuk ekziston.");
            }

            user.Role = existingState;

            _db.Users.Update(user);
            await _db.SaveChangesAsync();
            return NoContent();
        }

        //Delete
        [Route("Delete")]
        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(int Id)
        {
            var userIdDelete = await _db.Users.Include(q => q.Role).FirstOrDefaultAsync(q => q.Id == Id);

            if (userIdDelete == null)
            {
                return NotFound();
            }

            _db.Users.Remove(userIdDelete);
            await _db.SaveChangesAsync();
            return NoContent();
        }



        [HttpPost("Register")]
        //[Route("Register")]
        public async Task<IActionResult> Register(User objUser)
        {
            var dbuser = _db.Users.Where(u => u.Email == objUser.Email).FirstOrDefault();
            if (dbuser != null)
            {
                return BadRequest("Emaili ekziston!");
            }

            var existingState = await _db.Roles.FindAsync(objUser.RoleId);
            if (existingState == null)
            {
                return NotFound($"Roli me ID {objUser.RoleId} nuk ekziston.");
            }

            objUser.Role = existingState;


            // Generate salt and hash password using bcrypt algorithm
            string salt = BCrypt.Net.BCrypt.GenerateSalt();
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(objUser.Password, salt);

            objUser.Password = hashedPassword; // Save hashed password in the database
            _db.Users.Add(objUser);
            await _db.SaveChangesAsync();
            return Ok("Regjistrimi u shtua me sukses.");

        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(Login user)
        {
            var userInDb = await _db.Users.SingleOrDefaultAsync(u => u.Email == user.Email);
            if (userInDb == null || !BCrypt.Net.BCrypt.Verify(user.Password, userInDb.Password))
            {
                return BadRequest("Emaili ose Fjalekalimi gabim.");
            }

            // Generate tokens
            var tokens = GenerateTokens(userInDb);
            userInDb.RefreshToken = tokens.RefreshToken;
            userInDb.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(1); // Refresh token is valid for 1 days
            await _db.SaveChangesAsync();

            return Ok(new { AccessToken = tokens.AccessToken, RefreshToken = tokens.RefreshToken });
        }

        private (string AccessToken, string RefreshToken) GenerateTokens(User user)
        {
            var userInDb = _db.Users.SingleOrDefault(u => u.Email == user.Email);
            var existingState = _db.Roles.Find(userInDb.RoleId);

            var claims = new[]
            {
        new Claim(ClaimTypes.Name, user.Email),
        new Claim(ClaimTypes.Role, existingState.Name),
        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
    };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("YourSecretKeyWithAtLeast16Characters"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var accessToken = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(60), // Shorter expiry for access token
                signingCredentials: creds);

            var refreshToken = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));

            return (new JwtSecurityTokenHandler().WriteToken(accessToken), refreshToken);
        }

        [HttpPost]
        [Route("RefreshToken")]
        public async Task<IActionResult> RefreshToken(string refreshToken)
        {
            var userInDb = await _db.Users.FirstOrDefaultAsync(u => u.RefreshToken == refreshToken && u.RefreshTokenExpiryTime > DateTime.UtcNow);
            if (userInDb == null)
            {
                return BadRequest("Invalid or expired refresh token.");
            }

            // Generate new tokens
            var newTokens = GenerateTokens(userInDb);
            userInDb.RefreshToken = newTokens.RefreshToken; // Update refresh token
           // userInDb.RefreshTokenExpiryTime = DateTime.UtcNow.AddMinutes(2);
            await _db.SaveChangesAsync();

            return Ok(new { AccessToken = newTokens.AccessToken, RefreshToken = newTokens.RefreshToken });
        }

        //Filtering
        [HttpGet]
        [Route("GetAllFiltering")]
        public async Task<IActionResult> GetFiltering([FromQuery] string searchQuery, [FromQuery] string sortField, [FromQuery] bool isAscending)
        {
            var query = _db.Users.AsQueryable(); 
            int searchQueryInt;

            // Kërkimi
            if (!string.IsNullOrEmpty(searchQuery))
            {
                query = query.Where(p =>
                    p.FirstName.Contains(searchQuery) ||
                    p.LastName.Contains(searchQuery) ||
                    p.Email.Contains(searchQuery) ||
                    p.Password.Contains(searchQuery) ||
                    (int.TryParse(searchQuery, out searchQueryInt) && p.Id == searchQueryInt) ||
                    (int.TryParse(searchQuery, out searchQueryInt) && p.RoleId == searchQueryInt)
                );
            }

            // Renditja
            switch (sortField?.ToLower())
            {
                case "id":
                    query = isAscending ? query.OrderBy(p => p.Id) : query.OrderByDescending(p => p.Id);
                    break;

                case "firstname":
                    query = isAscending ? query.OrderBy(p => p.FirstName) : query.OrderByDescending(p => p.FirstName);
                    break;

                case "lastname":
                    query = isAscending ? query.OrderBy(p => p.LastName) : query.OrderByDescending(p => p.LastName);
                    break;

                case "email":
                    query = isAscending ? query.OrderBy(p => p.Email) : query.OrderByDescending(p => p.Email);
                    break;

                case "password":
                    query = isAscending ? query.OrderBy(p => p.Password) : query.OrderByDescending(p => p.Password);
                    break;

                case "roleid":
                    query = isAscending ? query.OrderBy(p => p.RoleId) : query.OrderByDescending(p => p.RoleId);
                    break;

                default:
                    query = query.OrderBy(p => p.Id);
                    break;
            }

            var result = await query.ToListAsync();
            return Ok(result);
        }

    }
}
