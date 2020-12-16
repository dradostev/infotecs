using System.Windows;
using Grpc.Core;
using Infotecs.Articles.Client.Rpc.Services;
using Infotecs.Articles.Client.Wpf.Events;
using Prism.Events;

namespace Infotecs.Articles.Client.Wpf.ViewModels
{
    /// <summary>
    /// Single Article details view model.
    /// </summary>
    public class ArticleDetailViewModel : BaseViewModel
    {
        private readonly IArticlesRpcClient articlesRpcClient;

        private readonly IEventAggregator eventAggregator;

        private ArticleViewModel article;

        /// <summary>
        /// Initializes a new instance of the <see cref="ArticleDetailViewModel"/> class.
        /// </summary>
        /// <param name="articlesRpcClient">gRPC client injection.</param>
        /// <param name="eventAggregator">Event aggregator injection.</param>
        public ArticleDetailViewModel(IArticlesRpcClient articlesRpcClient, IEventAggregator eventAggregator)
        {
            this.articlesRpcClient = articlesRpcClient;
            this.eventAggregator = eventAggregator;
            this.eventAggregator.GetEvent<OpenArticleDetailEvent>().Subscribe(this.OnOpenArticleDetail);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ArticleDetailViewModel"/> class.
        /// </summary>
        public ArticleDetailViewModel()
        {
        }
        
        /// <summary>
        /// Gets article ViewModel.
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
        
        /// <summary>
        /// Fetch Article by ID.
        /// </summary>
        /// <param name="articleId">Article ID.</param>
        public void Load(long articleId)
        {
            try
            {
                var articleReply = this.articlesRpcClient.ShowArticle(articleId);
                this.Article = new ArticleViewModel(articleReply);
            }
            catch (RpcException e)
            {
                MessageBox.Show(
                    $"Error fetching Article {articleId}:\n{e.Message}",
                    "Error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }
        
        private void OnOpenArticleDetail(long articleId)
        {
            this.Load(articleId);
        }
    }
}
