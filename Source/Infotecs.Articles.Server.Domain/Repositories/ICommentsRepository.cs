namespace Infotecs.Articles.Server.Domain.Repositories
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Infotecs.Articles.Server.Domain.Entities;

    public interface ICommentsRepository
    {
        /// <summary>
        /// Asynchronously lists all Comments attached to Article with given ID.
        /// </summary>
        /// <param name="articleId">ID of Article entity.</param>
        /// <returns>Collection of Comments.</returns>
        Task<IEnumerable<Comment>> ListByArticleAsync(long articleId);

        /// <summary>
        /// Asynchronously creates new Comment.
        /// </summary>
        /// <param name="entity">New Comment entity.</param>
        /// <returns>Returns newly created and saved <see cref="Comment"/> with ID.</returns>
        Task<Comment> CreateAsync(Comment entity);

        /// <summary>
        ///
        /// </summary>
        /// <param name="commentId">ID of Comment entity.</param>
        /// <returns>Boolean result if operation was successful.</returns>
        Task<bool> DeleteAsync(long commentId);
    }
}