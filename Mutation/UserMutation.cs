
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using THEAI_BE.Data;
using THEAI_BE.Models;
namespace THEAI_BE.Mutation
{
public record CreateUserInput(string Auth0Id, string Email, string Name);

public class UserMutation
{
    public async Task<User> CreateUser(CreateUserInput input, [Service] ApplicationDbContext context)
    {
        var user = new User
        {
            Email = input.Email,
            Name = input.Name,
            Auth0Id = input.Auth0Id,
            CreatedAt = DateTime.UtcNow
        };

        context.Users.Add(user);
        await context.SaveChangesAsync();

        return user;
    }
}



}