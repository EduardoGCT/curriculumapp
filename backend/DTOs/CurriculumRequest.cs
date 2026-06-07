using System;
using System.Collections.Generic;

namespace curriculumApi.DTOs;

public record CurriculumCreateRequest(
    string Name,
    PersonalInfoCreateRequest PersonalInfo,
    List<EducationCreateRequest> Educations,
    List<ExperienceCreateRequest> Experiences,
    List<SkillCreateRequest> Skills
);

public record CurriculumUpdateRequest(string Name);

public record PersonalInfoCreateRequest(string FullName, string? Email, string? Phone, string? Summary);
public record PersonalInfoUpdateRequest(string FullName, string? Email, string? Phone, string? Summary);

public record EducationCreateRequest(string Institution, string Degree, DateTime? Start, DateTime? End, string? Description);
public record EducationUpdateRequest(string Institution, string Degree, DateTime? Start, DateTime? End, string? Description);

public record ExperienceCreateRequest(string Company, string Role, DateTime? Start, DateTime? End, string? Description);
public record ExperienceUpdateRequest(string Company, string Role, DateTime? Start, DateTime? End, string? Description);

public record SkillCreateRequest(string Name, string? Level);
public record SkillUpdateRequest(string Name, string? Level);