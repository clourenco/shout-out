using ShoutOut.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoutOut.Domain.Repositories
{
	public interface IPostRepository
	{
		void Create(Post item);

		Post Get(string id, string authorId);

		ICollection<Post> GetAll();

		void Update(string id, string authorId, Post item);

		bool Delete(string id, string authorId);
	}
}
