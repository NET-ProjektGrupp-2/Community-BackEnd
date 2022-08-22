using System.ComponentModel.DataAnnotations;

namespace Community_BackEnd.Data
{
    public class  Login
    {
        [Required(ErrorMessage = "User Name is required")]
        public string Username { get; set; } 

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
    }
}
