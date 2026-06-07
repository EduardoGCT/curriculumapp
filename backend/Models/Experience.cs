namespace curriculumApi.Models;

public class Experience
{
    public int Id { get; set; }
    public Guid CurriculumModelId { get; set; }
    public CurriculumModel Curriculum { get; set; } = null!;

    public string Company { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
    public DateTime? Start { get; set; }
    public DateTime? End { get; set; }
    public string? Description { get; set; }
}