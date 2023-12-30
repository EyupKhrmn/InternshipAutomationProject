using InternshipAutomation.Persistance.Context;
using Microsoft.EntityFrameworkCore;

namespace InternshipAutomation.Application.Repository.GeneralRepository;

public class GeneralRepository<TContext> : IGeneralRepository where TContext : DbContext
{
    private readonly InternshipAutomationDbContext _internshipAutomationDbContext;
    public GeneralRepository(TContext context, InternshipAutomationDbContext internshipAutomationDbContext)
    {
        _internshipAutomationDbContext = internshipAutomationDbContext;
    }

    public IQueryable<TEntity> Query<TEntity>()
        where TEntity : class
    {
        return _internshipAutomationDbContext.Set<TEntity>();
    }

    public object Add(object entity)
    {
        return _internshipAutomationDbContext.Add(entity).Entity;
    }

    public object Update(object entity)
    {
        _internshipAutomationDbContext.Update(entity);
        return entity;
    }

    public object Delete(object entity)
    {
        _internshipAutomationDbContext.Remove(entity);
        return entity;
    }

    public int SaveChanges()
    {
        return _internshipAutomationDbContext.SaveChanges();
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await _internshipAutomationDbContext.SaveChangesAsync(cancellationToken);
    }
}