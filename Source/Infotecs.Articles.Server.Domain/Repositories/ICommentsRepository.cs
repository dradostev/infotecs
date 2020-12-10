using System.Collections.Generic;
using System.Threading.Tasks;
using Infotecs.Articles.Server.Domain.Entities;

namespace Infotecs.Articles.Server.Domain.Repositories
{
    public interface ICommentsRepository
    {
        Task<IEnumerable<Comment>> ListByArticleAsync(long articleId);
        Task<Comment> CreateAsync(Comment entity);
        Task<bool> DeleteAsync(long commentId);
    }
}