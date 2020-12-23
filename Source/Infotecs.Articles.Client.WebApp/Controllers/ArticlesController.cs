using System.Collections.Generic;
using System.Threading.Tasks;
using Grpc.Core;
using Infotecs.Articles.Client.Rpc.Dto;
using Infotecs.Articles.Client.Rpc.Services;
using Infotecs.Articles.Client.WebApp.Events;
using Infotecs.Articles.Client.WebApp.Hubs;
using Infotecs.Articles.Client.WebApp.Requests;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Infotecs.Articles.Client.WebApp.Controllers
{
    /// <summary>
    /// Articles CRUD HTTP controller.
    /// </summary>
    [ApiController]
    [Route("articles")]
    public class ArticlesController : Controller
    {
        private readonly IArticlesRpcClient rpcClient;
        private readonly ILogger<ArticlesController> logger;
        private readonly ArticlesHub hub;

        /// <summary>
        /// Initializes a new instance of the <see cref="ArticlesController"/> class.
        /// </summary>
        /// <param name="rpcClient">RPC client injection.</param>
        /// <param name="logger">Logger injection.</param>
        /// <param name="hub">SignalR hub injection.</param>
        public ArticlesController(
            IArticlesRpcClient rpcClient,
            ILogger<ArticlesController> logger,
            ArticlesHub hub)
        {
            this.rpcClient = rpcClient;
            this.logger = logger;
            this.hub = hub;
        }

        /// <summary>
        /// List Articles.
        /// </summary>
        /// <returns>JSON Result with Articles list.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ArticleDto>>> ListArticlesAsync()
        {
            var articles = await this.rpcClient.ListArticlesAsync();
            return this.Ok(articles);
        }


        /// <summary>
        /// Gets Article by ID or NotFound error.
        /// </summary>
        /// <param name="articleId">Article ID.</param>
        /// <returns>JSON Result with Article or NotFound result.</returns>
        [HttpGet("{articleId}")]
        public async Task<ActionResult<ArticleDto>> ShowArticleAsync(long articleId)
        {
            try
            {
                var article = await this.rpcClient.ShowArticleAsync(articleId);
                return this.Ok(article);
            }
            catch (RpcException e)
            {
                this.logger.LogInformation($"RPC error: {e.Message}");
                return this.NotFound();
            }
        }

        /// <summary>
        /// Create new Article by HTTP request.
        /// </summary>
        /// <param name="request">Create Article model.</param>
        /// <returns>JSON Result with newly created article.</returns>
        [HttpPost]
        public async Task<ActionResult<ArticleDto>> CreateArticleAsync([FromBody] CreateArticle request)
        {
            var article = await this.rpcClient.CreateArticleAsync(request.Username, request.Title, request.Content);

            await this.hub.OnArticleCreatedAsync(new ArticleCreatedEvent
            {
                Id = article.Id,
                Title = article.Title,
                Username = article.Username
            });

            return this.StatusCode(201, article);
        }

        /// <summary>
        /// Deletes Article by ID.
        /// </summary>
        /// <param name="articleId">Article ID.</param>
        /// <returns>No Content Result or NotFound Result.</returns>
        [HttpDelete("{articleId}")]
        public async Task<IActionResult> DeleteArticleAsync(long articleId)
        {
            try
            {
                await this.rpcClient.DeleteArticleAsync(articleId);

                await this.hub.OnArticleDeletedAsync(new ArticleDeletedEvent { Id = articleId });
                
                return this.NoContent();
            }
            catch (RpcException e)
            {
                this.logger.LogInformation($"RPC error: {e.Message}");
                return this.NotFound();
            }
        }

        /// <summary>
        /// Adds a Comment to Article by ID.
        /// </summary>
        /// <param name="articleId">Article ID.</param>
        /// <param name="request">Add Comment request model.</param>
        /// <returns>JSON result with newly created Comment or NotFound result.</returns>
        [HttpPost("{articleId}/comments")]
        public async Task<ActionResult<CommentDto>> AddCommentAsync(long articleId, [FromBody] AddComment request)
        {
            try
            {
                var comment = await this.rpcClient.AddCommentAsync(articleId, request.Username, request.Content);

                await this.hub.OnCommentAddedAsync(new CommentAddedEvent
                {
                    CommentId = comment.CommentId,
                    ArticleId = comment.ArticleId,
                    Username = comment.Username,
                    Content = comment.Content
                });
                
                return this.StatusCode(201, comment);
            }
            catch (RpcException e)
            {
                this.logger.LogInformation($"RPC error: {e.Message}");
                return this.NotFound();
            }
        }
    }
}
