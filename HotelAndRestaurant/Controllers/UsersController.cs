using HotelAndRestaurant.Data;
using HotelAndRestaurant.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
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

            var existingState = await _db.Roles.FindAsync(userInDb.RoleId);

            var role = existingState.Name;
            var id = userInDb.Id.ToString();
            // Create claims for the token
            var claims = new[]
            {
        new Claim(ClaimTypes.Name, userInDb.Email),
        new Claim(ClaimTypes.Role, role),
        new Claim(ClaimTypes.NameIdentifier, id)
    };

            // Generate symmetric security key
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("YourSecretKeyWithAtLeast16Characters"));

            // Generate signing credentials using the security key
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            // Create token descriptor
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(1), // Set token expiration
                SigningCredentials = credentials
            };

            // Create a token handler
            var tokenHandler = new JwtSecurityTokenHandler();

            // Generate token based on the token descriptor
            var token = tokenHandler.CreateToken(tokenDescriptor);


            // Convert token to a string
            var tokenString = tokenHandler.WriteToken(token);

            return Ok(new { Token = tokenString });
        }

    }
}
