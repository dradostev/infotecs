using System.Threading.Tasks;
using Grpc.Core;

namespace Infotecs.Articles.Server.Application.Services
{
    public class ArticlesService : Articles.ArticlesBase
    {
        public override Task<Article> CreateArticle(CreateArticleRequest request, ServerCallContext context)
        {
            return base.CreateArticle(request, context);
        }

        public override Task<ListArticlesReply> ListArticles(EmptyRequest request, ServerCallContext context)
        {
            return base.ListArticles(request, context);
        }

        public override Task<ShowArticleReply> ShowArticle(ShowArticleRequest request, ServerCallContext context)
        {
            return base.ShowArticle(request, context);
        }

        public override Task<EmptyReply> DeleteArticle(DeleteArticleRequest request, ServerCallContext context)
        {
            return base.DeleteArticle(request, context);
        }

        public override Task<Comment> AddComment(AddCommentRequest request, ServerCallContext context)
        {
            return base.AddComment(request, context);
        }
    }
}