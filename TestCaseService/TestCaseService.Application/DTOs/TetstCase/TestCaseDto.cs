namespace TestCaseService.Application.DTOs.TetstCase
{
    public class CreateTestCaseDto
    {
        public required string QuestionId { get; set; }
        public required string Input { get; set; }
        public required string ExpectedOutput { get; set; }
        public required bool IsHidden { get; set; }
    }
    public class ReadTestCaseDto
    {
        public required string TestCaseId { get; set; }
        public required string QuestionId { get; set; }
        public required string Input { get; set; }
        public required string ExpectedOutput { get; set; }
        public required bool IsHidden { get; set; }
        public required DateTime CreatedAt { get; set; }
        public required DateTime UpdatedAt { get; set; }
    }

    public class UpdateTestCaseDto
    {
        public required string Input { get; set; }
        public required string ExpectedOutput { get; set; }
        public required bool IsHidden { get; set; }
    }
}