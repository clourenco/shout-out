using System;
using System.Collections.Generic;
using System.Text;

namespace ShoutOut.Domain.Models
{
	public interface IPost
	{
		string Id { get; set; }

		string AuthorId { get; set; }

		string Author { get; set; }

		string Title { get; set; }

		string Message { get; set; }

		DateTime Created { get; set; }

		DateTime Updated { get; set; }
	}
}
