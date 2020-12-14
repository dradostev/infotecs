namespace Infotecs.Articles.Server.Domain.Entities
{
    /// <summary>
    /// Comment is an entity user can create and attach to Article.
    /// </summary>
    public class Comment
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Comment"/> class.
        /// </summary>
        /// <param name="articleId">ID of article this comment is attached to.</param>
        /// <param name="username">Login of comment author.</param>
        /// <param name="content">Text content of the comment.</param>
        public Comment(long articleId, string username, string content)
        {
            this.ArticleId = articleId;
            this.Username = username;
            this.Content = content;
        }

        private Comment()
        {
        }

        /// <summary>
        /// Gets database primary key
        /// </summary>
        public long CommentId { get; private set; }

        /// <summary>
        /// Gets attached article foreign key
        /// </summary>
        public long ArticleId { get; private set; }

        /// <summary>
        /// Gets name of a user who created the comment
        /// </summary>
        public string Username { get; private set; }

        /// <summary>
        /// Gets text content of the comment
        /// </summary>
        public string Content { get; private set; }
    }
}