using System.ComponentModel.DataAnnotations;

namespace LearningAuth.Models
{
    public class LoginViewModel
    {
        [Required]
        public string Email { get; set; }


        [Required(ErrorMessage ="The password field cannot be empty")]
        [DataType(DataType.Password)]
        
        public string Password { get; set; }


        public bool RememberMe { get; set; }
    }
}
