using curriculumApi.Data;
using curriculumApi.Models;
using Microsoft.EntityFrameworkCore;

namespace curriculumApi.Routes;

public static class CurriculumRoute
{
    public static void CurriculumRoutes(this WebApplication app)
    {
        var route = app.MapGroup(prefix:"curriculum");

        route.MapPost(pattern:"", async (CurriculumRequest req, CurriculumContext context) => 
        {
            var curriculum = new CurriculumModel(req.name);
            await context.AddAsync(curriculum);
            await context.SaveChangesAsync();
        } );

        route.MapGet(pattern:"", async (CurriculumContext context) =>
        {
           var curriculums = await context.Curriculum.ToListAsync();
           return Results.Ok(curriculums);
        });

        route.MapPut("{id:guid}", async (Guid id, CurriculumRequest req, CurriculumContext context) =>
        {
            var curriculum = await context.Curriculum.FirstOrDefaultAsync(x => x.Id == id);

            if(curriculum == null)
                return Results.NotFound();

            curriculum.ChangeName(req.name);
            await context.SaveChangesAsync();

            return Results.Ok(curriculum);
        });

        route.MapDelete("{id:guid}", async (Guid id, CurriculumContext context) =>
        {
            var curriculum = await context.Curriculum.FirstOrDefaultAsync(x => x.Id == id); 

            if (curriculum == null){
                return Results.NotFound();
            }

            curriculum.SetInactive();
            await context.SaveChangesAsync();
            return Results.Ok(curriculum);
        });
    }
}