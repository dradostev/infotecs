using System;
using System.Collections.Generic;
using Infotecs.Articles.Server.Domain.Entities;

namespace Infotecs.Articles.Server.Database
{
    public class InMemoryDb
    {
        public List<Article> Articles = new List<Article>
        {
            new Article("vasya", "Vodka", "Alala", new Uri("http://google.com")),
            new Article("petya", "Beer", "Alala", new Uri("http://google.com")),
            new Article("hui", "Cognac", "Alala", new Uri("http://google.com")),
        };
        public List<Comment> Comments = new List<Comment>();
    }
}