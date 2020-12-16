using Infotecs.Articles.Client.Rpc.Dto;
using Prism.Events;

namespace Infotecs.Articles.Client.Wpf.Events
{
    /// <summary>
    /// Article saved event.
    /// </summary>
    public class ArticleSavedEvent : PubSubEvent<ArticleDto>
    {
    }
}
