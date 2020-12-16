using System.Collections.ObjectModel;
using Infotecs.Articles.Client.Rpc.Dto;

namespace Infotecs.Articles.Client.Wpf.ViewModels
{
    public class ArticleViewModel : BaseViewModel
    {
        private readonly ArticleDto articleDto;

        public ArticleViewModel(ArticleDto articleDto)
        {
            this.articleDto = articleDto;

            foreach (var comment in articleDto.Comments)
            {
                this.Comments.Add(new CommentViewModel(comment));
            }
        }

        public long Id => this.articleDto.Id;

        public string User => this.articleDto.Username;

        public string Title
        {
            get => this.articleDto.Title;
            set
            {
                this.articleDto.Title = value;
                this.OnPropertyChanged(nameof(this.Title));
            }
        }

        public string Content
        {
            get => this.articleDto.Content;
            set
            {
                this.articleDto.Content = value;
                this.OnPropertyChanged(nameof(this.Content));
            }
        }

        public byte[] Thumbnail
        {
            get => this.articleDto.Thumbnail;
            set
            {
                this.articleDto.Thumbnail = value;
                this.OnPropertyChanged(nameof(this.Thumbnail));
            }
        }

        public ObservableCollection<CommentViewModel> Comments { get; } = new ObservableCollection<CommentViewModel>();
    }
}
