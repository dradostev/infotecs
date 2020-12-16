using System.Collections.Generic;
using System.Threading.Tasks;
using Infotecs.Articles.Server.Domain.Entities;

namespace Infotecs.Articles.Server.Domain.Repositories
{
    /// <summary>
    /// Articles database repository interface.
    /// </summary>
    public interface IArticlesRepository
    {
        /// <summary>
        /// Asynchronously find article by ID.
        /// </summary>
        /// <param name="articleId">ID of Article entity.</param>
        /// <returns>Returns found <see cref="Article"/> or null.</returns>
        Task<Article> ShowAsync(long articleId);

        /// <summary>
        /// Asynchronously list all existing articles.
        /// </summary>
        /// <returns>Collection of Articles.</returns>
        Task<IEnumerable<Article>> ListAsync();

        /// <summary>
        /// Asynchronously persists new Article.
        /// </summary>
        /// <param name="entity">New Article entity.</param>
        /// <returns>Returns newly created and saved <see cref="Article"/> with ID.</returns>
        Task<Article> CreateAsync(Article entity);

        /// <summary>
        /// Asynchronously removes Article from database by ID.
        /// </summary>
        /// <param name="articleId">ID of Article entity.</param>
        /// <returns>Boolean result if operation was successful.</returns>
        Task<bool> DeleteAsync(long articleId);
    }
}