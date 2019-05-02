using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoutOut.Dtos
{
	public class PostUpdateDto
	{
		private string title;
		private string message;

		public string Title
		{
			get => title;
			set => title = value;
		}

		public string Message
		{
			get => message;
			set => message = value;
		}
	}
}
