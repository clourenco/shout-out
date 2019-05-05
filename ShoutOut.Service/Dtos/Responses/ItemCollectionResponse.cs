using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoutOut.Service.Dtos.Responses.Interfaces
{
	public sealed class ItemCollectionResponse<TModel> : IItemCollectionResponse<TModel>
	{
		public ICollection<TModel> Model { get; set; }
		public string Message { get; set; }
		public bool ErrorRaised { get; set; }
		public string ErrorMessage { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
	}
}
