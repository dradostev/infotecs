namespace Infotecs.Articles.Server.Domain.Entities
{
    public class Comment
    {
        public long Id { get; private set; }

        public string Username { get; private set; }

        public string Content { get; private set; }

        private Comment()
        {
        }

        public Comment(string username, string content)
        {
            Username = username;
            Content = content;
        }
    }
}