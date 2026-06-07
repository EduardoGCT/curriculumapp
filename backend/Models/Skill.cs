namespace curriculumApi.Models;

public class Skill
{
    public int Id { get; set; }
    public Guid CurriculumModelId { get; set; }
    public CurriculumModel Curriculum { get; set; } = null!;

    public string Name { get; set; } = string.Empty;
    public string? Level { get; set; }
}