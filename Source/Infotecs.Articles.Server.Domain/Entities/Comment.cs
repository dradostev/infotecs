namespace Infotecs.Articles.Server.Domain.Entities
{
    public class Comment
    {
        public long CommentId { get; private set; }

        public long ArticleId { get; private set; }

        public string Username { get; private set; }

        public string Content { get; private set; }

        private Comment()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Comment"/> class.
        /// </summary>
        /// <param name="articleId">ID of article this comment is attached to.</param>
        /// <param name="username">Login of comment author.</param>
        /// <param name="content">Text content of the comment.</param>
        public Comment(long articleId, string username, string content)
        {
            ArticleId = articleId;
            Username = username;
            Content = content;
        }
    }
}