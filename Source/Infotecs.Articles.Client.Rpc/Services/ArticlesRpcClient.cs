using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Google.Protobuf;
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
        public async Task<IEnumerable<ArticleDto>> ListArticlesAsync()
        {
            using var chan = GrpcChannel.ForAddress(Url);

            var client = new Articles.ArticlesClient(chan);

            var reply = await client.ListArticlesAsync(new EmptyRequest());

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
        public async Task<ArticleDto> ShowArticleAsync(long articleId)
        {
            using var chan = GrpcChannel.ForAddress(Url);

            var client = new Articles.ArticlesClient(chan);

            var reply = await client.ShowArticleAsync(new ShowArticleRequest { ArticleId = articleId });

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

        /// <inheritdoc/>
        public async Task<ArticleDto> CreateArticleAsync(string username, string title, string content)
        {
            using var chan = GrpcChannel.ForAddress(Url);

            var client = new Articles.ArticlesClient(chan);

            var reply = await client.CreateArticleAsync(new CreateArticleRequest
            {
                User = username,
                Title = title,
                Content = content,
                ThumbnailImage = ByteString.CopyFrom(new byte[] { 72, 101, 108, 108, 111 }),
            });
            
            return new ArticleDto
            {
                Id = reply.ArticleId,
                Title = reply.Title,
                Username = reply.User,
                Content = reply.Content,
                Comments = new List<CommentDto>(),
            };
        }

        /// <inheritdoc/>
        public async Task<CommentDto> AddCommentAsync(long articleId, string username, string content)
        {
            using var chan = GrpcChannel.ForAddress(Url);

            var client = new Articles.ArticlesClient(chan);

            var reply = await client.AddCommentAsync(new AddCommentRequest
            {
                ArticleId = articleId,
                User = username,
                Content = content,
            });

            return new CommentDto
            {
                CommentId = reply.CommentId,
                ArticleId = reply.ArticleId,
                Username = reply.User,
                Content = reply.Content,
            };
        }

        /// <inheritdoc/>
        public async Task DeleteArticleAsync(long articleId)
        {
            using var chan = GrpcChannel.ForAddress(Url);

            var client = new Articles.ArticlesClient(chan);

            await client.DeleteArticleAsync(new DeleteArticleRequest { ArticleId = articleId });
        }
    }
}
