using curriculumApi.Models;
using Microsoft.EntityFrameworkCore;

namespace curriculumApi.Data;

public class CurriculumContext : DbContext
{
    public CurriculumContext(DbContextOptions<CurriculumContext> options)
        : base(options)
    {
    }

    public DbSet<CurriculumModel> Curriculum { get; set; }
}