using Microsoft.AspNetCore.Mvc;

namespace ChatbotPlatform.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestController : ControllerBase
    {
        private readonly ILogger<TestController> _logger;

        public TestController(ILogger<TestController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Get()
        {
            _logger.LogInformation("Test endpoint called");
            return Ok(new
            {
                message = "CalimaticChatBot API is running!",
                timestamp = DateTime.UtcNow,
                environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")
            });
        }

        [HttpGet("version")]
        public IActionResult GetVersion()
        {
            return Ok(new
            {
                version = "1.0.0",
                dotnetVersion = Environment.Version.ToString(),
                apiName = "ChatbotPlatform.API"
            });
        }
    }
}
