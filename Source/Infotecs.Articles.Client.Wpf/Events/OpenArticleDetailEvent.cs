﻿namespace Infotecs.Articles.Client.Wpf.Events
{
    using Prism.Events;

    /// <summary>
    /// Event for transferring Article ID between views.
    /// </summary>
    public class OpenArticleDetailEvent : PubSubEvent<long>
    {
    }
}
