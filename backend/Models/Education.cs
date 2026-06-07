using System;

namespace curriculumApi.Models;

public class Education
{
    public int Id { get; set; }
    public Guid CurriculumModelId { get; set; }
    public CurriculumModel Curriculum { get; set; } = null!;

    public string Institution { get; set; } = string.Empty;
    public string Degree { get; set; } = string.Empty;
    public DateTime? Start { get; set; }
    public DateTime? End { get; set; }
    public string? Description { get; set; }
}
