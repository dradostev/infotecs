using Infotecs.Articles.Client.Rpc.Dto;

namespace Infotecs.Articles.Client.Wpf.ViewModels
{
    /// <summary>
    /// Comment ViewModel.
    /// </summary>
    public class CommentViewModel : BaseViewModel
    {
        private readonly CommentDto commentDto;

        /// <summary>
        /// Initializes a new instance of the <see cref="CommentViewModel"/> class.
        /// </summary>
        /// <param name="commentDto">Comment DTO.</param>
        public CommentViewModel(CommentDto commentDto)
        {
            this.commentDto = commentDto;
        }

        /// <summary>
        /// Gets database primary key
        /// </summary>
        public long CommentId => this.commentDto.CommentId;

        /// <summary>
        /// Gets attached article foreign key
        /// </summary>
        public long ArticleId => this.commentDto.ArticleId;

        /// <summary>
        /// Gets name of a user who created the comment
        /// </summary>
        public string Username
        {
            get => this.commentDto.Username;
            set
            {
                this.commentDto.Username = value;
                this.OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets text content of the comment
        /// </summary>
        public string Content
        {
            get => this.commentDto.Content;
            set
            {
                this.commentDto.Content = value;
                this.OnPropertyChanged();
            }
        }
    }
}
