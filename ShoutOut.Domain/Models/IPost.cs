using System;
using System.Collections.Generic;
using System.Text;

namespace ShoutOut.Domain.Models
{
	public interface IPost
	{
		string Id { get; }

		string AuthorId { get; }

		string Author { get; }

		string Title { get; }

		string Message { get; }

		DateTime? Created { get; }

		DateTime? Updated { get; }
	}
}
