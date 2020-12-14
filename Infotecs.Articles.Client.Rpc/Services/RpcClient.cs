using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Net.Client;
using Infotecs.Articles.Client.Rpc.Models;

namespace Infotecs.Articles.Client.Rpc.Services
{
    public class RpcClient
    {
        private const string Url = "http://localhost:5000";

        public async Task<IEnumerable<Article>> ListArticlesAsync()
        {
            using var chan = GrpcChannel.ForAddress(Url);

            var client = new Articles.ArticlesClient(chan);

            var reply = await client.ListArticlesAsync(new EmptyRequest());

            return reply.Articles.Select(x => new Article
            {
                Id = x.ArticleId,
                Title = x.Title,
                Username = x.User,
                Content = x.Content,
                Thumbnail = x.ThumbnailImage.ToByteArray(),
            });
        }
    }
}
