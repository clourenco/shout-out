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
			Id = id ?? throw new ArgumentNullException(nameof(id));

			AuthorId = authorId ?? throw new ArgumentNullException(nameof(authorId));
			AuthorId = authorId.Length < 8 || authorId.Length > 50 ? throw new ArgumentException($"{nameof(authorId)} invalid length") : authorId;

			Author = author ?? throw new ArgumentNullException(nameof(author));
			Author = author.Length < 1 || author.Length > 50 ? throw new ArgumentException($"{nameof(author)} invalid length") : author;

			Title = title ?? throw new ArgumentNullException(nameof(title));
			Title = title.Length < 1 || title.Length > 100 ? throw new ArgumentException($"{nameof(title)} invalid length") : title;

			Message = message ?? throw new ArgumentNullException(nameof(message));
			Message = message.Length < 1 || message.Length > 1000 ? throw new ArgumentException($"{nameof(message)} invalid length") : message;

			Created = created;
			Updated = updated;
		}
	}
}
