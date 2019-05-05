using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoutOut.Service.Dtos.Responses.Interfaces
{
	public sealed class SimpleResponse : ISimpleResponse
	{
		public string Message { get; set; }
		public bool ErrorRaised { get; set; }
		public string ErrorMessage { get; set; }
	}
}
