
namespace ConstrainService.Domain.Entities
{
    public class Constrain
    {
        public string ConstrainId { get; set; } = Guid.NewGuid().ToString();
        public required string QuestionId { get; set; }
        public required string InputDescription { get; set; }
        public required string OutputDescription { get; set; }
        public required string AdditionalNotes { get; set; }
        public required string TimeLimit { get; set; }
        public required string MemoryLimit { get; set; }
        public bool IsDeleted { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public required DateTime UpdatedAt { get; set; }

    }
}