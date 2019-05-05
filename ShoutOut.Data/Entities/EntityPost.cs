using System;
using System.Collections.Generic;
using System.Text;

namespace ShoutOut.Data.Entities
{
	public class EntityPost
	{
		public string Id { get; set; }

		public string AuthorId { get; set; }

		public string Author { get; set; }

		public string Title { get; set; }

		public string Message { get; set; }

		public DateTime Created { get; set; }

		public DateTime Updated { get; set; }
	}
}
