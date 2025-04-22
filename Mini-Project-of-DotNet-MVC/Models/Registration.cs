using System.ComponentModel.DataAnnotations;

namespace Mini_Project_of_DotNet_MVC.Models
{
    public class Registration
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Full name is required")]
        [DataType(DataType.Text)]
        [RegularExpression(@"^[a-zA-Z\s]*$", ErrorMessage = "Full name should only contain letters")]
        [Display(Name = "Full Name")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "CNIC number is required")]
        [RegularExpression(@"^\d{13}$", ErrorMessage = "CNIC must be exactly 13 digits")]
        [StringLength(13, ErrorMessage = "CNIC must be 13 digits long")]
        [Display(Name = "CNIC Number")]
        public string? CNICNumber { get; set; }

        [Required(ErrorMessage = "Gender is required")]
        [Display(Name = "Gender")]
        public string Gender { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Mobile number is required")]
        [RegularExpression(@"^03\d{2}-\d{7}$", ErrorMessage = "Mobile must be in 03XX-XXXXXXX format")]
        [Display(Name = "Mobile Number")]
        public string? MobileNumber { get; set; } // Changed from int to string

        [Required(ErrorMessage = "Date of birth is required")]
        [Display(Name = "Date of Birth")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DateOfBirth { get; set; } // Changed to DateTime type

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        public string? Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string? ConfirmPassword { get; set; }
    }
}