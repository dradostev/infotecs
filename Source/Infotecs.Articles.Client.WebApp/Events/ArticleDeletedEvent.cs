namespace Infotecs.Articles.Client.WebApp.Events
{
    /// <summary>
    /// Article deleted notification event.
    /// </summary>
    public class ArticleDeletedEvent
    {
        /// <summary>
        /// Gets database primary key
        /// </summary>
        public long Id { get; set; }
    }
}
