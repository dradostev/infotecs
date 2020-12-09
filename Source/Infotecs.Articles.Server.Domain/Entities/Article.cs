using System;
using System.Collections.Generic;

namespace Infotecs.Articles.Server.Domain.Entities
{
    public class Article
    {
        public long Id { get; private set; }

        public string User { get; private set; }

        public string Title { get; private set; }

        public string Content { get; private set; }

        public Uri Thumbnail { get; private set; }

        public IEnumerable<Comment> Comments { get; private set; }

        private Article()
        {
        }

        public Article(string user, string title, string content, Uri thumbnail)
        {
            User = user;
            Title = title;
            Content = content;
            Thumbnail = thumbnail;
        }
    }
}