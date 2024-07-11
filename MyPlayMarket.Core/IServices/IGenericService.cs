namespace MyPlayMarket.Core.IServices
{
    public interface IGenericService<T> where T : class
    {
        public Task<IEnumerable<T>> GetAllAsync();
        public Task<List<T>> GetEntitiesAsync(Func<IQueryable<T>, IQueryable<T>> expression);
        public Task<T> GetEntityAsync(Func<IQueryable<T>, IQueryable<T>> expression);
        public Task<T> GetByIdAsync(int id);
        public Task AddAsync(T entity);
        public Task DeleteAsync(int id);

        public Task UpdateAsync(T entity);
    }
}