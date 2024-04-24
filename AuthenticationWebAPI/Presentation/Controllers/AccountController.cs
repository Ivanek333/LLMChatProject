using AuthenticationWebAPI.Application.UserRequests.Commands.Authenticate;
using JwtAuthenticationManager;
using Shared.Application.DTOs;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AuthenticationWebAPI.Application.UserRequests.Commands.Register;
using AuthenticationWebAPI.Application.UserRequests.Commands.UpdateLLMParameters;
using Domain.Core.Entities;
using Microsoft.AspNetCore.Authorization;

namespace AuthenticationWebAPI.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController(ISender sender) : ControllerBase
    {
        [HttpPost("authenticate")]
        public async Task<AuthenticationResponse> Authenticate([FromBody] AuthenticationRequest request)
        {
            var authenticationResponse = await sender.Send(new AuthenticateCommand(request));
            return authenticationResponse;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] AuthenticationRequest request)
        {
            var user = await sender.Send(new RegisterCommand(request));
            return Ok();
        }

        [HttpPost("update-llm-parameters")]
        [Authorize(Policy = "UserIdMatches")]
        public async Task<IActionResult> UpdateLLMParameters([FromQuery] int userId, [FromBody] LLMParameters parameters)
        {
            await sender.Send(new UpdateLLMParametesCommand(userId, parameters));
            return Ok();
        }
    }
}
