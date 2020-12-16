using System.Collections.ObjectModel;
using Infotecs.Articles.Client.Rpc.Services;
using Infotecs.Articles.Client.Wpf.Events;
using Prism.Events;

namespace Infotecs.Articles.Client.Wpf.ViewModels
{
    /// <summary>
    /// ViewModel for Articles in sidebar.
    /// </summary>
    public class SidebarViewModel : BaseViewModel
    {
        private readonly IArticlesRpcClient articlesRpcClient;

        private readonly IEventAggregator eventAggregator;

        private ArticleViewModel selectedArticle;

        /// <summary>
        /// Initializes a new instance of the <see cref="SidebarViewModel"/> class.
        /// </summary>
        /// <param name="articlesRpcClient">Rpc client injection.</param>
        /// <param name="eventAggregator">Event aggregator injection.</param>
        public SidebarViewModel(IArticlesRpcClient articlesRpcClient, IEventAggregator eventAggregator)
        {
            this.articlesRpcClient = articlesRpcClient;
            this.eventAggregator = eventAggregator;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SidebarViewModel"/> class.
        /// </summary>
        public SidebarViewModel()
        {
        }

        /// <summary>
        /// Gets an observable collection of articles.
        /// </summary>
        public ObservableCollection<ArticleViewModel> Articles { get; } = new ObservableCollection<ArticleViewModel>();

        /// <summary>
        /// Gets or sets selected Article.
        /// </summary>
        public ArticleViewModel SelectedArticle
        {
            get => this.selectedArticle;
            set
            {
                this.selectedArticle = value;
                this.OnPropertyChanged();

                if (this.selectedArticle != null)
                {
                    this.eventAggregator.GetEvent<OpenArticleDetailEvent>().Publish(this.selectedArticle.Id);
                }
            }
        }

        /// <summary>
        /// Populate MainView model with data from API.
        /// </summary>
        public void OnLoad()
        {
            var result = this.articlesRpcClient.ListArticles();

            this.Articles.Clear();

            foreach (var article in result)
            {
                this.Articles.Add(new ArticleViewModel(article));
            }
        }
    }
}
