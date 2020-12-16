namespace Infotecs.Articles.Client.Rpc.Dto
{
    /// <summary>
    /// Comment DTO.
    /// </summary>
    public class CommentDto
    {
        /// <summary>
        /// Gets database primary key
        /// </summary>
        public long CommentId { get; set; }

        /// <summary>
        /// Gets attached article foreign key
        /// </summary>
        public long ArticleId { get; set; }

        /// <summary>
        /// Gets name of a user who created the comment
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Gets text content of the comment
        /// </summary>
        public string Content { get; set; }
    }
}
