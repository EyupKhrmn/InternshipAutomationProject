using Microsoft.EntityFrameworkCore;

namespace InternshipAutomation.Application.Repository.GeneralRepository;

public class GeneralRepository<TContext> : IGeneralRepository where TContext : DbContext
{
    protected TContext Context { get; set; }
    public GeneralRepository(TContext context)
    {
        Context = context;
    }

    public IQueryable<TEntity> Query<TEntity>()
        where TEntity : class
    {
        return Context.Set<TEntity>();
    }

    public object Add(object entity)
    {
        return Context.Add(entity).Entity;
    }

    public object Update(object entity)
    {
        Context.Update(entity);
        return entity;
    }

    public object Delete(object entity)
    {
        Context.Remove(entity);
        return entity;
    }

    public int SaveChanges()
    {
        return Context.SaveChanges();
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await Context.SaveChangesAsync(cancellationToken);
    }
}