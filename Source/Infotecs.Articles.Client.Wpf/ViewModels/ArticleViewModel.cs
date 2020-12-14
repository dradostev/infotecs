namespace Infotecs.Articles.Client.Wpf.ViewModels
{
    using Infotecs.Articles.Client.Rpc.Models;

    public class ArticleViewModel : BaseViewModel
    {
        private readonly Article article;

        public ArticleViewModel(Article article)
        {
            this.article = article;
        }

        public long Id => this.article.Id;

        public string User => this.article.Username;

        public string Title
        {
            get => this.article.Title;
            set
            {
                this.article.Title = value;
                this.OnPropertyChanged(nameof(this.Title));
            }
        }

        public string Content
        {
            get => this.article.Content;
            set
            {
                this.article.Content = value;
                this.OnPropertyChanged(nameof(this.Content));
            }
        }

        public byte[] Thumbnail
        {
            get => this.article.Thumbnail;
            set
            {
                this.article.Thumbnail = value;
                this.OnPropertyChanged(nameof(this.Thumbnail));
            }
        }
    }
}
