namespace InternshipAutomation.Application.Repository.GeneralRepository;

public interface IGeneralRepository
{
    IQueryable<TEntity> Query<TEntity>() where TEntity : class;
    object Add(object entity);
    object Update(object entity);
    object Delete(object entity);
    int SaveChanges();
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}