using System.Threading.Tasks;
using Infotecs.Articles.Client.WebApp.Events;
using Microsoft.AspNetCore.SignalR;

namespace Infotecs.Articles.Client.WebApp.Hubs
{
    /// <summary>
    /// SignalR hub for publishing notification events.
    /// </summary>
    public class ArticlesHub : Hub
    {
        /// <summary>
        /// Fires when article has been created.
        /// </summary>
        /// <param name="e">Article created event payload.</param>
        /// <returns>Async Task.</returns>
        public async Task OnArticleCreatedAsync(ArticleCreatedEvent e)
        {
            await this.Clients.All.SendAsync("ArticleCreatedEvent", e);
        }

        /// <summary>
        /// Fires when article has been deleted.
        /// </summary>
        /// <param name="e">Article deleted event payload.</param>
        /// <returns>Async Task.</returns>
        public async Task OnArticleDeletedAsync(ArticleDeletedEvent e)
        {
            await this.Clients.All.SendAsync("ArticleDeletedEvent", e);
        }

        /// <summary>
        /// Fires when comment has been added to an article.
        /// </summary>
        /// <param name="e">Comment added event payload.</param>
        /// <returns>Async Task.</returns>
        public async Task OnCommentAddedAsync(CommentAddedEvent e)
        {
            await this.Clients.All.SendAsync("CommentAddedEvent", e);
        }
    }
}
