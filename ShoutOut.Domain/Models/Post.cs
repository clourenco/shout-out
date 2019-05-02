using System;

namespace ShoutOut.Domain.Models
{
	public class Post : IPost
	{
		private string id;
		private string authorId;
		private string author;
		private string title;
		private string message;
		private DateTime created;
		private DateTime updated;

		public string Id
		{
			get => id;
			set => id = value;

		}

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

		public DateTime Created
		{
			get => created;
			set => created = value;
		}

		public DateTime Updated
		{
			get => updated;
			set => updated = value;
		}
	}
}
