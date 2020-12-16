﻿namespace Infotecs.Articles.Client.Wpf
{
    using Autofac;
    using Infotecs.Articles.Client.Rpc.Services;
    using Infotecs.Articles.Client.Wpf.ViewModels;
    using Prism.Events;

    /// <summary>
    /// Dependency injection bootstrapper.
    /// </summary>
    public class Bootstrapper
    {
        /// <summary>
        /// Bootstrap DI container.
        /// </summary>
        /// <returns>Autofac DI container.</returns>
        public IContainer Bootstrap()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<EventAggregator>().As<IEventAggregator>().SingleInstance();
            builder.RegisterType<MainWindow>().AsSelf();
            builder.RegisterType<MainViewModel>().AsSelf();
            builder.RegisterType<SidebarViewModel>().AsSelf();
            builder.RegisterType<ArticleDetailViewModel>().AsSelf();
            builder.RegisterType<RpcClient>().As<IRpcClient>();

            return builder.Build();
        }
    }
}