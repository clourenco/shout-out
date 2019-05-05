using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoutOut.Service.Dtos.Models
{
	public class PostCreateDto
	{
		public string AuthorId { get; set; }

		public string Author { get; set; }

		public string Title { get; set; }

		public string Message { get; set; }
	}
}
