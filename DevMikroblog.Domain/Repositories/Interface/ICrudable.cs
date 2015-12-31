namespace DevMikroblog.Domain.Repositories.Interface
{
    public interface ICrudable<TEntity, TId>
    {
        TEntity Create(TEntity entity);
        TEntity Read(TId id);
        bool Update(TEntity entity);
        bool Delete(TId id);
    }
}
