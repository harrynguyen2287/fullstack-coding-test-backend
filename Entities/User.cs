namespace Backend.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string? Password { get; set; }
        public string? Name { get; set; }
        public DateTime? DateOfBirth { get; set; }
    }
}