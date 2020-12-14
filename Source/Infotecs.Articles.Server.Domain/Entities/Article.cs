namespace Infotecs.Articles.Server.Domain.Entities
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Comment is an entity user can create and write things in.
    /// </summary>
    public class Article
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Article"/> class.
        /// </summary>
        /// <param name="username">User Login.</param>
        /// <param name="title">Title of article.</param>
        /// <param name="content">Text content of article.</param>
        /// <param name="thumbnail">Article preview image as a byte array.</param>
        public Article(string username, string title, string content, byte[] thumbnail)
        {
            this.Username = username;
            this.Title = title;
            this.Content = content;
            this.Thumbnail = thumbnail;
        }

        private Article()
        {
        }

        /// <summary>
        /// Gets database primary key
        /// </summary>
        public long Id { get; private set; }

        /// <summary>
        /// Gets a user who created Article
        /// </summary>
        public string Username { get; private set; }

        /// <summary>
        /// Gets Article title
        /// </summary>
        public string Title { get; private set; }

        /// <summary>
        /// Gets text content of the Article
        /// </summary>
        public string Content { get; private set; }

        /// <summary>
        /// Gets Article thumbnail image as byte array
        /// </summary>
        public byte[] Thumbnail { get; private set; }

        /// <summary>
        /// Gets collection on Comments attached to the Article
        /// </summary>
        public IList<Comment> Comments { get; private set; } = new List<Comment>();
    }
}