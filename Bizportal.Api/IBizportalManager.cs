using System;
using System.Linq;
using System.Threading.Tasks;

namespace Bizportal.Api
{
    public interface IBizportalManager<T> where T : BaseEntity
    {
        IQueryable<T> GetAll(Func<IQueryable<T>, IQueryable<T>> queryModifier = null);
        Task<T> GetById(Guid id);
        Task<T> Add(T entity);
        Task Delete(Guid id);
    }
}
