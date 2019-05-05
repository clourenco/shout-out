using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoutOut.Service.Dtos.Responses.Interfaces
{
	public interface ISingleItemResponse<TModel> : ISimpleResponse
	{
		TModel Model { get; set; }
		string Route { get; set; }

	}
}

