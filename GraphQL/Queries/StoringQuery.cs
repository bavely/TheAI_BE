using THEAI_BE.Models;
using THEAI_BE.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace THEAI_BE.GraphQL.Queries
{
    [ExtendObjectType(Name = "Query")]
    public class StoringQuery
    {
        [UseFiltering]
        [UseSorting]
        [Authorize]
        public IQueryable<Threads> GetThreadsByUserId(int userId, [Service] ApplicationDbContext db) =>
            db.Threads.Where(t => t.UserId == userId);

        [Authorize]
        public async Task<Threads?> GetThreadByIdAsync(int id, [Service] ApplicationDbContext db) =>
            await db.Threads.Include(t => t.Messages)
                             .FirstOrDefaultAsync(t => t.Id == id);

        [Authorize]
        public async Task<Messages?> GetMessageByIdAsync(int id, [Service] ApplicationDbContext db) =>
            await db.Messages.Include(m => m.User).Include(m => m.Threads)
                             .FirstOrDefaultAsync(m => m.Id == id);

        [Authorize]
        public async Task<Messages> CreateMessageAsync([FromBody] Messages input, [Service] ApplicationDbContext db)
        {
            var message = new Messages
            {
                Content = input.Content,
                CreatedAt = DateTime.UtcNow,
                Role = input.Role,
                UserId = input.UserId,
                Threads = input.Threads
            };
            db.Messages.Add(message);
            await db.SaveChangesAsync();
            return message;
        }

        [Authorize]
        public async Task<Threads> CreateThreadAsync([FromBody] Threads input, [Service] ApplicationDbContext db)
        {
            var thread = new Threads
            {
                Title = input.Title,
                CreatedAt = DateTime.UtcNow,
                UserId = input.UserId
            };
            db.Threads.Add(thread);
            await db.SaveChangesAsync();
            return thread;
        }
    }
}