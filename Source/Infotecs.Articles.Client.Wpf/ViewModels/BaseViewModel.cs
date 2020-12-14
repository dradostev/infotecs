namespace Infotecs.Articles.Client.Wpf.ViewModels
{
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    /// <summary>
    /// Base MVVM view model to derive from.
    /// </summary>
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        /// <inheritdoc/>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// On property changed.
        /// </summary>
        /// <param name="propertyName">String name of changed property.</param>
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
