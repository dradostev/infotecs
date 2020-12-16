using Infotecs.Articles.Client.Rpc.Dto;

namespace Infotecs.Articles.Client.Wpf.ViewModels
{
    /// <summary>
    /// Article creation ViewModel.
    /// </summary>
    public class CreateArticleViewModel : BaseViewModel
    {
        private ArticleViewModel article;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateArticleViewModel"/> class.
        /// </summary>
        public CreateArticleViewModel()
        {
            this.article = new ArticleViewModel(new ArticleDto());
        }

        /// <summary>
        /// Gets Article ViewModel for creation.
        /// </summary>
        public ArticleViewModel Article
        {
            get => this.article;
            private set
            {
                this.article = value;
                this.OnPropertyChanged();
            }
        }
    }
}
