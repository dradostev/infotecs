using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infotecs.Articles.Server.Domain.Entities;
using Infotecs.Articles.Server.Domain.Repositories;

namespace Infotecs.Articles.Server.Database.Repostories
{
    public class ArticlesInMemoryRepository : IRepository<Article>
    {
        private readonly InMemoryDb db;

        public ArticlesInMemoryRepository(InMemoryDb db)
        {
            this.db = db;
        }
        
        public async Task<Article> Show(long entityId)
        {
            return db.Articles.FirstOrDefault(x => x.Id == entityId);
        }

        public async Task<IEnumerable<Article>> List()
        {
            return db.Articles.ToList();
        }

        public async Task<Article> Create(Article entity)
        {
            db.Articles.Add(entity);
            return entity;
        }

        public async Task Delete(long entityId)
        {
            db.Articles.Remove(db.Articles.FirstOrDefault(x => x.Id == entityId));
        }
    }
}