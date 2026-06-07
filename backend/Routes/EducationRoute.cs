using curriculumApi.Data;
using curriculumApi.Models;
using curriculumApi.DTOs;
using Microsoft.EntityFrameworkCore;

namespace curriculumApi.Routes;

public static class EducationRoute
{
    public static void EducationRoutes(this WebApplication app)
    {
        var route = app.MapGroup("/curriculum/{curriculumId:guid}/educations");

        route.MapPost("", async (Guid curriculumId, EducationCreateRequest req, CurriculumContext context) =>
        {
            var c = await context.Curricula.FindAsync(curriculumId);
            if (c == null) return Results.NotFound();
            var edu = new Education { Institution = req.Institution, Degree = req.Degree, Start = req.Start, End = req.End, Description = req.Description };
            c.Educations.Add(edu);
            await context.SaveChangesAsync();
            return Results.Created($"/curriculum/{curriculumId}/educations/{edu.Id}", edu);
        });

        route.MapPut("{educationId:int}", async (Guid curriculumId, int educationId, EducationUpdateRequest req, CurriculumContext context) =>
        {
            var edu = await context.Educations.FirstOrDefaultAsync(e => e.Id == educationId && e.CurriculumModelId == curriculumId);
            if (edu == null) return Results.NotFound();
            edu.Institution = req.Institution;
            edu.Degree = req.Degree;
            edu.Start = req.Start;
            edu.End = req.End;
            edu.Description = req.Description;
            await context.SaveChangesAsync();
            return Results.Ok(edu);
        });

        route.MapDelete("{educationId:int}", async (Guid curriculumId, int educationId, CurriculumContext context) =>
        {
            var edu = await context.Educations.FirstOrDefaultAsync(e => e.Id == educationId && e.CurriculumModelId == curriculumId);
            if (edu == null) return Results.NotFound();
            context.Educations.Remove(edu);
            await context.SaveChangesAsync();
            return Results.NoContent();
        });
    }
}