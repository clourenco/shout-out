using System.ComponentModel.DataAnnotations;

namespace ShoutOut.Service.Dtos.Models
{
	public class PostCreateDto
	{
		[Required]
		[StringLength(50, ErrorMessage = "The author id should have between 8 and 50 characters long.", MinimumLength = 8)]
		public string AuthorId { get; set; }

		[Required]
		[StringLength(50, ErrorMessage = "The author cannot be empty and should have a maximum of 50 characters long.", MinimumLength = 1)]
		public string Author { get; set; }

		[Required]
		[StringLength(100, ErrorMessage = "The title cannot be empty and should have a maximum of 100 characters long.", MinimumLength = 1)]
		public string Title { get; set; }

		[Required]
		[StringLength(1000, ErrorMessage = "The message cannot be empty and should have a maximum of 1000 characters long.", MinimumLength = 1)]
		public string Message { get; set; }
	}
}
