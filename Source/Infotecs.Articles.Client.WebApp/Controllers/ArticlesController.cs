using Infotecs.Articles.Client.Rpc.Services;
using Microsoft.AspNetCore.Mvc;

namespace Infotecs.Articles.Client.WebApp.Controllers
{
    /// <summary>
    /// Articles CRUD HTTP controller.
    /// </summary>
    [ApiController]
    [Route("articles")]
    public class ArticlesController : Controller
    {
        private readonly IArticlesRpcClient rpcClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="ArticlesController"/> class.
        /// </summary>
        /// <param name="rpcClient">RPC client injection.</param>
        public ArticlesController(IArticlesRpcClient rpcClient)
        {
            this.rpcClient = rpcClient;
        }
    }
}
