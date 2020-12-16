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

        /// <summary>
        /// Initializes a new instance of the <see cref="MainViewModel"/> class.
        /// </summary>
        /// <param name="sidebarViewModel">Sidebar ViewModel.</param>
        /// <param name="articleDetailViewModel">Article details ViewModel.</param>
        public MainViewModel(SidebarViewModel sidebarViewModel, ArticleDetailViewModel articleDetailViewModel)
        {
            this.SidebarViewModel = sidebarViewModel;
            this.ArticleDetailViewModel = articleDetailViewModel;
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
        /// Gets or sets currently selected page.
        /// </summary>
        public Page CurrentPage
        {
            get => this.currentPage;
            set
            {
                this.currentPage = value;
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
    }
}
