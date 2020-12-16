using System.Windows;
using Autofac;

namespace Infotecs.Articles.Client.Wpf
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// On application startup.
        /// </summary>
        private void App_OnStartup(object sender, StartupEventArgs e)
        {
            var container = new Bootstrapper().Bootstrap();

            var mainWindow = container.Resolve<MainWindow>();
            mainWindow.Show();
        }
    }
}
