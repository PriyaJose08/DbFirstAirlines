using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DbFirstAirlines.Models
{
    public partial class UsersDb
    {
        [Display(Name = "Title")]
        [Required(ErrorMessage = "Title required")]
        [MinLength(2, ErrorMessage = "Min 2 characters"), MaxLength(3, ErrorMessage = "Max 3 characters")]
        public string? Title { get; set; }

        [Display(Name = "First Name")]
        [Required(ErrorMessage = "FirstName required")]
        [MinLength(3, ErrorMessage = "Min 3 characters"), MaxLength(10, ErrorMessage = "Max 10 characters")]
        public string? FirstName { get; set; }
        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "LastName required")]
        [MinLength(3, ErrorMessage = "Min 3 characters"), MaxLength(10, ErrorMessage = "Max 10 characters")]
        public string? LastName { get; set; }
        [Display(Name = "Email ID")]
        [Required(ErrorMessage = "Email required")]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail id is not valid")]
        [MinLength(15, ErrorMessage = "Min 15 characters"), MaxLength(30, ErrorMessage = "Max 30 characters")]
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "password required")]
        [DataType(DataType.Password)]
        [MinLength(8, ErrorMessage = "Min 8 characters"), MaxLength(10, ErrorMessage = "Max 10 characters")]
        public string? Password { get; set; }
        [Display(Name = "Date Of Birth")]
        [Required(ErrorMessage = "Date Of Birth required")]
        [DataType(DataType.Date)]
        public DateTime? DateOfBirth { get; set; }
        [Display(Name = "Phone Number")]
        [Required(ErrorMessage = "Phone Number required"), RegularExpression(@"^([0-9]{10})$", ErrorMessage = "Phone Number is not valid")]
        [StringLength(10, ErrorMessage = "Max 10 characters")]
        public string? PhoneNumber { get; set; }
        public int Userid { get; set; }
    }
}
