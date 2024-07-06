namespace MyPlayMarket.Core.IServices
{
    public interface IGenericService<T> where T : class
    {
        public Task AddAsync(T entity);
        public Task DeleteAsync(int id);
        public Task<IEnumerable<T>> GetAllAsync();
        public Task<T> GetByIdAsync(int id);
        public Task UpdateAsync(T entity);
    }
}