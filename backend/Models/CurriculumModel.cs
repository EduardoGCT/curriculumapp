namespace curriculumApi.Models;

public class CurriculumModel (string name)
{
    

    public Guid Id { get; init; } = Guid.NewGuid();
    public string Name { get; private set; } = name;

    public void ChangeName(string name)
    {
        Name = name;
    }

    public void SetInactive()
    {
        Name = "desativado";
    }
}