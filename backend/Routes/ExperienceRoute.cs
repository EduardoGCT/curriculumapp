using curriculumApi.Data;
using curriculumApi.Models;
using curriculumApi.DTOs;
using Microsoft.EntityFrameworkCore;

namespace curriculumApi.Routes;

public static class ExperienceRoute
{
	public static void ExperienceRoutes(this WebApplication app)
	{
		var route = app.MapGroup("/curriculum/{curriculumId:guid}/experiences");

		route.MapPost("", async (Guid curriculumId, ExperienceCreateRequest req, CurriculumContext context) =>
		{
			var c = await context.Curricula.FindAsync(curriculumId);
			if (c == null) return Results.NotFound();
			var item = new Experience { Company = req.Company, Role = req.Role, Start = req.Start, End = req.End, Description = req.Description };
			c.Experiences.Add(item);
			await context.SaveChangesAsync();
			return Results.Created($"/curriculum/{curriculumId}/experiences/{item.Id}", item);
		});

		route.MapPut("{experienceId:int}", async (Guid curriculumId, int experienceId, ExperienceUpdateRequest req, CurriculumContext context) =>
		{
			var item = await context.Experiences.FirstOrDefaultAsync(e => e.Id == experienceId && e.CurriculumModelId == curriculumId);
			if (item == null) return Results.NotFound();
			item.Company = req.Company;
			item.Role = req.Role;
			item.Start = req.Start;
			item.End = req.End;
			item.Description = req.Description;
			await context.SaveChangesAsync();
			return Results.Ok(item);
		});

		route.MapDelete("{experienceId:int}", async (Guid curriculumId, int experienceId, CurriculumContext context) =>
		{
			var item = await context.Experiences.FirstOrDefaultAsync(e => e.Id == experienceId && e.CurriculumModelId == curriculumId);
			if (item == null) return Results.NotFound();
			context.Experiences.Remove(item);
			await context.SaveChangesAsync();
			return Results.NoContent();
		});
	}
}
