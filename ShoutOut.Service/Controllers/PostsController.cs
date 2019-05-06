using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ShoutOut.Domain.Models;
using ShoutOut.Domain.Repositories;
using ShoutOut.Service.Dtos.Models;
using System.Collections.Generic;
using System;
using ShoutOut.Service.Dtos.Responses;
using ShoutOut.Service.Dtos.Responses.Interfaces;

namespace ShoutOut.Controllers
{
	[Route("api/v1/[controller]")]
	[ApiController]
	public class PostsController : ControllerBase
	{
		#region Constants 

		private const string GetHttpMethod = "GET";
		private const string PostHttpMethod = "POST";
		private const string PutHttpMethod = "PUT";
		private const string DeleteHttpMethod = "DELETE";

		private const string PostCreated = "Post item created successfully.";
		private const string PostUpdated = "Post item updated sucessfully";
		private const string PostDeleted = "Post item deleted sucessfully";
		private const string PostRetrieved = "Post item retrieved sucessfully.";
		private const string PostsRetrieved = "Post items retrieved sucessfully.";

		private const string InternalServerError = "An internal server error ocurred.";

		#endregion

		#region Fields

		private readonly IMapper mapper;
		private readonly IPostRepository store;

		#endregion

		#region Constructor

		public PostsController(IPostRepository postRepository, IMapper postMapper)
		{
			mapper = postMapper ?? throw new ArgumentNullException(nameof(postRepository));
			store = postRepository ?? throw new ArgumentNullException(nameof(postRepository));
		}

		#endregion

		#region Public methods

		/// <summary>
		/// Retrieves posts.
		/// </summary>
		/// <returns>A response with a collection of post items.</returns>
		/// <response code="200">Items sucessfully retrieved.</response>
		/// <response code="500">Internal server error.</response>
		[HttpGet]
		[ProducesResponseType(200)]
		[ProducesResponseType(500)]
		public IActionResult GetAll()
		{
			ICollection<Post> items = null;
			IItemCollectionResponse<Post> response = null;

			try
			{
				response = new ItemCollectionResponse<Post>();

				items = store.GetAll();

				SetItemCollectionResponse(response, items, PostsRetrieved);
			}
			catch (Exception ex)
			{
				response = new ItemCollectionResponse<Post>();

				SetItemCollectionResponse(response, null, InternalServerError);

				return response.ToHttpResponse();
			}

			return response.ToHttpResponse();
		}

		/// <summary>
		/// Retrieves a post by id and author id.
		/// </summary>
		/// <param name="id">Id of the post to retrieve.</param>
		/// <param name="authorId">Author id of the post to retrieve.</param>
		/// <returns>A response including the post item.</returns>
		/// <response code="200">Item sucessfully retrieved.</response>
		/// <response code="404">Item does not exist.</response>
		/// <response code="500">Internal server error.</response> 
		[HttpGet("{id}/author/{authorId}", Name = nameof(GetItem))]
		[ProducesResponseType(200)]
		[ProducesResponseType(404)]
		[ProducesResponseType(500)]
		public IActionResult GetItem(string id, string authorId)
		{
			Post item = null;
			ISingleItemResponse<Post> response = null;

			try
			{
				response = new SingleItemResponse<Post>();

				item = store.Get(id, StringifyAuthorId(authorId));

				SetSingleItemResponse(response, item, GetHttpMethod, PostRetrieved);
			}
			
			catch (Exception ex)
			{
				if (ex is InvalidOperationException)
				{
					SetSingleItemResponse(response, null, GetHttpMethod, ex.Message);
				}
				else
				{
					SetSingleItemResponse(response, null, GetHttpMethod, InternalServerError);
				}

				return response.ToHttpResponse();
			}

			return response.ToHttpResponse();
		}

		/// <summary>
		/// Creates a post.
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
		/// <param name="createDtoItem">Request model to create a post.</param>
		/// <returns>A response including the newly created post item.</returns>
		/// <response code="201">Item successfully created.</response>
		/// <response code="400">Item is null.</response>
		/// <response code="500">Internal server error.</response>
		[HttpPost]
		[ProducesResponseType(201)]
		[ProducesResponseType(400)]
		[ProducesResponseType(500)]
		public IActionResult Post([FromBody] PostCreateDto createDtoItem)
		{
			Post item = null;
			ISingleItemResponse<Post> response = null;

			try
			{
				response = new SingleItemResponse<Post>();
				
				item = mapper.Map<Post>(createDtoItem);

				store.Create(item);

				item = store.Get(item.Id, item.AuthorId);
				
				SetSingleItemResponse(response, item, PostHttpMethod, PostCreated);
			}
			catch (Exception ex)
			{
				response = new SingleItemResponse<Post>();

				SetSingleItemResponse(response, null, PostHttpMethod, InternalServerError);

				return response.ToHttpResponse();
			}

			return response.ToCreateOrUpdateHttpResponse();
		}

		// PUT api/posts/5
		/// <summary>
		/// Updates a post.
		/// </summary>
		/// <remarks>
		/// Sample request:
		///
		///     POST api/posts/{id}/author/{authorId}
		///		{
		///			"title": "Avengers",
		///			"message": "Ironman is one of the avengers."
		///		}
		///
		/// </remarks>
		/// <param name="id">Id of the post to update.</param>
		/// <param name="authorId">Author id of the post to update.</param>
		/// <param name="updateDtoItem">Request model to update a post.</param>
		/// <returns>A response with the updated post item.</returns>
		/// <response code="200">Item successfully updated.</response>
		/// <response code="400">Item is null</response>
		/// <response code="404">Item does not exist.</response>
		/// <response code="500">Internal server error.</response>
		[HttpPut("{id}/author/{authorId}")]
		[ProducesResponseType(200)]
		[ProducesResponseType(400)]
		[ProducesResponseType(404)]
		[ProducesResponseType(500)]
		public IActionResult Put(string id, string authorId, [FromBody] PostUpdateDto updateDtoItem)
		{
			ISingleItemResponse<Post> response = null;

			try
			{

				Post item = MapToModel(updateDtoItem);

				store.Update(id, StringifyAuthorId(authorId), item);

				Post updatedItem = store.Get(id, StringifyAuthorId(authorId));

				response = new SingleItemResponse<Post>();
				response = SetSingleItemResponse(response, updatedItem, PutHttpMethod, PostUpdated);
			}
			catch (Exception ex)
			{
				if (ex is ArgumentException || ex is ArgumentNullException || ex is InvalidOperationException)
				{
					response = new SingleItemResponse<Post>();
					response = SetSingleItemResponse(response, null, PutHttpMethod, ex.Message);
				}
				else
				{
					response = new SingleItemResponse<Post>();
					response = SetSingleItemResponse(response, null, PutHttpMethod, InternalServerError);
				}
				
				return response.ToHttpResponse();
			}

			return response.ToCreateOrUpdateHttpResponse();
		}

		/// <summary>
		/// Deletes a post.
		/// </summary>
		/// <param name="id">The id of the post to delete.</param>
		/// <param name="authorId">The author id of the post to delete.</param>
		/// <returns>A response indicating if the delete was successful.</returns>
		/// <response code="200">Item deleted successfully.</response>
		/// <response code="404">Item does not exist.</response>
		/// <response code="500">Internal server error.</response>
		[HttpDelete("{id}/author/{authorId}")]
		[ProducesResponseType(200)]
		[ProducesResponseType(404)]
		[ProducesResponseType(500)]
		public IActionResult Delete(string id, string authorId)
		{
			ISimpleResponse response = null;
			
			try
			{
				store.Delete(id, StringifyAuthorId(authorId));

				response = new SimpleResponse();

				SetSimpleResponse(response, PostDeleted);
			}
			catch (Exception ex)
			{
				if (ex is InvalidOperationException)
				{
					response = new SimpleResponse();

					SetSimpleResponse(response, ex.Message);

					return response.ToHttpResponse();
				}
				else
				{
					response = new SimpleResponse();

					SetSimpleResponse(response, InternalServerError);

					return response.ToHttpResponse();
				}
				
			}

			return response.ToHttpResponse();
		}

		#endregion

		#region Private methods

		private Post MapToModel(PostUpdateDto updateDtoItem)
		{
			return mapper.Map<Post>(updateDtoItem);
		}

		private string StringifyAuthorId(string authorId)
		{
			return authorId.Replace("@", "at").Replace(".", "dot");
		}

		private string GenerateId()
		{
			return Guid.NewGuid().ToString("N");
		}

		private string GetPostUrl(Post item)
		{
			return String.Format("{0}://{1}{2}/{3}/{4}/{5}",
					Request.Scheme.ToString(),
					Request.Host.ToString(),
					Request.Path.ToString(),
					item.Id,
					"author",
					StringifyAuthorId(item.AuthorId));
		}

		private ISimpleResponse SetSimpleResponse(ISimpleResponse response, string message = "")
		{
			response.ErrorRaised = message == InternalServerError;
			response.Message = message;

			return response;
		}

		private ISingleItemResponse<Post> SetSingleItemResponse(ISingleItemResponse<Post> response, Post model, string httpMethod, string message = "")
		{
			if (model != null)
			{
				response.Model = model;
				response.Route = httpMethod == "POST" ? GetPostUrl(model) : "";
			}
			else
			{
				response.ErrorRaised = message == InternalServerError;
			}

			response.Message = message;

			return response;
		}

		private IItemCollectionResponse<Post> SetItemCollectionResponse(IItemCollectionResponse<Post> response, ICollection<Post> model, string message = "")
		{
			if (model != null)
			{
				response.Model = model;
			}
			else
			{
				response.ErrorRaised = true;
			}

			response.Message = message;

			return response;
		}

		#endregion
	}

}
