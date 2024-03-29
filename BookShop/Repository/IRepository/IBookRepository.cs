using BookShop.Models;

namespace BookShop.Repository.IRepository
{
	public interface IBookRepository:IRepository<Book>
	{
		void Update(Book book);
	}
}
