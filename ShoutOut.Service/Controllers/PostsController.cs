using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ShoutOut.Domain.Models;
using ShoutOut.Domain.Repositories;
using ShoutOut.Service.Dtos.Models;
using System.Collections.Generic;
using System;
using ShoutOut.Service.Dtos.Responses;

namespace ShoutOut.Controllers
{
	[Route("api/v1/[controller]")]
	[ApiController]
	public class PostsController : ControllerBase
	{
		//private IMemoryCache cache;
		//private PostStore store = null;
		private readonly IMapper mapper;
		private readonly IPostRepository store;

		public PostsController(IPostRepository postRepository, IMapper postMapper)
		{
			mapper = postMapper;
			store = postRepository;
		}

		//public PostsController(IMemoryCache memoryCache)
		//{
		//	//if (store == null)
		//	//{
		//	//	store = new PostStore();
		//	//}

		//	cache = memoryCache;

		//	if (!cache.TryGetValue("_PostStore", out store))
		//	{
		//		store = new PostStore();

		//		var cacheEntryOptions = new MemoryCacheEntryOptions();
		//		cacheEntryOptions.SetSlidingExpiration(TimeSpan.FromSeconds(28800));
		//		cacheEntryOptions.AbsoluteExpiration = DateTime.MaxValue;

		//		cache.Set("_PostStore", store, cacheEntryOptions);
		//	}
		//	else
		//	{
		//		store = (PostStore)cache.Get("_PostStore");
		//	}
		//}

		// GET api/posts
		[HttpGet]
		//[Route("[Action]")]
		public ActionResult<ICollection<Post>> GetAll()
		{
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
		/// <param name="createDtoItem"></param>
		/// <returns>A newly created post item.</returns>
		/// <response code="201">Item successfully created.</response>
		/// <response code="400">Item is null.</response>
		/// <response code="500">Internal server error.</response>
		[HttpPost]
		[ProducesResponseType(201)]
		[ProducesResponseType(400)]
		[ProducesResponseType(500)]
		//public ActionResult<Post> Post([FromBody] PostCreateDto createDtoItem)
		public IActionResult Post([FromBody] PostCreateDto createDtoItem)
		{
			Post item = null;
			SingleItemResponse<Post> response = null;

			try
			{
				//if (!CheckModelState())
				//{
				//	return BadRequest();
				//}

				response = new SingleItemResponse<Post>();
				item = mapper.Map<Post>(createDtoItem);

				store.Create(item);

				response.Model = store.Get(item.Id, item.AuthorId);
				response.Route = GetPostCreatedUrl(item);
				response.Message = "The item was successfully created.";
			}
			catch (Exception ex)
			{
				response = new SingleItemResponse<Post>();
				response.ErrorRaised = true;
				response.ErrorMessage = "An internal server error ocurred.";

				return response.ToHttpResponse();
			}

			return response.ToCreatedHttpResponse();
			//return CreatedAtRoute(nameof(GetItem), new { id = item.Id, authorId = StringifyAuthorId(item.AuthorId) }, item);
		}

		// PUT api/posts/5
		[HttpPut("{id}/author/{authorId}")]
		public ActionResult<string> Put(string id, string authorId, [FromBody] PostUpdateDto updateDtoItem)
		{
			Post item = mapper.Map<Post>(updateDtoItem);
			store.Update(id, StringifyAuthorId(authorId), item);
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

		private string StringifyAuthorId(string authorId)
		{
			return authorId.Replace("@", "at").Replace(".", "dot");
		}

		private bool CheckModelState()
		{
			return ModelState.IsValid;
		}

		private string GetPostCreatedUrl(Post item)
		{
			//return $"{Request.Scheme.ToString()}://{Request.Host.ToString()}{Request.Path.ToString()}{item.Id}/author/{StringifyAuthorId(item.AuthorId)}";
			return String.Format("{0}://{1}{2}/{3}/{4}/{5}",
					Request.Scheme.ToString(),
					Request.Host.ToString(),
					Request.Path.ToString(),
					item.Id,
					"author",
					StringifyAuthorId(item.AuthorId));
		}
	}

}
