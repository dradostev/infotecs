﻿using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Infotecs.Articles.Client.Rpc.Dto;
using Infotecs.Articles.Client.Rpc.Services;
using Infotecs.Articles.Client.Wpf.Events;
using Prism.Commands;
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
            this.eventAggregator.GetEvent<ArticleSavedEvent>().Subscribe(OnArticleSaved);
            this.eventAggregator.GetEvent<ArticleDeletedEvent>().Subscribe(OnArticleDeleted);
            this.CreateArticleCommand = new DelegateCommand(this.OnCreateArticle, () => true);
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
        /// Gets create Article command.
        /// </summary>
        public ICommand CreateArticleCommand { get; }

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
        public async void OnLoad()
        {
            var result = await this.articlesRpcClient.ListArticlesAsync();

            this.Articles.Clear();

            foreach (var article in result)
            {
                this.Articles.Add(new ArticleViewModel(article));
            }
        }
        
        private void OnCreateArticle()
        {
            this.eventAggregator.GetEvent<OpenCreateArticleViewEvent>().Publish();
        }
        
        private void OnArticleSaved(ArticleDto article)
        {
            this.Articles.Add(new ArticleViewModel(article));
        }
        
        private void OnArticleDeleted(long articleId)
        {
            var deletedArticle = this.Articles.First(x => x.Id == articleId);
            this.Articles.Remove(deletedArticle);
        }
    }
}
