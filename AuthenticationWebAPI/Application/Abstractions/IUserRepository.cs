using Domain.Core.Entities;
using Shared.Application.DTOs;

namespace AuthenticationWebAPI.Application.Abstractions
{
    public interface IUserRepository
    {
        public Task<bool> ValidateUser(AuthenticationRequest request, CancellationToken cancellation = default);
        public Task<User?> GetUser(AuthenticationRequest request, CancellationToken cancellation = default);
        public Task<User?> GetUserById(int userId, CancellationToken cancellation = default);
        public void AddUser(User user);
        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }

}