using Shared.Application.Exceptions;

namespace AuthenticationWebAPI.Application.Exceptions
{
    public class UserNotFoundException : NotFoundException
    {
        public UserNotFoundException(int userId)
            : base($"User with the id {userId} was not found.") { }
        public UserNotFoundException(string userName)
            : base($"User with the name {userName} was not found.") { }
    }
}
