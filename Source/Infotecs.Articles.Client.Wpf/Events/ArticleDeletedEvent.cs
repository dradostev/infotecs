using Prism.Events;

namespace Infotecs.Articles.Client.Wpf.Events
{
    /// <summary>
    /// Event after deleting Article.
    /// </summary>
    public class ArticleDeletedEvent : PubSubEvent<long>
    {
    }
}
