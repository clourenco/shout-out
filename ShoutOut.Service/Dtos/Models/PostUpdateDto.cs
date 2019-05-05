using System.ComponentModel.DataAnnotations;

namespace ShoutOut.Service.Dtos.Models
{
	public class PostUpdateDto
	{
		[Required]
		[StringLength(100, ErrorMessage = "The title cannot be empty and should have a maximum of 100 characters long.", MinimumLength = 1)]
		public string Title { get; set; }

		[Required]
		[StringLength(1000, ErrorMessage = "The message cannot be empty and should have a maximum of 1000 characters long.", MinimumLength = 1)]
		public string Message { get; set; }
	}
}
