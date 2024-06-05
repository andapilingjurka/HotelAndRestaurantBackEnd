
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;

    namespace HotelAndRestaurant.Controllers
    {
        [Route("api/[controller]")]
        [ApiController]
        public class ChatController : ControllerBase
        {
            private static readonly Dictionary<string, string> faqs = new Dictionary<string, string>
        {
            { "What is your name?", "My name is ChatBot." },
            { "How are you?", "I'm just a bot, but I'm functioning properly!" },
            { "What is Bambus Group?", "Bambus Group is a corporation founded in 2014, specializing in integrating the digital world into all business areas." },
            { "Where is Bambus Group located?", "Bambus Group has offices in Kosovo, Albania, and Germany." }
        };

            [HttpPost]
            public IActionResult Post([FromBody] QuestionModel questionModel)
            {
                var question = questionModel.Question.ToLower();
                if (faqs.TryGetValue(question, out var answer))
                {
                    return Ok(new { answer });
                }
                return Ok(new { answer = "I don't know the answer to that question." });
            }
        }

        public class QuestionModel
        {
            public string Question { get; set; }
        }
    }


