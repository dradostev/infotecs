using System.Collections.Generic;

namespace Infotecs.Articles.Client.Rpc.Dto
{
    /// <summary>
    /// Article DTO.
    /// </summary>
    public class ArticleDto
    {
        /// <summary>
        /// Gets database primary key
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Gets a user who created Article
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Gets Article title
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets text content of the Article
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// Gets Article thumbnail image as byte array
        /// </summary>
        public byte[] Thumbnail { get; set; }

        /// <summary>
        /// Gets collection on Comments attached to the Article
        /// </summary>
        public IList<CommentDto> Comments { get; set; } = new List<CommentDto>();
    }
}
