using curriculumApi.Data;
using curriculumApi.Models;
using curriculumApi.DTOs;
using Microsoft.EntityFrameworkCore;

namespace curriculumApi.Routes;

public static class SkillRoute
{
	public static void SkillRoutes(this WebApplication app)
	{
		var route = app.MapGroup("/curriculum/{curriculumId:guid}/skills");

		route.MapPost("", async (Guid curriculumId, SkillCreateRequest req, CurriculumContext context) =>
		{
			var c = await context.Curricula.FindAsync(curriculumId);
			if (c == null) return Results.NotFound();
			var s = new Skill { Name = req.Name, Level = req.Level };
			c.Skills.Add(s);
			await context.SaveChangesAsync();
			return Results.Created($"/curriculum/{curriculumId}/skills/{s.Id}", s);
		});

		route.MapPut("{skillId:int}", async (Guid curriculumId, int skillId, SkillUpdateRequest req, CurriculumContext context) =>
		{
			var s = await context.Skills.FirstOrDefaultAsync(x => x.Id == skillId && x.CurriculumModelId == curriculumId);
			if (s == null) return Results.NotFound();
			s.Name = req.Name;
			s.Level = req.Level;
			await context.SaveChangesAsync();
			return Results.Ok(s);
		});

		route.MapDelete("{skillId:int}", async (Guid curriculumId, int skillId, CurriculumContext context) =>
		{
			var s = await context.Skills.FirstOrDefaultAsync(x => x.Id == skillId && x.CurriculumModelId == curriculumId);
			if (s == null) return Results.NotFound();
			context.Skills.Remove(s);
			await context.SaveChangesAsync();
			return Results.NoContent();
		});
	}
}
