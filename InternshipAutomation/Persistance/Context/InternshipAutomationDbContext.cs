using InternshipAutomation.Domain.Entities.Base;
using InternshipAutomation.Domain.Entities.Files;
using InternshipAutomation.Domain.Entities.Internship;
using InternshipAutomation.Domain.User;
using IntershipOtomation.Domain.Entities.User;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Mono.TextTemplating;

namespace InternshipAutomation.Persistance.Context;

public class InternshipAutomationDbContext : IdentityDbContext<User,AppRole,Guid>
{
    public InternshipAutomationDbContext(DbContextOptions options) : base(options)
    {
    }

    #region Files

    public DbSet<InternshipApplicationFile> ApplicationFiles { get; set; }
    public DbSet<InternshipDailyReportFile> DailyReportFiles { get; set; }
    public DbSet<InternshipEvaluationFormForCompany> InternshipEvaluationFormForCompanies { get; set; }
    public DbSet<InternshipResultReport> InternshipResultReports { get; set; }
    public DbSet<StateContributionFile> ContributionFiles { get; set; }

    #endregion

    #region Internship

    public DbSet<Company> Companies { get; set; }
    public DbSet<Internship> Internships { get; set; }
    public DbSet<InternshipApplicationFile> InternshipApplicationFiles { get; set; }
    public DbSet<InternshipDailyReportFile> InternshipDailyReportFiles { get; set; }
    public DbSet<StateContributionFile> StateContributionFiles { get; set; }
    public DbSet<InternshipPeriod> InternshipPeriods { get; set; }

    #endregion

    #region Override SaveChanges

    public override int SaveChanges()
    {
        foreach (var entry in ChangeTracker.Entries().ToList())
        {
            if (entry is IEntity)
            {
                CheckEntity(entry);
            }
        }

        return base.SaveChanges();
    }
    
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        foreach (var entry in ChangeTracker.Entries())
        {
            CheckEntity(entry);
        }

        return base.SaveChangesAsync(cancellationToken);
    }

    //TODO gelen entitynin createdDate ve LastModificationDate gibi özellikleri buradan yazılacak
    void CheckEntity(EntityEntry entry)
    {
        if (entry.Entity is not IEntity appEntity)
            return;

        switch (entry.State)
        {
            case EntityState.Modified:
                appEntity.LastModificationDate = DateTime.Now;
                break;
            case EntityState.Added:
                appEntity.CreatedDate = DateTime.Now;
                break;
            default:
                break;
        }
    }

    #endregion

    #region Relations

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<InternshipPeriod>()
            .HasMany(_ => _.Internships)
            .WithOne(_ => _.InternshipPeriod)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<Internship>()
            .HasMany(_ => _.InternshipDailyReportFiles)
            .WithOne(_ => _.Internship)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<Internship>()
            .HasOne(_ => _.InternshipEvaluationFormForCompany)
            .WithOne(_ => _.Internship)
            .HasForeignKey<InternshipEvaluationFormForCompany>(_=>_.InternshipId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<Internship>()
            .HasOne(_ => _.InternshipResultReport)
            .WithOne(_ => _.Internship)
            .HasForeignKey<InternshipResultReport>(_=>_.InternshipId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<Internship>()
            .HasOne(_ => _.StateContributionFile)
            .WithOne(_ => _.Internship)
            .HasForeignKey<StateContributionFile>(_=>_.InternshipId)
            .OnDelete(DeleteBehavior.NoAction);
            
        base.OnModelCreating(modelBuilder);
    }

    #endregion
}