using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ChatbotPlatform.Core.Data;

namespace ChatbotPlatform.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TestController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("clients")]
        public async Task<IActionResult> GetClients()
        {
            var clients = await _context.Clients.ToListAsync();
            return Ok(new { 
                count = clients.Count, 
                clients = clients 
            });
        }

        [HttpGet("chatbots")]
        public async Task<IActionResult> GetChatbots()
        {
            var chatbots = await _context.Chatbots
                .Include(c => c.Client)
                .ToListAsync();
            return Ok(new { 
                count = chatbots.Count, 
                chatbots = chatbots 
            });
        }

        [HttpGet("users")]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _context.Users
                .Include(u => u.Client)
                .Select(u => new {
                    u.Id,
                    u.Email,
                    u.FirstName,
                    u.LastName,
                    ClientName = u.Client.CompanyName
                })
                .ToListAsync();
            return Ok(new { 
                count = users.Count, 
                users = users 
            });
        }

        [HttpGet("database-status")]
        public async Task<IActionResult> GetDatabaseStatus()
        {
            var clientCount = await _context.Clients.CountAsync();
            var userCount = await _context.Users.CountAsync();
            var chatbotCount = await _context.Chatbots.CountAsync();
            var documentCount = await _context.Documents.CountAsync();

            return Ok(new
            {
                status = "Connected",
                tables = new
                {
                    clients = clientCount,
                    users = userCount,
                    chatbots = chatbotCount,
                    documents = documentCount
                },
                message = "Phase 02 Complete! Database is working perfectly."
            });
        }
    }
}