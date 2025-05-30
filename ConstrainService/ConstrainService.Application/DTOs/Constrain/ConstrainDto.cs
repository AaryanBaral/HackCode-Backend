
namespace ConstrainService.Application.DTOs.Constrain
{
    public class ReadConstrainDto
    {

        public required string ConstrainId { get; set; }
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

    public class AddConstrainDto
    {
        public required string QuestionId { get; set; }
        public required string InputDescription { get; set; }
        public required string OutputDescription { get; set; }
        public required string AdditionalNotes { get; set; }
        public required string TimeLimit { get; set; }
        public required string MemoryLimit { get; set; }
    }
}