using System.Collections.ObjectModel;
using Infotecs.Articles.Client.Rpc.Dto;

namespace Infotecs.Articles.Client.Wpf.ViewModels
{
    /// <summary>
    /// Article ViewModel.
    /// </summary>
    public class ArticleViewModel : BaseViewModel
    {
        private readonly ArticleDto articleDto;

        /// <summary>
        /// Initializes a new instance of the <see cref="ArticleViewModel"/> class.
        /// </summary>
        /// <param name="articleDto">Article model.</param>
        public ArticleViewModel(ArticleDto articleDto)
        {
            this.articleDto = articleDto;

            foreach (var comment in articleDto.Comments)
            {
                this.Comments.Add(new CommentViewModel(comment));
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ArticleViewModel"/> class.
        /// </summary>
        public ArticleViewModel()
        {
        }

        /// <summary>
        /// Gets Article ID.
        /// </summary>
        public long Id => this.articleDto.Id;

        /// <summary>
        /// Gets or sets Article author's name.
        /// </summary>
        public string User
        {
            get => this.articleDto.Username;
            set
            {
                this.articleDto.Username = value;
                this.OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets Article title.
        /// </summary>
        public string Title
        {
            get => this.articleDto.Title;
            set
            {
                this.articleDto.Title = value;
                this.OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets Article text content.
        /// </summary>
        public string Content
        {
            get => this.articleDto.Content;
            set
            {
                this.articleDto.Content = value;
                this.OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets Article thumbnail image as byte array.
        /// </summary>
        public byte[] Thumbnail
        {
            get => this.articleDto.Thumbnail;
            set
            {
                this.articleDto.Thumbnail = value;
                this.OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets observable collection of Comment ViewModels.
        /// </summary>
        public ObservableCollection<CommentViewModel> Comments { get; } = new ObservableCollection<CommentViewModel>();
    }
}
