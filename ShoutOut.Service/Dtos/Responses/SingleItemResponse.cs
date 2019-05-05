using ShoutOut.Service.Dtos.Responses.Interfaces;
using System;

namespace ShoutOut.Service.Dtos.Responses
{
	public sealed class SingleItemResponse<TModel> : ISingleItemResponse<TModel>
	{
		public TModel Model { get; set; }
		public string Route { get; set; }
		public string Message { get; set; }
		public bool ErrorRaised { get; set; }
		public string ErrorMessage { get; set; }
	}
}
