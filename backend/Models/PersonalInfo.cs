namespace curriculumApi.Models;

public class PersonalInfo
{

    public Guid Id { get; set; }

    public string FullName { get; set; } = string.Empty;
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public string? Summary { get; set; }
}