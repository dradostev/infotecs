namespace Infotecs.Articles.Server.Domain.Entities
{
    public class Comment
    {
        public long Id { get; private set; }

        public string User { get; private set; }

        public string Content { get; private set; }

        private Comment()
        {
        }

        public Comment(string user, string content)
        {
            User = user;
            Content = content;
        }
    }
}