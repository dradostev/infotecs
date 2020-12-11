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

        public Comment(long articleId, string username, string content)
        {
            ArticleId = articleId;
            Username = username;
            Content = content;
        }
    }
}