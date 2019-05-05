using System;

namespace ShoutOut.Domain.Models
{
	public class Post : IPost
	{
		public string Id { get; }

		public string AuthorId { get; }

		public string Author { get; }

		public string Title { get; }

		public string Message { get; }

		public DateTime? Created { get; }

		public DateTime? Updated { get; }

		public Post(string id, string authorId, string author, string title, string message, DateTime? created, DateTime? updated)
		{
			Id = id;
			AuthorId = authorId;
			Author = author;
			Title = title;
			Message = message;
			Created = created;
			Updated = updated;
		}

	}
}
