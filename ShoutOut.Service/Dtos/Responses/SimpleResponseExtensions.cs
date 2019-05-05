using Microsoft.AspNetCore.Mvc;
using ShoutOut.Service.Dtos.Responses.Interfaces;
using System.Net;

namespace ShoutOut.Service.Dtos.Responses
{
	public static class SimpleResponseExtensions
	{
		public static IActionResult ToHttpResponse(this ISimpleResponse response)
			=> new ObjectResult(response)
			{
				StatusCode = (int)(response.ErrorRaised ? HttpStatusCode.InternalServerError : HttpStatusCode.OK)
			};

		public static IActionResult ToHttpResponse<TModel>(this ISingleItemResponse<TModel> response)
		{
			var status = HttpStatusCode.OK;

			if (response.ErrorRaised)
				status = HttpStatusCode.InternalServerError;
			else if (response.Model == null)
				status = HttpStatusCode.NotFound;

			return new ObjectResult(response)
			{
				StatusCode = (int)status
			};
		}

		public static IActionResult ToHttpResponse<TModel>(this IItemCollectionResponse<TModel> response)
		{
			var status = HttpStatusCode.OK;

			if (response.ErrorRaised)
				status = HttpStatusCode.InternalServerError;
			else if (response.Model == null)
				status = HttpStatusCode.NoContent;

			return new ObjectResult(response)
			{
				StatusCode = (int)status
			};
		}

		public static IActionResult ToCreatedHttpResponse<TModel>(this ISingleItemResponse<TModel> response)
		{
			var status = HttpStatusCode.Created;

			if (response.ErrorRaised)
				status = HttpStatusCode.InternalServerError;
			else if (response.Model == null)
				status = HttpStatusCode.NotFound;

			return new ObjectResult(response)
			{
				StatusCode = (int)status
			};
		}
	}
}
