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
		#region Private fields

		private ICollection<EntityPost> internalStore;
		private readonly IMapper mapper;

		#endregion

		#region Constructor

		public PostStore(IMapper postMapper, ICollection<EntityPost> store = null)
		{
			mapper = postMapper ?? throw new ArgumentNullException(nameof(postMapper));
			internalStore = store ?? new Collection<EntityPost>();
		}

		#endregion

		#region Public methods

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

		public void Create(Post post)
		{
			CheckParameter<Post>(post, nameof(post));

			EntityPost itemToAdd = mapper.Map<EntityPost>(post);
			internalStore.Add(itemToAdd);
		}

		public bool Delete(string id, string authorId)
		{
			CheckParameter<string>(id, nameof(id));
			CheckParameter<string>(authorId, nameof(authorId));

			EntityPost item = GetEntityItem(id, authorId);
			return internalStore.Remove(item);
		}

		public void Update(string id, string authorId, Post post)
		{
			CheckParameter<string>(id, nameof(id));
			CheckParameter<string>(authorId, nameof(authorId));
			CheckParameter<Post>(post, nameof(post));

			bool updated = false;
			EntityPost itemToUpdate = GetEntityItem(id, authorId);

			if (!String.IsNullOrEmpty(post.Title) && itemToUpdate.Title != post.Title)
			{
				itemToUpdate.Title = post.Title;
				updated = true;
			}

			if (!String.IsNullOrEmpty(post.Message) && itemToUpdate.Message != post.Message)
			{
				itemToUpdate.Message = post.Message;
				updated = true;
			}

			if (updated)
			{
				itemToUpdate.Updated = post.Updated ?? DateTime.Now;
			}
		}

		#endregion

		#region Private methods

		private void CheckParameter<T>(T param, string paramName)
		{
			if (param == null)
			{
				throw new ArgumentNullException(paramName);
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

		#endregion
	}
}
