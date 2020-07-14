using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Bizportal.Api
{
    public class BizportalManager<T> : IBizportalManager<T> where T : BaseEntity
    {
        protected readonly BizportalDbContext _dbContext;
        public BizportalManager(BizportalDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<T> Add(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();

            return entity;
        }

        public async Task Delete(Guid id)
        {
            var entity = this.GetById(id);
            _dbContext.Remove(entity);

            await _dbContext.SaveChangesAsync();
        }

        public IQueryable<T> GetAll(Func<IQueryable<T>, IQueryable<T>> queryModifier = null)
        {
            var query = _dbContext.Set<T>().AsQueryable();

            query = queryModifier != null ? queryModifier(query) : query;

            return query.AsNoTracking();
        }

        public async Task<T> GetById(Guid id)
        {
            var result = await _dbContext.Set<T>().Where(x => x.Id.Equals(id)).SingleOrDefaultAsync();
            return result;
        }
    }
}
