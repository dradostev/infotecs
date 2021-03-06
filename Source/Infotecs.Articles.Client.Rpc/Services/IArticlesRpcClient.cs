﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Infotecs.Articles.Client.Rpc.Dto;

namespace Infotecs.Articles.Client.Rpc.Services
{
    /// <summary>
    /// Client Articles RPC interface.
    /// </summary>
    public interface IArticlesRpcClient
    {
        /// <summary>
        /// Fetch Articles list from server.
        /// </summary>
        /// <returns>Enumerable of Articles.</returns>
        Task<IEnumerable<ArticleDto>> ListArticlesAsync();
        
        /// <summary>
        /// Fetch single Article with Comments by ID from server.
        /// </summary>
        /// <param name="articleId">Article ID.</param>
        /// <returns>Article with Comments.</returns>
        Task<ArticleDto> ShowArticleAsync(long articleId);

        /// <summary>
        /// Save newly created Article.
        /// </summary>
        /// <param name="username">Author name.</param>
        /// <param name="title">Article title.</param>
        /// <param name="content">Article text content.</param>
        /// <returns>New Article.</returns>
        Task<ArticleDto> CreateArticleAsync(string username, string title, string content);

        /// <summary>
        /// Save new Comment for Article.
        /// </summary>
        /// <param name="articleId">Article ID.</param>
        /// <param name="username">Comment author.</param>
        /// <param name="content">Comment text content.</param>
        /// <returns>New Comment.</returns>
        Task<CommentDto> AddCommentAsync(long articleId, string username, string content);

        /// <summary>
        /// Delete Article by ID.
        /// </summary>
        /// <param name="articleId">Article ID.</param>
        /// <returns>Async Task.</returns>
        Task DeleteArticleAsync(long articleId);
    }
}
