using System.Linq;
using System.Threading.Tasks;
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
        
        public override Task<ArticleModel> CreateArticle(CreateArticleRequest request, ServerCallContext context)
        {
            logger.LogInformation("Create Article Request");
            return base.CreateArticle(request, context);
        }

        public override async Task<ListArticlesReply> ListArticles(EmptyRequest request, ServerCallContext context)
        {
            logger.LogInformation("List Articles Request");

            var articles = await articleRepository.List();

            return await Task.FromResult(new ListArticlesReply
            {
                Articles =
                {
                    new RepeatedField<ArticleModel>
                    {
                        articles.Select(x => new ArticleModel
                        {
                            ArticleId = x.Id,
                            Title = x.Title,
                            Content = x.Content
                        })
                    }
                }
            });
        }

        public override Task<ShowArticleReply> ShowArticle(ShowArticleRequest request, ServerCallContext context)
        {
            return base.ShowArticle(request, context);
        }

        public override Task<EmptyReply> DeleteArticle(DeleteArticleRequest request, ServerCallContext context)
        {
            return base.DeleteArticle(request, context);
        }

        public override Task<CommentModel> AddComment(AddCommentRequest request, ServerCallContext context)
        {
            return base.AddComment(request, context);
        }
    }
}