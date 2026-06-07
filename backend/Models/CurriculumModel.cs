namespace curriculumApi.Models;

public class CurriculumModel
{
    public Guid Id { get; set; } = Guid.NewGuid();
    
    // Mudamos de 'private set' para 'set' (ou 'init') para o JSON conseguir preencher
    public string Name { get; set; } = string.Empty;

    public PersonalInfo PersonalInfo { get; set; } = new();
    public List<Education> Educations { get; set; } = new();
    public List<Experience> Experiences { get; set; } = new();
    public List<Skill> Skills { get; set; } = new();

    // 1. CONSTRUTOR VAZIO: Obrigatório para o EF Core e para o Serializador JSON
    public CurriculumModel()
    {
    }

    // 2. Seu construtor atual pode continuar aqui sem problemas
    public CurriculumModel(string name)
    {
        Name = name;
    }

    public void ChangeName(string name) => Name = name;
    public void SetInactive() => Name = "desativado";
}