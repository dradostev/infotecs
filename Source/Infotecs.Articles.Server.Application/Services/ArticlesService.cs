using System.Linq;
using System.Threading.Tasks;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Grpc.Core;
using Infotecs.Articles.Server.Domain.Entities;
using Infotecs.Articles.Server.Domain.Repositories;
using Infotecs.Articles.Server.Domain.Repositories;
using Microsoft.Extensions.Logging;

namespace Infotecs.Articles.Server.Application.Services
{
    public class ArticlesService : Articles.ArticlesBase
    {
        private readonly ILogger<ArticlesService> logger;
        private readonly IRepository<Article> articleRepository;

        public ArticlesService(ILogger<ArticlesService> logger, IRepository<Article> articleRepository)
        {
            this.logger = logger;
            this.articleRepository = articleRepository;
        }
        
        public override async Task<ArticleModel> CreateArticle(CreateArticleRequest request, ServerCallContext context)
        {
            var article = await articleRepository.CreateAsync(
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
            var articles = await articleRepository.ListAsync();

            return await Task.FromResult(new ListArticlesReply
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
            });
        }

        public override async Task<ShowArticleReply> ShowArticle(ShowArticleRequest request, ServerCallContext context)
        {
            var article = await articleRepository.ShowAsync(request.ArticleId);

            if (article is null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, "Article not found"));
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
                }
            };
        }

        public override async Task<EmptyReply> DeleteArticle(DeleteArticleRequest request, ServerCallContext context)
        {
            await articleRepository.DeleteAsync(request.ArticleId);

            return new EmptyReply();
        }

        public override Task<CommentModel> AddComment(AddCommentRequest request, ServerCallContext context)
        {
            return base.AddComment(request, context);
        }
    }
}