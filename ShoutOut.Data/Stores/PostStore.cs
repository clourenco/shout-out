using AutoMapper;
using ShoutOut.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using ShoutOut.Data.Entities;
using ShoutOut.Domain.Models;

namespace ShoutOut.Data.Stores
{
	public class PostStore : IPostRepository
	{
		private ICollection<EntityPost> internalStore;
		private readonly IMapper mapper;

		public PostStore(IMapper postMapper)
		{
			postMapper = postMapper ?? throw new ArgumentNullException(nameof(postMapper));
			mapper = postMapper;
			internalStore = new Collection<EntityPost>();
		}

		public ICollection<Post> GetAll()
		{
			return internalStore
					.Select(p => mapper.Map<Post>(p))
						.ToList();
		}

		public Post Get(string id, string authorId)
		{
			return mapper.Map<Post>(
					GetEntityItem(id, authorId));
		}

		public void Create(Post item)
		{
			EntityPost itemToAdd = mapper.Map<EntityPost>(item);
			internalStore.Add(itemToAdd);
		}

		public bool Delete(string id, string authorId)
		{
			EntityPost item = GetEntityItem(id, authorId);
			return internalStore.Remove(item);
		}

		public void Update(string id, string authorId, Post item)
		{
			if (!Exists(id, authorId))
			{
				throw new InvalidOperationException($"The post with id {id} belonging to the author {authorId} does not exist.");
			}

			bool updated = false;
			EntityPost itemToUpdate = GetEntityItem(id, authorId);

			if (!String.IsNullOrEmpty(item.Title) && itemToUpdate.Title != item.Title)
			{
				itemToUpdate.Title = item.Title;
				updated = true;
			}

			if (!String.IsNullOrEmpty(item.Message) && itemToUpdate.Message != item.Message)
			{
				itemToUpdate.Message = item.Message;
				updated = true;
			}

			if (updated)
			{
				itemToUpdate.Updated = item.Updated ?? DateTime.Now;
			}
		}

		private bool Exists(string id, string authorId)
		{
			return internalStore.Any(p => p.Id == id && p.AuthorId == authorId);
		}

		private EntityPost GetEntityItem(string id, string authorId)
		{
			if (!Exists(id, authorId))
			{
				throw new InvalidOperationException($"The post with id {id} belonging to the author {authorId} does not exist.");
			}

			return internalStore.First(p => p.Id == id && p.AuthorId == authorId);
		}

	}
}
