using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infotecs.Articles.Server.Domain.Repositories
{
    public interface IRepository<T>
    {
        Task<T> Show(long entityId);
        Task<IEnumerable<T>> List();
        Task<T> Create(T entity);
        Task Delete(long entityId);
    }
}