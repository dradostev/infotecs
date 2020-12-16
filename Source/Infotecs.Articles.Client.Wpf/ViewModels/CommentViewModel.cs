namespace Infotecs.Articles.Client.Wpf.ViewModels
{
    using Infotecs.Articles.Client.Rpc.Models;

    /// <summary>
    /// Comment ViewModel.
    /// </summary>
    public class CommentViewModel : BaseViewModel
    {
        private readonly Comment comment;

        /// <summary>
        /// Initializes a new instance of the <see cref="CommentViewModel"/> class.
        /// </summary>
        /// <param name="comment">Comment DTO.</param>
        public CommentViewModel(Comment comment)
        {
            this.comment = comment;
        }

        /// <summary>
        /// Gets database primary key
        /// </summary>
        public long CommentId => this.comment.CommentId;

        /// <summary>
        /// Gets attached article foreign key
        /// </summary>
        public long ArticleId => this.comment.ArticleId;

        /// <summary>
        /// Gets name of a user who created the comment
        /// </summary>
        public string Username
        {
            get => this.comment.Username;
            set
            {
                this.comment.Username = value;
                this.OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets text content of the comment
        /// </summary>
        public string Content
        {
            get => this.comment.Content;
            set
            {
                this.comment.Content = value;
                this.OnPropertyChanged();
            }
        }
    }
}
