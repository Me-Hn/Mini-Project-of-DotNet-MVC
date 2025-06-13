using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace Mini_Project_of_DotNet_MVC.Models
{
    public class VehicleRegistration
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Full Name is required")]
        public string? FullName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Date of Birth is required")]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [Required(ErrorMessage = "Phone Number is required")]
        public string? PhoneNumber { get; set; }

        [Required(ErrorMessage = "Address is required")]
        public string? Address { get; set; }

        public string? Nationality { get; set; }

       // [Required(ErrorMessage = "User Photo is required")]
        public string? UserPhotoPath { get; set; }

        //[Required(ErrorMessage = "CNIC Front Photo is required")]
        public string? CnicFrontPath { get; set; }

        //[Required(ErrorMessage = "CNIC Back Photo is required")]
        public string? CnicBackPath { get; set; }

        [Required(ErrorMessage = "Vehicle Type is required")]
        public string? VehicleType { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime ExpiryDate { get; set; } = DateTime.Now.AddYears(1);

        // These properties are not mapped to the database
        [NotMapped]
        [Required(ErrorMessage = "User Photo is required")]
        [DataType(DataType.Upload)]
        public IFormFile UserPhoto { get; set; }

        [NotMapped]
        [Required(ErrorMessage = "CNIC Front Photo is required")]
        [DataType(DataType.Upload)]
        public IFormFile CnicFront { get; set; }

        [NotMapped]
        [Required(ErrorMessage = "CNIC Back Photo is required")]
        [DataType(DataType.Upload)]
        public IFormFile CnicBack { get; set; }
    }
}