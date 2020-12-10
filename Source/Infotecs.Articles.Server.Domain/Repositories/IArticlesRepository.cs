using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Infotecs.Articles.Server.Domain.Entities;

namespace Infotecs.Articles.Server.Domain.Repositories
{
    public interface IArticlesRepository
    {
        Task<Article> ShowAsync(long articleId);
        Task<IEnumerable<Article>> ListAsync();
        Task<Article> CreateAsync(Article entity);
        Task<bool> DeleteAsync(long articleId);
    }
}