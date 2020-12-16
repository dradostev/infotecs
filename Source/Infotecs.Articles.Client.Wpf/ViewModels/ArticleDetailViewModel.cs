using Infotecs.Articles.Client.Rpc.Services;
using Infotecs.Articles.Client.Wpf.Events;
using Prism.Events;

namespace Infotecs.Articles.Client.Wpf.ViewModels
{
    public class ArticleDetailViewModel : BaseViewModel
    {
        private readonly IRpcClient rpcClient;
        private readonly IEventAggregator eventAggregator;

        private ArticleViewModel article;

        public ArticleDetailViewModel(IRpcClient rpcClient, IEventAggregator eventAggregator)
        {
            this.rpcClient = rpcClient;
            this.eventAggregator = eventAggregator;
            this.eventAggregator.GetEvent<OpenArticleDetailEvent>().Subscribe(this.OnOpenArticleDetail);
        }

        public ArticleDetailViewModel()
        {
        }

        private void OnOpenArticleDetail(long articleId)
        {
            this.Load(articleId);
        }

        public void Load(long articleId)
        {
            var articleReply = this.rpcClient.ShowArticle(articleId);
            this.Article = new ArticleViewModel(articleReply);
        }

        public ArticleViewModel Article
        {
            get => this.article;
            private set
            {
                this.article = value;
                this.OnPropertyChanged();
            }
        }
    }
}
