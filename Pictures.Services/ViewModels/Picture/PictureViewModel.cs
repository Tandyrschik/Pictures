
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Pictures.Domain.ViewModels.Picture
{
	public class PictureViewModel
	{
        public string Address { get; set; } = "No Address";

		[Required(ErrorMessage = "Set a name of the picture.")]
        [MaxLength(15, ErrorMessage = "Name length should not be more than 15 characters.")]
        [MinLength(1, ErrorMessage = "Name length should not be less than 6 characters.")]
        public string Name { get; set; }

		public int AccountId { get; set; }

        [Required(ErrorMessage = "Select a image.")]
        public IFormFile ImageFile { set; get; }
    }
}
