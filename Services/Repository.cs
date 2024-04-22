using ContactHub.Data;
using ContactHub.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ContactHub.Services
{
    public class Repository : IRepository
    {
        private readonly MyDBContext _dbContext;

        public Repository(MyDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<bool> AddAsync<T>(T entity) where T : class
        {
            _dbContext.Add(entity);
            return await _dbContext.SaveChangesAsync() > 0 ? true : false;
        }

        public async Task<bool> DeleteAsync<T>(T entity) where T : class
        {
            _dbContext.Remove(entity);
            return await _dbContext.SaveChangesAsync() > 0 ? true: false;
        }

        public IQueryable<T> GetAllUsersAsync<T>() where T : class
        {
            return _dbContext.Set<T>();
        }

        public async Task<T> GetByIdAsync<T>(string Id) where T : class
        {
            return await _dbContext.FindAsync<T>(Id);
        }

        public async Task<bool> UpdateAsync<T>(T entity) where T : class
        {
            _dbContext.Update(entity);
            return await _dbContext.SaveChangesAsync() > 0 ? true : false;
        }
    }
}
