namespace Infotecs.Articles.Client.Wpf.ViewModels
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Windows.Controls;
    using System.Windows.Input;
    using Infotecs.Articles.Client.Rpc.Models;
    using Infotecs.Articles.Client.Rpc.Services;
    using Infotecs.Articles.Client.Wpf.Pages;

    /// <summary>
    /// Main Window view model.
    /// </summary>
    public class MainViewModel : BaseViewModel
    {
        private Page currentPage;

        private ArticleViewModel selectedArticle;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainViewModel"/> class.
        /// </summary>
        public MainViewModel()
        {
            this.CurrentPage = this.StartPage;
        }

        /// <summary>
        /// Gets starting page reference.
        /// </summary>
        public Page StartPage { get; } = new StartPage();

        /// <summary>
        /// Gets article page reference.
        /// </summary>
        public Page ArticlePage { get; } = new ArticlePage();

        /// <summary>
        /// Gets create article page reference.
        /// </summary>
        public Page CreateArticlePage { get; } = new CreateArticlePage();

        /// <summary>
        /// Gets or sets currently selected page.
        /// </summary>
        public Page CurrentPage
        {
            get => this.currentPage;
            set
            {
                this.currentPage = value;
                this.OnPropertyChanged(nameof(this.CurrentPage));
            }
        }

        /// <summary>
        /// Gets an observable collection of articles.
        /// </summary>
        public ObservableCollection<ArticleViewModel> Articles { get; } = new ObservableCollection<ArticleViewModel>();

        /// <summary>
        /// Gets or sets a currently selected article.
        /// </summary>
        public ArticleViewModel SelectedArticle
        {
            get => this.selectedArticle;
            set
            {
                this.selectedArticle = value;
                this.OnPropertyChanged(nameof(this.SelectedArticle));
            }
        }

        /// <summary>
        /// Gets open start page relay command.
        /// </summary>
        public ICommand OpenStartPageCommand =>
            new RelayCommand(x => true, x => this.CurrentPage = this.StartPage);

        /// <summary>
        /// Gets open article page relay command.
        /// </summary>
        public ICommand OpenArticlePageCommand =>
            new RelayCommand(x => true, x => this.CurrentPage = this.ArticlePage);

        /// <summary>
        /// Gets open create article page relay command.
        /// </summary>
        public ICommand OpenCreateArticlePageCommand =>
            new RelayCommand(x => true, x => this.CurrentPage = this.ArticlePage);

        public ICommand FetchDataCommand =>
            new RelayCommand(
                x => true,
                async _ =>
                {
                    var rpc = new RpcClient();
                    var result = await rpc.ListArticlesAsync();

                    foreach (var article in result)
                    {
                        this.Articles.Add(new ArticleViewModel(article));
                    }
                });
    }
}
