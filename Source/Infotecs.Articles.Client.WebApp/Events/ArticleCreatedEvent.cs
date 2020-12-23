namespace Infotecs.Articles.Client.WebApp.Events
{
    /// <summary>
    /// Article Created notification event.
    /// </summary>
    public class ArticleCreatedEvent
    {
        /// <summary>
        /// Gets database primary key
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Gets a user who created Article
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Gets Article title
        /// </summary>
        public string Title { get; set; }
    }
}
