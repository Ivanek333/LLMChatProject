using ChattingService.Application.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ChattingService.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController(ILogger<ChatController> logger, ISender sender) : ControllerBase
    {
        [HttpGet]
        public async List<ChatShortInfo> GetChats([FromQuery] int userId, string token)
        {
            // authorization check??
        }
    }
}
