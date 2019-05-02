using ShoutOut.Domain.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace ShoutOut.Store
{
	public class PostStore : IStore<Post>
	{
		private ICollection<Post> internalStore;

		/// <summary>
		/// 
		/// </summary>
		public PostStore()
		{
			internalStore = new Collection<Post>();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="item"></param>
		public void Create(Post item)
		{
			internalStore.Add(item);
		}

		
		/// <summary>
		/// Delete
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		/// <exception cref="InvalidOperationException"></exception>
		public bool Delete(string id)
		{
			Post itemToDelete = Get(id);

			return internalStore.Remove(itemToDelete);
		}

		
		/// <summary>
		/// Delete
		/// </summary>
		/// <param name="id"></param>
		/// <param name="authorId"></param>
		/// <returns></returns>
		/// <exception cref="InvalidOperationException"></exception>
		public bool Delete(string id, string authorId)
		{
			Post itemToDelete = Get(id, authorId);

			return internalStore.Remove(itemToDelete);
		}

		/// <summary>
		/// Get
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		/// <exception cref="InvalidOperationException"></exception>
		public Post Get(string id)
		{
			if (!Exists(id))
			{
				throw new InvalidOperationException($"The post with id {id} does not exist.");
			}

			return internalStore.First(p => p.Id == id);
		}

		/// <summary>
		/// Get
		/// </summary>
		/// <param name="id"></param>
		/// <param name="authorId"></param>
		/// <returns></returns>
		/// <exception cref="InvalidOperationException"></exception>
		public Post Get(string id, string authorId)
		{
			if (!Exists(id, authorId))
			{
				throw new InvalidOperationException($"The post with id {id} belonging to the author {authorId} does not exist.");
			}

			return internalStore.First(p => p.Id == id && p.AuthorId == authorId);
		}

		public ICollection<Post> GetAll()
		{
			return internalStore;
		}

		/// <summary>
		/// Update
		/// </summary>
		/// <param name="id"></param>
		/// <param name="item"></param>
		/// <exception cref="InvalidOperationException"></exception>
		public void Update(string id, Post item)
		{
			throw new NotImplementedException();
			//if (!Exists(id, item.AuthorId))
			//{
			//	throw new InvalidOperationException($"The post with id {id} does not exist.");
			//}

			//Post itemToUpdate = Get(id, item.AuthorId);
			//itemToUpdate.Title = item.Title;
			//itemToUpdate.Message = item.Message;
		}

		public void Update(string id, string authorId, string title, string message)
		{
			if (!Exists(id, authorId))
			{
				throw new InvalidOperationException($"The post with id {id} belonging to the author {authorId} does not exist.");
			}

			Post itemToUpdate = Get(id, authorId);
			itemToUpdate.Title = title;
			itemToUpdate.Message = message;
			itemToUpdate.Updated = DateTime.Now;
		}

		/// <summary>
		/// Exists
		/// </summary>
		/// <param name="id"></param>
		/// <param name="authorId"></param>
		/// <returns></returns>
		private bool Exists(string id, string authorId = "")
		{
			bool retVal = false;

			if (!String.IsNullOrEmpty(authorId))
			{
				retVal = internalStore.Any(p => p.Id == id && p.AuthorId == authorId);
			}
			else
			{
				retVal = internalStore.Any(p => p.Id == id);
			}

			return retVal;
		}

	}
}
