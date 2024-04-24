namespace Shared.Application.DTOs
{
    public record AuthenticationResponse(UserDTO User, JwtData JwtData);
}
