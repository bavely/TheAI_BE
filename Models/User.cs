namespace THEAI_BE.Models 
{
    

public class User
{
    public int Id { get; set; }
    public string Email { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string Auth0Id { get; set; } = default!;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

     

    }
}