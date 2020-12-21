using System.Windows;
using System.Windows.Input;
using Grpc.Core;
using Infotecs.Articles.Client.Rpc.Dto;
using Infotecs.Articles.Client.Rpc.Services;
using Infotecs.Articles.Client.Wpf.Events;
using Prism.Commands;
using Prism.Events;

namespace Infotecs.Articles.Client.Wpf.ViewModels
{
    /// <summary>
    /// Article creation ViewModel.
    /// </summary>
    public class CreateArticleViewModel : BaseViewModel
    {
        private readonly IArticlesRpcClient articlesRpcClient;
        
        private readonly IEventAggregator eventAggregator;

        private ArticleViewModel article;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateArticleViewModel"/> class.
        /// </summary>
        /// <param name="articlesRpcClient">Articles RPC client injection.</param>
        /// <param name="eventAggregator">Event aggregator injection.</param>
        public CreateArticleViewModel(IArticlesRpcClient articlesRpcClient, IEventAggregator eventAggregator)
        {
            this.articlesRpcClient = articlesRpcClient;
            this.eventAggregator = eventAggregator;
            this.article = new ArticleViewModel(new ArticleDto());
            this.SaveArticleCommand = new DelegateCommand(this.OnSaveArticle);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateArticleViewModel"/> class.
        /// </summary>
        public CreateArticleViewModel()
        {
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
        
        /// <summary>
        /// Gets save Article command.
        /// </summary>
        public ICommand SaveArticleCommand { get; }
        
        private async void OnSaveArticle()
        {
            try
            {
                var newArticle = await this.articlesRpcClient.CreateArticleAsync(
                    this.Article.User,
                    this.Article.Title,
                    this.Article.Content);

                Article = new ArticleViewModel(newArticle);
                
                this.eventAggregator.GetEvent<ArticleSavedEvent>().Publish(newArticle);
                this.eventAggregator.GetEvent<OpenArticleDetailEvent>().Publish(newArticle.Id);
            }
            catch (RpcException e)
            {
                MessageBox.Show(
                    $"Error creating Article:\n{e.Message}",
                    "Error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }
    }
}
