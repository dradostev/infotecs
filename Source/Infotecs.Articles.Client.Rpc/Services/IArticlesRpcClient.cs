﻿using System.Collections.Generic;
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
        IEnumerable<ArticleDto> ListArticles();
        
        /// <summary>
        /// Fetch single Article with Comments by ID from server.
        /// </summary>
        /// <param name="articleId">Article ID.</param>
        /// <returns>Article with Comments.</returns>
        ArticleDto ShowArticle(long articleId);

        /// <summary>
        /// Save newly created Article.
        /// </summary>
        /// <param name="username">Author name.</param>
        /// <param name="title">Article title.</param>
        /// <param name="content">Article text content.</param>
        /// <returns>New Article.</returns>
        ArticleDto CreateArticle(string username, string title, string content);
    }
}
