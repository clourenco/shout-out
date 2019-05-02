using System;
using System.Collections.Generic;

namespace ShoutOut.Store
{
	public interface IStore<T>
	{
		void Create(T item);

		T Get(string id);

		ICollection<T> GetAll();

		void Update(string id, T item);

		bool Delete(string id);
	}
}
