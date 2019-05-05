using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoutOut.Service.Dtos.Responses.Interfaces
{
	public interface ISimpleResponse
	{
		string Message { get; set; }

		bool ErrorRaised { get; set; }

		string ErrorMessage { get; set; }
	}
}
