using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infotecs.Articles.Server.Domain.Repositories
{
    public interface IRepository<T>
    {
        Task<T> ShowAsync(long entityId);
        Task<IEnumerable<T>> ListAsync();
        Task<T> CreateAsync(T entity);
        Task DeleteAsync(long entityId);
    }
}