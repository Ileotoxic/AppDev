﻿using System.Linq.Expressions;

namespace BookShop.Repository.IRepository
{
	public interface IRepository<T> where T : class
	{
		void Add(T entity);
		void Delete(T entity);
		IEnumerable<T> GetAll();
		T Get(Expression<Func<T, bool>> predicate);
	}
}
