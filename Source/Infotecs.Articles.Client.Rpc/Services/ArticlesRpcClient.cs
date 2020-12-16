using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using Grpc.Net.Client;
using Infotecs.Articles.Client.Rpc.Dto;

namespace Infotecs.Articles.Client.Rpc.Services
{
    /// <summary>
    /// <inheritdoc cref="IArticlesRpcClient"/>
    /// </summary>
    public class ArticlesRpcClient : IArticlesRpcClient
    {
        private const string Url = "http://localhost:5001";

        /// <summary>
        /// Initializes a new instance of the <see cref="ArticlesRpcClient"/> class.
        /// </summary>
        public ArticlesRpcClient()
        {
            AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);
        }

        /// <inheritdoc/>
        public IEnumerable<ArticleDto> ListArticles()
        {
            using var chan = GrpcChannel.ForAddress(Url);

            var client = new Articles.ArticlesClient(chan);

            var reply = client.ListArticles(new EmptyRequest());

            return reply.Articles.Select(x => new ArticleDto
            {
                Id = x.ArticleId,
                Title = x.Title,
                Username = x.User,
                Content = x.Content,
                Thumbnail = x.ThumbnailImage.ToByteArray(),
            });
        }

        /// <inheritdoc/>
        public ArticleDto ShowArticle(long articleId)
        {
            using var chan = GrpcChannel.ForAddress(Url);

            var client = new Articles.ArticlesClient(chan);

            var reply = client.ShowArticle(new ShowArticleRequest { ArticleId = articleId });

            return new ArticleDto
            {
                Id = reply.Article.ArticleId,
                Title = reply.Article.Title,
                Username = reply.Article.User,
                Content = reply.Article.Content,
                Comments = reply.Comments.Select(x => new CommentDto
                {
                    CommentId = x.CommentId,
                    ArticleId = x.ArticleId,
                    Username = x.User,
                    Content = x.Content,
                }).ToList(),
            };
        }
    }
}
