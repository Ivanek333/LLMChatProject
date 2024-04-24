using AuthenticationWebAPI.Application.Abstractions;
using Domain.Core.Entities;
using Shared.Application.DTOs;
using Microsoft.EntityFrameworkCore;

namespace AuthenticationWebAPI.Persistence
{
    public class UserRepository(UserDbContext userDb) : IUserRepository
    {
        public Task<bool> ValidateUser(AuthenticationRequest request, CancellationToken cancellation = default)
            => userDb.Users.AnyAsync(u => 
                u.Name == request.UserName && 
                u.Password == request.Password, cancellation);

        public Task<User?> GetUser(AuthenticationRequest request, CancellationToken cancellation = default)
            => userDb.Users.FirstOrDefaultAsync(u => 
                u.Name == request.UserName && 
                u.Password == request.Password, cancellation);

        public Task<User?> GetUserById(int userId, CancellationToken cancellation = default)
            => userDb.Users.FirstOrDefaultAsync(u => u.Id == userId, cancellation);

        public void AddUser(User user)
            => userDb.Users.Add(user);

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
            => userDb.SaveChangesAsync(cancellationToken);
    }
}
