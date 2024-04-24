using ChatWebAPI.Application.ChatRequests.Commands.CreateChat;
using ChatWebAPI.Application.ChatRequests.Commands.DeleteMessage;
using ChatWebAPI.Application.ChatRequests.Commands.SendMessage;
using ChatWebAPI.Application.ChatRequests.Commands.UpdateLLMParameters;
using ChatWebAPI.Application.ChatRequests.Queries.GetChat;
using ChatWebAPI.Application.ChatRequests.Queries.GetChats;
using ChatWebAPI.Application.ChatRequests.Queries.GetModels;
using ChatWebAPI.Application.DTOs;
using Domain.Core.Entities;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ChatWebAPI.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController(ISender sender) : ControllerBase
    {
        [HttpGet("getmodels")]
        public async Task<List<string>> GetModels(CancellationToken cancellationToken)
        {
            var models = await sender.Send(new GetModelsQuery(), cancellationToken);
            return models;
        }
        [HttpGet("getchatsinfo")]
        [Authorize(Policy = "UserIdMatches")]
        public async Task<List<ChatShortInfo>> GetChatsInfo([FromQuery] int userId, CancellationToken cancellationToken)
        {
            var chats = await sender.Send(new GetChatsInfoQuery(userId), cancellationToken);
            return chats;
        }

        [HttpGet("get")]
        [Authorize(Policy = "UserIdMatches")]
        public async Task<ChatDTO> GetChat([FromQuery] int userId, [FromQuery] int chatId, CancellationToken cancellationToken)
        {
            var chat = await sender.Send(new GetChatQuery(userId, chatId), cancellationToken);
            return chat.Adapt<ChatDTO>() with
            {
                ChatLLMParameters = chat.ChatLLMParameters // idk, somehow it doesn't map and returns null
            };
        }

        [HttpPost("create")]
        [Authorize(Policy = "UserIdMatches")]
        public async Task<IActionResult> CreateChat([FromQuery] int userId, [FromBody] LLMParameters parameters, CancellationToken cancellationToken)
        {
            var chatId = await sender.Send(new CreateChatCommand(userId, parameters), cancellationToken);
            return CreatedAtAction(nameof(GetChat), new { userId, chatId }, null);
        }

        [HttpPost("update-llm-parameters")]
        [Authorize(Policy = "UserIdMatches")]
        public async Task<IActionResult> UpdateLLMParameters([FromQuery] int userId, [FromQuery] int chatId, [FromBody] LLMParameters parameters, CancellationToken cancellationToken)
        {
            await sender.Send(new UpdateLLMParametesCommand(userId, chatId, parameters), cancellationToken);
            return Ok();
        }

        [HttpPost("message/send")]
        [Authorize(Policy = "UserIdMatches")]
        public async Task<IActionResult> SendMessage([FromQuery] int userId, [FromQuery] int chatId, [FromBody] MessageDTO message, CancellationToken cancellationToken)
        {
            await sender.Send(new SendMessageCommand(userId, chatId, message.Adapt<Message>()), cancellationToken);
            return Ok();
        }

        [HttpDelete("message/delete")]
        [Authorize(Policy = "UserIdMatches")]
        public async Task<IActionResult> DeleteMessage([FromQuery] int userId, [FromQuery] int chatId, [FromQuery] int messageIndex, CancellationToken cancellationToken)
        {
            await sender.Send(new DeleteMessageCommand(userId, chatId, messageIndex), cancellationToken);
            return Ok();
        }
    }
}
