using System;
using System.Collections.Generic;

namespace Infotecs.Articles.Server.Domain.Entities
{
    public class Article
    {
        public long Id { get; private set; }

        public string Username { get; private set; }

        public string Title { get; private set; }

        public string Content { get; private set; }

        public byte[] Thumbnail { get; private set; }

        public IEnumerable<Comment> Comments { get; private set; }

        private Article()
        {
        }

        public Article(string username, string title, string content, byte[] thumbnail)
        {
            Username = username;
            Title = title;
            Content = content;
            Thumbnail = thumbnail;
        }
    }
}