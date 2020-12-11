namespace Infotecs.Articles.Server.Database.Repositories
{
    using System.Collections.Generic;
    using System.Data;
    using System.Threading.Tasks;
    using Dapper;
    using Infotecs.Articles.Server.Domain.Entities;
    using Infotecs.Articles.Server.Domain.Repositories;
    using Npgsql;

    public class CommentsRepository : ICommentsRepository
    {
        private readonly string connectionString;

        /// <summary>
        /// Initializes a new instance of the <see cref="CommentsRepository"/> class.
        /// </summary>
        /// <param name="connectionString">Connection string for PostgreSQL database.</param>
        public CommentsRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        private IDbConnection DbConnection => new NpgsqlConnection(this.connectionString);

        /// <inheritdoc/>
        public async Task<IEnumerable<Comment>> ListByArticleAsync(long articleId)
        {
            using var connection = this.DbConnection;
            connection.Open();

            var comments = await connection.QueryAsync<Comment>(
                @"select * from comments where article_id = @Id", new { @Id = articleId });

            return comments;
        }

        /// <inheritdoc/>
        public async Task<Comment> CreateAsync(Comment entity)
        {
            using var connection = this.DbConnection;
            connection.Open();

            var commentId = await connection.ExecuteScalarAsync<long>(
                @"insert into comments (article_id, username, content) 
                    values (@ArticleId, @Username, @Content)
                    returning id",
                entity);

            var comment = await connection.QuerySingleAsync<Comment>(
                @"select id as Id, article_id as ArticleId, username as Username, content as Content
                     from comments where id = @Id", new { Id = commentId });

            return comment;
        }

        /// <inheritdoc/>
        public async Task<bool> DeleteAsync(long commentId)
        {
            using var connection = this.DbConnection;
            connection.Open();

            var rows = await connection.ExecuteAsync(
                @"delete from comments where id = @Id", new { Id = commentId });

            return rows != 0;
        }
    }
}