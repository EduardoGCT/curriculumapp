using curriculumApi.Data;
using curriculumApi.Models;
using curriculumApi.DTOs;
using Microsoft.EntityFrameworkCore;

namespace curriculumApi.Routes;

public static class CurriculumRoute
{
    public static void CurriculumRoutes(this WebApplication app)
    {
        var route = app.MapGroup(prefix: "curriculum");

        // ==================== POST: CRIAR CURRÍCULO ====================
        route.MapPost(pattern: "", async (CurriculumCreateRequest req, CurriculumContext context) =>
        {
            var curriculum = new CurriculumModel(req.Name)
            {
                PersonalInfo = new PersonalInfo
                {
                    FullName = req.PersonalInfo.FullName,
                    Email = req.PersonalInfo.Email,
                    Phone = req.PersonalInfo.Phone,
                    Summary = req.PersonalInfo.Summary
                }
            };

            foreach (var e in req.Educations)
                curriculum.Educations.Add(new Education { Institution = e.Institution, Degree = e.Degree, Start = e.Start, End = e.End, Description = e.Description });

            foreach (var ex in req.Experiences)
                curriculum.Experiences.Add(new Experience { Company = ex.Company, Role = ex.Role, Start = ex.Start, End = ex.End, Description = ex.Description });

            foreach (var s in req.Skills)
                curriculum.Skills.Add(new Skill { Name = s.Name, Level = s.Level });

            await context.Curricula.AddAsync(curriculum);
            await context.SaveChangesAsync();
            return Results.Created($"/curriculum/{curriculum.Id}", curriculum);
        });

        // ==================== GET: LISTAR TODOS (Corrigido) ====================
        route.MapGet(pattern: "", async (CurriculumContext context) =>
        {
            var curriculums = await context.Curricula
                .Include(c => c.PersonalInfo)
                .Include(c => c.Educations)
                .Include(c => c.Experiences)
                .Include(c => c.Skills)
                .ToListAsync();

            return Results.Ok(curriculums);
        });

        // ==================== GET: BUSCAR POR ID (Corrigido) ====================
        route.MapGet("{id:guid}", async (Guid id, CurriculumContext context) =>
        {
            var c = await context.Curricula
                .Include(c => c.PersonalInfo)
                .Include(x => x.Educations)
                .Include(x => x.Experiences)
                .Include(x => x.Skills)
                .FirstOrDefaultAsync(x => x.Id == id);

            return c is null ? Results.NotFound() : Results.Ok(c);
        });

        // ==================== PUT: ATUALIZAR NOME ====================
        route.MapPut("{id:guid}", async (Guid id, CurriculumUpdateRequest req, CurriculumContext context) =>
        {
            var curriculum = await context.Curricula.FirstOrDefaultAsync(x => x.Id == id);
            if (curriculum == null) return Results.NotFound();
            curriculum.ChangeName(req.Name);
            await context.SaveChangesAsync();
            return Results.Ok(curriculum);
        });

        // ==================== PUT: ATUALIZAR INFO PESSOAL ====================
        route.MapPut("{id:guid}/personal-info", async (Guid id, PersonalInfoUpdateRequest req, CurriculumContext context) =>
        {
            var curriculum = await context.Curricula.FirstOrDefaultAsync(x => x.Id == id);
            if (curriculum == null) return Results.NotFound();
            curriculum.PersonalInfo = new PersonalInfo { FullName = req.FullName, Email = req.Email, Phone = req.Phone, Summary = req.Summary };
            await context.SaveChangesAsync();
            return Results.Ok(curriculum.PersonalInfo);
        });

        // ==================== DELETE: DESATIVAR CURRÍCULO ====================
        route.MapDelete("{id:guid}", async (Guid id, CurriculumContext context) =>
        {
            var curriculum = await context.Curricula.FirstOrDefaultAsync(x => x.Id == id);

            if (curriculum == null)
            {
                return Results.NotFound();
            }

            curriculum.SetInactive();
            await context.SaveChangesAsync();
            return Results.Ok(curriculum);
        });
    }
}