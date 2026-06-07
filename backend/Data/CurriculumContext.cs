using curriculumApi.Models;
using Microsoft.EntityFrameworkCore;

namespace curriculumApi.Data;

public class CurriculumContext : DbContext
{
    public CurriculumContext(DbContextOptions<CurriculumContext> options)
        : base(options)
    {
    }

    public DbSet<CurriculumModel> Curricula { get; set; }
    public DbSet<Education> Educations { get; set; }
    public DbSet<Experience> Experiences { get; set; }
    public DbSet<Skill> Skills { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<CurriculumModel>()
            .OwnsOne(c => c.PersonalInfo);

        modelBuilder.Entity<Education>()
            .HasOne(e => e.Curriculum)
            .WithMany(c => c.Educations)
            .HasForeignKey(e => e.CurriculumModelId);

        modelBuilder.Entity<Experience>()
            .HasOne(e => e.Curriculum)
            .WithMany(c => c.Experiences)
            .HasForeignKey(e => e.CurriculumModelId);

        modelBuilder.Entity<Skill>()
            .HasOne(s => s.Curriculum)
            .WithMany(c => c.Skills)
            .HasForeignKey(s => s.CurriculumModelId);
    }
}