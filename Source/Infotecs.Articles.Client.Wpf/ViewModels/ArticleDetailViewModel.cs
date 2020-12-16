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
    /// Single Article details view model.
    /// </summary>
    public class ArticleDetailViewModel : BaseViewModel
    {
        private readonly IArticlesRpcClient articlesRpcClient;

        private readonly IEventAggregator eventAggregator;

        private ArticleViewModel article;

        private CommentViewModel comment;

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
            this.AddCommentCommand = new DelegateCommand(OnAddComment);
            this.DeleteArticleCommand = new DelegateCommand(OnDeleteArticle);
            this.Comment = new CommentViewModel(new CommentDto());
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
        /// Gets new Comment ViewModel.
        /// </summary>
        public CommentViewModel Comment
        {
            get => this.comment;
            set
            {
                this.comment = value;
                this.OnPropertyChanged();
            }
        }
        
        /// <summary>
        /// Gets add Comment command.
        /// </summary>
        public ICommand AddCommentCommand { get; }
        
        /// <summary>
        /// Gets delete Article command.
        /// </summary>
        public ICommand DeleteArticleCommand { get; }
        
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
        
        private void OnAddComment()
        {
            try
            {
                var commentReply = this.articlesRpcClient.AddComment(
                    Article.Id,
                    Comment.Username,
                    Comment.Content);
                
                this.Article.Comments.Add(new CommentViewModel(commentReply));
                
                this.Comment = new CommentViewModel(new CommentDto());
            }
            catch (RpcException e)
            {
                MessageBox.Show(
                    $"Error creating Comment:\n{e.Message}",
                    "Error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }
        
        private void OnDeleteArticle()
        {
            try
            {
                this.articlesRpcClient.DeleteArticle(Article.Id);

                this.eventAggregator.GetEvent<OpenCreateArticleViewEvent>().Publish();
                this.eventAggregator.GetEvent<ArticleDeletedEvent>().Publish(Article.Id);
            }
            catch (RpcException e)
            {
                MessageBox.Show(
                    $"Error deleting Article:\n{e.Message}",
                    "Error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }
    }
}
