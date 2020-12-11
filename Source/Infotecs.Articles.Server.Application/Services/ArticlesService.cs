namespace Infotecs.Articles.Server.Application.Services
{
    using System.Linq;
    using System.Threading.Tasks;
    using FluentValidation;
    using Google.Protobuf;
    using Google.Protobuf.Collections;
    using Grpc.Core;
    using Infotecs.Articles.Server.Domain.Entities;
    using Infotecs.Articles.Server.Domain.Repositories;
    using Microsoft.Extensions.Logging;

    public class ArticlesService : Articles.ArticlesBase
    {
        private readonly ILogger<ArticlesService> logger;
        private readonly IArticlesRepository articlesRepository;
        private readonly ICommentsRepository commentsRepository;
        private readonly IValidatorFactory validatorFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="ArticlesService"/> class.
        /// </summary>
        /// <param name="logger">Logger interface.</param>
        /// <param name="articlesRepository">Articles repository.</param>
        /// <param name="commentsRepository">Comments repository.</param>
        /// <param name="validatorFactory">FluentValidation Validator Factory.</param>
        public ArticlesService(
            ILogger<ArticlesService> logger,
            IArticlesRepository articlesRepository,
            ICommentsRepository commentsRepository,
            IValidatorFactory validatorFactory)
        {
            this.logger = logger;
            this.articlesRepository = articlesRepository;
            this.commentsRepository = commentsRepository;
            this.validatorFactory = validatorFactory;
        }

        /// Create operation for Article entity
        /// <inheritdoc/>
        public override async Task<ArticleModel> CreateArticle(CreateArticleRequest request, ServerCallContext context)
        {
            var validator = this.validatorFactory.GetValidator<CreateArticleRequest>();

            var result = await validator.ValidateAsync(request);

            if (!result.IsValid)
            {
                this.logger.LogInformation($"Validation error occurred");
                throw new RpcException(
                    new Status(StatusCode.InvalidArgument, "Validation error"));
            }

            var article = await this.articlesRepository.CreateAsync(
                new Article(
                    request.User, request.Title, request.Content, request.ThumbnailImage.ToByteArray()));

            return new ArticleModel
            {
                ArticleId = article.Id,
                Content = article.Content,
                Title = article.Title,
                ThumbnailImage = ByteString.CopyFrom(article.Thumbnail),
                User = article.Username,
            };
        }

        /// List all Article entities
        /// <inheritdoc/>
        public override async Task<ListArticlesReply> ListArticles(EmptyRequest request, ServerCallContext context)
        {
            var articles = await this.articlesRepository.ListAsync();

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
                            User = article.Username,
                        }),
                    },
                },
            };
        }

        /// Show one Article entity by ID
        /// <inheritdoc/>
        public override async Task<ShowArticleReply> ShowArticle(ShowArticleRequest request, ServerCallContext context)
        {
            var article = await this.articlesRepository.ShowAsync(request.ArticleId);

            if (article is null)
            {
                this.logger.LogInformation($"Article ID#{request.ArticleId} wasn't found");
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
                    User = article.Username,
                },
                Comments =
                {
                    new RepeatedField<CommentModel>
                    {
                        article.Comments.Select(comment => new CommentModel
                        {
                            CommentId = comment.CommentId,
                            ArticleId = comment.ArticleId,
                            User = comment.Username,
                            Content = comment.Content,
                        }),
                    },
                },
            };
        }

        /// Delete one Article entity by ID
        /// <inheritdoc/>
        public override async Task<EmptyReply> DeleteArticle(DeleteArticleRequest request, ServerCallContext context)
        {
            if (!await this.articlesRepository.DeleteAsync(request.ArticleId))
            {
                this.logger.LogInformation($"Article ID#{request.ArticleId} wasn't found");
                throw new RpcException(new Status(StatusCode.NotFound, "Article wasn't not found"));
            }

            return new EmptyReply();
        }

        /// Create one Comment entity and attach it to Article entity by ID
        /// <inheritdoc/>
        public override async Task<CommentModel> AddComment(AddCommentRequest request, ServerCallContext context)
        {
            var validator = this.validatorFactory.GetValidator<AddCommentRequest>();

            var result = await validator.ValidateAsync(request);

            if (!result.IsValid)
            {
                this.logger.LogInformation($"Validation error occurred");
                throw new RpcException(
                    new Status(StatusCode.InvalidArgument, "Validation error"));
            }

            var comment = await this.commentsRepository.CreateAsync(
                new Comment(request.ArticleId, request.User, request.Content));

            return new CommentModel
            {
                CommentId = comment.CommentId,
                ArticleId = comment.ArticleId,
                Content = comment.Content,
                User = comment.Username,
            };
        }
    }
}