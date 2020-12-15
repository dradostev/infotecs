namespace Infotecs.Articles.Client.Wpf.ViewModels
{
    public class ArticlePageViewModel : BaseViewModel
    {
        public ArticleViewModel CurrentArticle
        {
            get => this.currentArticle;
            set
            {
                this.currentArticle = value;
                this.OnPropertyChanged();
            }
        }

        private ArticleViewModel currentArticle;
    }
}
