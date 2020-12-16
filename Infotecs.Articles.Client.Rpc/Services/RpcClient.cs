using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using Grpc.Net.Client;
using Infotecs.Articles.Client.Rpc.Models;

namespace Infotecs.Articles.Client.Rpc.Services
{
    public class RpcClient : IRpcClient
    {
        private const string Url = "http://localhost:5001";

        public RpcClient()
        {
            AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);
        }

        public IEnumerable<Article> ListArticles()
        {
            using var chan = GrpcChannel.ForAddress(Url);

            var client = new Articles.ArticlesClient(chan);

            var reply = client.ListArticles(new EmptyRequest());

            return reply.Articles.Select(x => new Article
            {
                Id = x.ArticleId,
                Title = x.Title,
                Username = x.User,
                Content = x.Content,
                Thumbnail = x.ThumbnailImage.ToByteArray(),
            });
        }

        public Article ShowArticle(long articleId)
        {
            using var chan = GrpcChannel.ForAddress(Url);

            var client = new Articles.ArticlesClient(chan);

            try
            {
                var reply = client.ShowArticle(new ShowArticleRequest { ArticleId = articleId });
                return new Article
                {
                    Id = reply.Article.ArticleId,
                    Title = reply.Article.Title,
                    Username = reply.Article.User,
                    Content = reply.Article.Content,
                    Comments = reply.Comments.Select(x => new Comment
                    {
                        CommentId = x.CommentId,
                        ArticleId = x.ArticleId,
                        Username = x.User,
                        Content = x.Content,
                    }).ToList(),
                };
            }
            catch (RpcException e)
            {
                return null;
            }
        }
    }
}
