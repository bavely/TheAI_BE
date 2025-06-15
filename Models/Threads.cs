namespace THEAI_BE.Models
{
   public class Threads
    {
        public int Id { get; set; }
        public string Title { get; set; } = default!;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public int UserId { get; set; }
        public User User { get; set; } = default!;

        // Navigation property for related messages
        public ICollection<Messages> Messages { get; set; } = new List<Messages>();
    }
}