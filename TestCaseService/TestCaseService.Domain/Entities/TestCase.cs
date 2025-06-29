namespace TestCaseService.Domain.Entities
{
    public class TestCase
    {
        public string TestCaseId { get; set; } = Guid.NewGuid().ToString();
        public required string QuestionId { get; set; }
        public required string Input { get; set; }
        public required string ExpectedOutput { get; set; }
        public required bool IsHidden { get; set; }
        public required DateTime CreatedAt { get; set; }
        public required DateTime UpdatedAt { get; set; }
    }
}