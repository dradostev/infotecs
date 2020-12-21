using System.ComponentModel.DataAnnotations;

namespace Infotecs.Articles.Client.WebApp.Requests
{
    /// <summary>
    /// Create Comment HTTP request model.
    /// </summary>
    public class AddComment
    {
        /// <summary>
        /// Gets or sets attached article foreign key
        /// </summary>
        [Required]
        [Range(0, long.MaxValue)]
        public long ArticleId { get; set; }

        /// <summary>
        /// Gets or sets name of a user who created the comment
        /// </summary>
        [Required]
        [MinLength(3)]
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets text content of the comment
        /// </summary>
        [Required]
        [MinLength(10)]
        public string Content { get; set; }
    }
}
