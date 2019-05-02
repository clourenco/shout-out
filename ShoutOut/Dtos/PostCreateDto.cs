using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoutOut.Dtos
{
	public class PostCreateDto
	{
		private string authorId;
		private string author;
		private string title;
		private string message;

		public string AuthorId
		{
			get => authorId;
			set => authorId = value;
		}

		public string Author
		{
			get => author;
			set => author = value;
		}

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
