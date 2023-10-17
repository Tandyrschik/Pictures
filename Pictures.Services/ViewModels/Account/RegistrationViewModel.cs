
using System.ComponentModel.DataAnnotations;

namespace Pictures.Domain.ViewModels.Account
{
    public class RegistrationViewModel
    {
        [Required(ErrorMessage = "Enter login")]
        [MaxLength(20, ErrorMessage = "Login length should not be more than 15 characters")]
        [MinLength(3, ErrorMessage = "Login length should not be less than 3 characters")]
        public string Login { get; set; }


        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Enter password")]
        [MaxLength(20, ErrorMessage = "Password length should not be more than 15 characters")]
        [MinLength(6, ErrorMessage = "Password length should not be less than 6 characters")]
        public string Password { get; set; }


        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Enter password")]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public string PasswordConfirm { get; set; }


        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Enter email")]
        [MaxLength(30, ErrorMessage = "Email length should not be more than 30 characters")]
        public string Email { get; set; }


        [Required(ErrorMessage = "Enter name")]
        [MaxLength(20, ErrorMessage = "Name length should not be more than 20 characters")]
        public string Name { get; set; }


        [Required(ErrorMessage = "Enter surname")]
        [MaxLength(20, ErrorMessage = "Surname length should not be more than 20 characters")]
        public string Surname { get; set; }
    }
}
