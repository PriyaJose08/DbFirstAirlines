using System.ComponentModel.DataAnnotations;

namespace DbFirstAirlines.Models
{
    public class Login
    {
        
        
        [Required(ErrorMessage = "Email Required")]
        [Display(Name = "User Email")]
        public string Emailid { get; set; }
        [Required(ErrorMessage = "Password Required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
