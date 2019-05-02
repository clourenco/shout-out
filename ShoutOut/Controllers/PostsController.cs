using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using ShoutOut.Domain.Models;
using ShoutOut.Dtos;
using ShoutOut.Store;
using System;
using System.Collections.Generic;


namespace ShoutOut.Controllers
{
	[Route("api/v1/[controller]")]
	[ApiController]
	public class PostsController : ControllerBase
	{
		private IMemoryCache cache;
		private PostStore store = null;

		public PostsController(IMemoryCache memoryCache)
		{
			//if (store == null)
			//{
			//	store = new PostStore();
			//}

			cache = memoryCache;

			if (!cache.TryGetValue("_PostStore", out store))
			{
				store = new PostStore();

				var cacheEntryOptions = new MemoryCacheEntryOptions();
				cacheEntryOptions.SetSlidingExpiration(TimeSpan.FromSeconds(28800));
				cacheEntryOptions.AbsoluteExpiration = DateTime.MaxValue;

				cache.Set("_PostStore", store, cacheEntryOptions);
			}
			else
			{
				store = (PostStore)cache.Get("_PostStore");
			}
		}

		// GET api/posts
		[HttpGet]
		//[Route("[Action]")]
		public ActionResult<ICollection<Post>> GetAll()
		{
			//return new string[] { $"I'm post one with id {Guid.NewGuid()}.", $"I'm post two with id {Guid.NewGuid()}." };

			ICollection<Post> items = store.GetAll();
			return Ok(items);
		}

		// GET api/posts/5
		[HttpGet("{id}/author/{authorId}", Name = nameof(GetItem))]
		public ActionResult<Post> GetItem(string id, string authorId)
		{
			return store.Get(id, StringifyAuthorId(authorId));
		}

		/// <summary>
		/// Creates a post item.
		/// </summary>
		/// <remarks>
		/// Sample request:
		///
		///     POST api/posts
		///		{
		///			"authorId": "ironman@mail.com",
		///			"author": "Tony Stark",
		///			"title": "Ironman",
		///			"message": "Tony Stark is ironman."
		///		}
		///
		/// </remarks>
		/// <param name="post"></param>
		/// <returns>A newly created post item.</returns>
		/// <response code="201">The item was successfully created.</response>
		/// <response code="400">The item is null.</response>
		[HttpPost]
		[ProducesResponseType(201)]
		[ProducesResponseType(400)]
		public ActionResult<Post> Post([FromBody] PostCreateDto post)
		{
			Post item = new Post();

			CreateMapToModel(post, item);
			store.Create(item);

			return CreatedAtRoute(nameof(GetItem), new { id = item.Id, authorId = StringifyAuthorId(item.AuthorId) }, item);
		}

		

		// PUT api/posts/5
		[HttpPut("{id}/author/{authorId}")]
		public ActionResult<string> Put(string id, string authorId, [FromBody] PostUpdateDto post)
		{
			//return $"Put successful: {post.TheMessage}";

			store.Update(id, StringifyAuthorId(authorId), post.Title, post.Message);
			Post updatedItem = store.Get(id, StringifyAuthorId(authorId));

			return Ok(updatedItem);
		}

		// DELETE api/posts/5
		[HttpDelete("{id}/author/{authorId}")]
		public ActionResult<string> Delete(string id, string authorId)
		{
			store.Delete(id, StringifyAuthorId(authorId));
			return NoContent();
		}

		private void CreateMapToModel(PostCreateDto dto, Post model)
		{
			model.Id = Guid.NewGuid().ToString("N");
			model.AuthorId = StringifyAuthorId(dto.AuthorId);
			model.Author = dto.Author;
			model.Title = dto.Title;
			model.Message = dto.Message;
			model.Created = model.Updated = DateTime.Now;
		}

		//private void UpdateMapToModel(PostUpdateDto dto, Post model)
		//{
		//	model.Title = dto.Title;
		//	model.Message = dto.Message;
		//	model.Updated = DateTime.Now;
		//}

		private string StringifyAuthorId(string authorId)
		{
			return authorId.Replace("@", "at").Replace(".", "dot");
		}
	}

	public class ThePost
	{
		public string TheMessage { get; set; }
	}
}
