namespace PlaygroundService.Domain.Entities
{
    public class Playground
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public required string UserId { get; set; } = default!;
        public required string Code { get; set; } = default!;
        public required string Language { get; set; }
        public string Output { get; set; } = default!;
        public string Error { get; set; } = default!;
        public bool IsSuccess { get; set; }
        public DateTime ExecutedAt { get; set; } = DateTime.UtcNow;
        public bool IsDeleted { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public required DateTime UpdatedAt { get; set; }
    }
}