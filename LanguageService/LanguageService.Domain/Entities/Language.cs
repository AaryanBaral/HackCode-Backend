namespace LanguageService.Domain.Entities
{
    public class Language
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public required string Name { get; set; }
        public required string DockerImage { get; set; }
        public required string FileExtension { get; set; }
        public string? CompileCommand { get; set; }
        public required string ExecuteCommand { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public required DateTime ModifiedAt { get; set; }

    }
}