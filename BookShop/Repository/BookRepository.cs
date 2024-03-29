using BookShop.Data;
using BookShop.Models;
using BookShop.Repository.IRepository;

namespace BookShop.Repository
{
	public class BookRepository:Repository<Book>,IBookRepository
	{
		private readonly ApplicationDbContext _context;
		public BookRepository(ApplicationDbContext context) : base(context)
		{
			_context = context;
		}

		public void Update(Book book)
		{
			_context.Books.Update(book);
		}
	}
}
