using System.Collections.ObjectModel;
using System.Windows.Input;
using Infotecs.Articles.Client.Wpf.Events;
using Prism.Events;

namespace Infotecs.Articles.Client.Wpf.ViewModels
{
    /// <summary>
    /// Main Window view model.
    /// </summary>
    public class MainViewModel : BaseViewModel
    {
        private readonly IEventAggregator eventAggregator;

        private BaseViewModel currentViewModel;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainViewModel"/> class.
        /// </summary>
        /// <param name="sidebarViewModel">Sidebar ViewModel injection.</param>
        /// <param name="articleDetailViewModel">Article details ViewModel injection.</param>
        /// <param name="createArticleViewModel">Article creation injection.</param>
        /// <param name="eventAggregator">Event aggregator injection.</param>
        public MainViewModel(
            SidebarViewModel sidebarViewModel,
            ArticleDetailViewModel articleDetailViewModel,
            CreateArticleViewModel createArticleViewModel,
            IEventAggregator eventAggregator)
        {
            this.SidebarViewModel = sidebarViewModel;
            this.ArticleDetailViewModel = articleDetailViewModel;
            this.CreateArticleViewModel = createArticleViewModel;
            this.eventAggregator = eventAggregator;
            this.eventAggregator.GetEvent<OpenCreateArticleViewEvent>().Subscribe(this.OnOpenCreateArticleView);
            this.eventAggregator.GetEvent<OpenArticleDetailEvent>().Subscribe(this.OnOpenArticleDetailView);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MainViewModel"/> class.
        /// </summary>
        public MainViewModel()
        {
        }

        /// <summary>
        /// Gets sidebar ViewModel.
        /// </summary>
        public SidebarViewModel SidebarViewModel { get; }

        /// <summary>
        /// Gets single Article details ViewModel.
        /// </summary>
        public ArticleDetailViewModel ArticleDetailViewModel { get; }
        
        /// <summary>
        /// Gets Article creation ViewModel.
        /// </summary>
        public CreateArticleViewModel CreateArticleViewModel { get; }
        
        /// <summary>
        /// Gets or sets current view ViewModel.
        /// </summary>
        public BaseViewModel CurrentViewModel
        {
            get => this.currentViewModel;
            set
            {
                this.currentViewModel = value;
                this.OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets an observable collection of articles.
        /// </summary>
        public ObservableCollection<ArticleViewModel> Articles { get; } = new ObservableCollection<ArticleViewModel>();

        /// <summary>
        /// Populate MainView model with data from API.
        /// </summary>
        public void OnLoad()
        {
            this.SidebarViewModel.OnLoad();
        }
        
        private void OnOpenCreateArticleView()
        {
            this.CurrentViewModel = CreateArticleViewModel;
        }
        
        private void OnOpenArticleDetailView(long articleId)
        {
            this.CurrentViewModel = ArticleDetailViewModel;
        }
    }
}
