using System.Collections.Generic;
using Infotecs.Articles.Client.Rpc.Models;

namespace Infotecs.Articles.Client.Rpc.Services
{
    public interface IRpcClient
    {
        IEnumerable<Article> ListArticles();
        Article ShowArticle(long articleId);
    }
}
