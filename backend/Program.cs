using curriculumApi.Data;
using curriculumApi.Routes;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var dbPath = Path.Combine(builder.Environment.ContentRootPath, "curriculum.sqlite");
builder.Services.AddDbContext<CurriculumContext>(options =>
    options.UseSqlite($"Data Source={dbPath}"));
builder.Services.AddAuthorization(); 
builder.Services.AddControllers();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});
builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options =>
{
    options.SerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Curriculum API v1");
    });
}

app.CurriculumRoutes();
app.EducationRoutes();
app.ExperienceRoutes();
app.SkillRoutes();
app.UseCors("AllowAll");
app.UseAuthorization();

app.MapControllers();
app.UseHttpsRedirection();
app.Run();

