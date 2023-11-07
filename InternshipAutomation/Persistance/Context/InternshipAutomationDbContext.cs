using InternshipAutomation.Domain.Entities.Base;
using InternshipAutomation.Domain.Entities.Files;
using InternshipAutomation.Domain.Entities.Internship;
using InternshipAutomation.Domain.User;
using IntershipOtomation.Domain.Entities.User;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace InternshipAutomation.Persistance.Context;

public class InternshipAutomationDbContext : IdentityDbContext<User,AppRole,Guid>
{
    public InternshipAutomationDbContext(DbContextOptions options) : base(options)
    {
    }

    #region Users
    
    public DbSet<CompanyUser> CompanyUsers { get; set; }
    //public DbSet<AdminUser> AdminUsers { get; set; }
    public DbSet<StudentUser> StudentUsers { get; set; }

    #endregion

    #region Files

    //public DbSet<BackUpFile> BackUpFiles { get; set; }
    //public DbSet<InternshipApplicationFile> InternshipApplicationFiles { get; set; }
    //public DbSet<InternshipBookPageFile> InternshipBookPages { get; set; }
    //public DbSet<SendingFile> SendingFiles { get; set; }

    #endregion

    #region Internship

    public DbSet<Company> Companies { get; set; }
    public DbSet<Internship> Internships { get; set; }

    #endregion

    #region Override SaveChanges

    public override int SaveChanges()
    {
        foreach (var entry in ChangeTracker.Entries().ToList())
        {
            CheckEntity(entry);
        }

        return base.SaveChanges();
    }

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
        modelBuilder.Entity<StudentUser>()
            .HasOne(_ => _.Internship)
            .WithOne(_ => _.StudentUser)
            .HasForeignKey<StudentUser>(_ => _.InternshipId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<CompanyUser>()
            .HasOne(_ => _.Internship)
            .WithOne(_ => _.CompanyUser)
            .HasForeignKey<CompanyUser>(_ => _.InternshipId)
            .OnDelete(DeleteBehavior.NoAction);
        
        base.OnModelCreating(modelBuilder);
    }

    #endregion
}