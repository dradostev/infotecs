using System.Collections.Generic;
using Infotecs.Articles.Client.Rpc.Dto;

namespace Infotecs.Articles.Client.Rpc.Services
{
    /// <summary>
    /// Client Articles RPC interface.
    /// </summary>
    public interface IArticlesRpcClient
    {
        IEnumerable<ArticleDto> ListArticles();
        ArticleDto ShowArticle(long articleId);
    }
}
