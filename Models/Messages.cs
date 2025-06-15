namespace THEAI_BE.Models
{
    public class Messages
    {
        public int Id { get; set; }
        public string Content { get; set; } = default!;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public int UserId { get; set; }
        public User User { get; set; } = default!;
        public string Role { get; set; } = "user"; // Default role is 'user', can be 'user' or 'assistant'
        // public int ThreadId { get; set; }
        public Threads Threads { get; set; } = default!;
    }
}