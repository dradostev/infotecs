﻿namespace Infotecs.Articles.Server.Database.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Threading.Tasks;
    using Dapper;
    using Infotecs.Articles.Server.Domain.Entities;
    using Infotecs.Articles.Server.Domain.Repositories;
    using Npgsql;

    /// <summary>
    /// Articles database repository implementation.
    /// </summary>
    public class ArticlesRepository : IArticlesRepository
    {
        private readonly string connectionString;

        /// <summary>
        /// Initializes a new instance of the <see cref="ArticlesRepository"/> class.
        /// </summary>
        /// <param name="connectionString">Connection string for PostgreSQL database.</param>
        public ArticlesRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        private IDbConnection DbConnection => new NpgsqlConnection(this.connectionString);

        /// <inheritdoc/>
        public async Task<Article> ShowAsync(long articleId)
        {
            using var connection = this.DbConnection;
            connection.Open();

            Article article = null;

            await connection.QueryAsync<Article, Comment, Article>(
                @"select
                        articles.*,
                        comments.id CommentId, comments.article_id,
                        comments.username, comments.content
                    from articles inner join comments on articles.id = comments.article_id
                    where articles.id = @Id",
                param: new { @Id = articleId },
                map: (a, c) =>
                {
                    article ??= a;
                    article.Comments.Add(c);
                    return article;
                },
                splitOn: "article_id");

            return article;
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<Article>> ListAsync()
        {
            using var connection = this.DbConnection;
            connection.Open();

            var articles = await connection.QueryAsync<Article>(
                @"select * from articles");

            return articles;
        }

        /// <inheritdoc/>
        public async Task<Article> CreateAsync(Article entity)
        {
            using var connection = this.DbConnection;
            connection.Open();

            var articleId = await connection.ExecuteScalarAsync<long>(
                @"insert into articles (username, title, content, thumbnail) 
                    values (@Username, @Title, @Content, @Thumbnail)
                    returning id",
                entity);

            var article = await connection.QuerySingleAsync<Article>(
                @"select * from articles where id = @Id", new { Id = articleId });

            return article;
        }

        /// <inheritdoc/>
        public async Task<bool> DeleteAsync(long articleId)
        {
            using var connection = this.DbConnection;
            connection.Open();

            var rows = await connection.ExecuteAsync(
                @"delete from articles where id = @Id", new { Id = articleId });

            return rows != 0;
        }
    }
}