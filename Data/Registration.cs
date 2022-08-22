using System.ComponentModel.DataAnnotations;

namespace Community_BackEnd.Data.Forums
{
    public class Registration
    {
        [Required(ErrorMessage = "User Name is required")]
        public string? Username { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "Email is required")]
        [Display(Name = "Email")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string? Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password confirmation password do not match. ")]
        public string ConfirmPassword { set; get; }
        [Display(Name = "Is administrator")]
        public bool IsAdministrator { get; set; }

        [Display(Name = "Is Author")]
        public bool IsManager { get; set; }

        //[Required]
        //[Display(Name = "Office Number")]
        //public int OfficeNumber { get; set; }
        //added 
        //[Required]
        //[EmailAddress]
        //[Display(Name = "Email")]
        //public string Email { get; set; }
        //[Required]
        //[StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} charcters long", MinimumLength = 8)]
        //[DataType(DataType.Password)]
        //public string Password { get; set; }
        //public bool RememberMe { get; set; }
        //[DataType(DataType.Password)]
        //[Display(Name = "Confirm password")]
        //[Compare("Password", ErrorMessage = "The password confirmation password do not match. ")]
        //public string ConfirmPassword { set; get; }
        //[Required]
        //[StringLength(20, ErrorMessage = "The {0} must be at least {2} and at max {1} charcters long", MinimumLength = 6)]
        //[Display(Name = "Username")]
        //public string UserName { set; get; }
    }
}
