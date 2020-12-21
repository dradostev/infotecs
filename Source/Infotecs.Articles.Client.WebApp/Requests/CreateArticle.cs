using System.ComponentModel.DataAnnotations;

namespace Infotecs.Articles.Client.WebApp.Requests
{
    /// <summary>
    /// Create Article HTTP request model.
    /// </summary>
    public class CreateArticle
    {
        /// <summary>
        /// Gets or sets a user who created Article
        /// </summary>
        [Required]
        [MinLength(3)]
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets article title
        /// </summary>
        [Required]
        [MinLength(3)]
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets text content of the Article
        /// </summary>
        [Required]
        [MinLength(10)]
        public string Content { get; set; }
    }
}
