using THEAI_BE.Models;
using THEAI_BE.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace THEAI_BE.GraphQL.Queries
{
    [ExtendObjectType(Name = "Query")]
    public class UserQuery
    {

        [UseFiltering]
        [UseSorting]
        [Authorize]
        public IQueryable<User> GetUsers([Service] ApplicationDbContext db) =>
            db.Users;
        [Authorize]
        public async Task<User?> GetUserByAuth0IdAsync(string Auth0Id, [Service] ApplicationDbContext db)
        {
            return await db.Users.FirstOrDefaultAsync(u => u.Auth0Id == Auth0Id);
        }



        [Authorize]
        public async Task<User?> GetUserByIdAsync(int id, [Service] ApplicationDbContext db) =>
         await db.Users.FindAsync(id);

        //create user
        [Authorize]
        public async Task<User> CreateUserAsync([FromBody] UserInput input, [Service] ApplicationDbContext db)
        {
            var user = new User
            {
                Auth0Id = input.Auth0Id,
                Name = input.Name,
                Email = input.Email,
                CreatedAt = DateTime.UtcNow
            };
            db.Users.Add(user);
            await db.SaveChangesAsync();
            return user;
        }

            

   


    }
}