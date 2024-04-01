using Microsoft.Identity.Client;

namespace BookShop.Repository.IRepository
{
    public interface IUnitOfWork
    {
        public ICategoryRepository CategoryRepository { get; }
        public IBookRepository BookRepository { get; }
        void Save();

    }
}
