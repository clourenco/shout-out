using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoutOut.Service.Dtos.Responses.Interfaces
{
	public interface IItemCollectionResponse<TModel> : ISimpleResponse
	{
		ICollection<TModel> Model { get; set; }
	}
}
