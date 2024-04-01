using BookShop.Data;
using BookShop.Repository.IRepository;

namespace BookShop.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public ICategoryRepository CategoryRepository { get; private set; }

        public IBookRepository BookRepository { get; private set; }
        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            CategoryRepository = new CategoryRepository(context);
            BookRepository = new BookRepository(context);
        }
        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
