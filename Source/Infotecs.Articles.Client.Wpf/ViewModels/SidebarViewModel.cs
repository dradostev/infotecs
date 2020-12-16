namespace Infotecs.Articles.Client.Wpf.ViewModels
{
    using System.Collections.ObjectModel;
    using Infotecs.Articles.Client.Rpc.Models;
    using Infotecs.Articles.Client.Rpc.Services;
    using Infotecs.Articles.Client.Wpf.Events;
    using Prism.Events;

    /// <summary>
    /// ViewModel for Articles in sidebar.
    /// </summary>
    public class SidebarViewModel : BaseViewModel
    {
        private readonly IRpcClient rpcClient;

        private readonly IEventAggregator eventAggregator;

        private ArticleViewModel selectedArticle;

        /// <summary>
        /// Initializes a new instance of the <see cref="SidebarViewModel"/> class.
        /// </summary>
        /// <param name="rpcClient">Rpc client injection.</param>
        /// <param name="eventAggregator">Event aggregator injection.</param>
        public SidebarViewModel(IRpcClient rpcClient, IEventAggregator eventAggregator)
        {
            this.rpcClient = rpcClient;
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
            var result = this.rpcClient.ListArticles();

            this.Articles.Clear();

            foreach (var article in result)
            {
                this.Articles.Add(new ArticleViewModel(article));
            }
        }
    }
}
