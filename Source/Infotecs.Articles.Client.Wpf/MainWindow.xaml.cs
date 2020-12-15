namespace Infotecs.Articles.Client.Wpf
{
    using System.Windows;
    using Infotecs.Articles.Client.Wpf.ViewModels;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly MainViewModel viewModel;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow"/> class.
        /// </summary>
        /// <param name="viewModel">MainViewModel to inject.</param>
        public MainWindow(MainViewModel viewModel)
        {
            this.InitializeComponent();
            this.viewModel = viewModel;
            this.DataContext = this.viewModel;
            this.Loaded += this.OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            this.viewModel.OnLoad();
        }
    }
}
