namespace Infotecs.Articles.Server.Domain.Entities
{
    using System;
    using System.Collections.Generic;

    public class Article
    {
        public long Id { get; private set; }

        public string Username { get; private set; }

        public string Title { get; private set; }

        public string Content { get; private set; }

        public byte[] Thumbnail { get; private set; }

        public IList<Comment> Comments { get; private set; } = new List<Comment>();

        private Article()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Article"/> class.
        /// </summary>
        /// <param name="username">User Login.</param>
        /// <param name="title">Title of article.</param>
        /// <param name="content">Text content of article.</param>
        /// <param name="thumbnail">Article preview image as a byte array.</param>
        public Article(string username, string title, string content, byte[] thumbnail)
        {
            Username = username;
            Title = title;
            Content = content;
            Thumbnail = thumbnail;
        }
    }
}