using System.Linq;
using System.Threading.Tasks;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Grpc.Core;
using Infotecs.Articles.Server.Domain.Entities;
using Infotecs.Articles.Server.Domain.Repositories;
using Microsoft.Extensions.Logging;

namespace Infotecs.Articles.Server.Application.Services
{
    public class ArticlesService : Articles.ArticlesBase
    {
        private readonly ILogger<ArticlesService> logger;
        private readonly IArticlesRepository articlesRepository;
        private readonly ICommentsRepository commentsRepository;

        public ArticlesService(
            ILogger<ArticlesService> logger,
            IArticlesRepository articlesRepository,
            ICommentsRepository commentsRepository)
        {
            this.logger = logger;
            this.articlesRepository = articlesRepository;
            this.commentsRepository = commentsRepository;
        }
        
        public override async Task<ArticleModel> CreateArticle(CreateArticleRequest request, ServerCallContext context)
        {
            var article = await articlesRepository.CreateAsync(
                new Article(
                    request.User, request.Title, request.Content, request.ThumbnailImage.ToByteArray()));

            return new ArticleModel
            {
                ArticleId = article.Id,
                Content = article.Content,
                Title = article.Title,
                ThumbnailImage = ByteString.CopyFrom(article.Thumbnail),
                User = article.Username
            };
        }

        public override async Task<ListArticlesReply> ListArticles(EmptyRequest request, ServerCallContext context)
        {
            var articles = await articlesRepository.ListAsync();

            return new ListArticlesReply
            {
                Articles =
                {
                    new RepeatedField<ArticleModel>
                    {
                        articles.Select(article => new ArticleModel
                        {
                            ArticleId = article.Id,
                            Content = article.Content,
                            Title = article.Title,
                            ThumbnailImage = ByteString.CopyFrom(article.Thumbnail),
                            User = article.Username
                        })
                    }
                }
            };
        }

        public override async Task<ShowArticleReply> ShowArticle(ShowArticleRequest request, ServerCallContext context)
        {
            var article = await articlesRepository.ShowAsync(request.ArticleId);

            if (article is null)
            {
                logger.LogInformation($"Article ID#{request.ArticleId} wasn't found");
                throw new RpcException(new Status(StatusCode.NotFound, "Article wasn't not found"));
            }
            
            return new ShowArticleReply
            {
                Article = new ArticleModel
                {
                    ArticleId = article.Id,
                    Content = article.Content,
                    Title = article.Title,
                    ThumbnailImage = ByteString.CopyFrom(article.Thumbnail),
                    User = article.Username
                },
                Comments =
                {
                    new RepeatedField<CommentModel>
                    {
                        article.Comments.Select(comment => new CommentModel
                        {
                            CommentId = comment.Id,
                            ArticleId = comment.ArticleId,
                            User = comment.Username,
                            Content = comment.Content
                        })
                    }
                }
            };
        }

        public override async Task<EmptyReply> DeleteArticle(DeleteArticleRequest request, ServerCallContext context)
        {
            if (!await articlesRepository.DeleteAsync(request.ArticleId))
            {
                throw new RpcException(new Status(StatusCode.NotFound, "Article wasn't not found"));
            }

            return new EmptyReply();
        }

        public override async Task<CommentModel> AddComment(AddCommentRequest request, ServerCallContext context)
        {
            var comment = await commentsRepository.CreateAsync(
                new Comment(request.ArticleId, request.User, request.Content));

            return new CommentModel
            {
                CommentId = comment.Id,
                ArticleId = comment.ArticleId,
                Content = comment.Content,
                User = comment.Username
            };
        }
    }
}