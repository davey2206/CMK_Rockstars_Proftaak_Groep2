using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace CMK_Rockstars_Proftaak_Groep2.Models
{
    public class UserModel
    {
        [Required(ErrorMessage = "Firstname Required")]
        [StringLength(50, ErrorMessage = "Enter at least two characters.", MinimumLength = 2)]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Lastname Required")]
        [StringLength(50, ErrorMessage = "Enter at least two characters.", MinimumLength = 2)]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Email Required")]
        [EmailAddress(ErrorMessage = "Invalid email")]
        public string Email { get; set; }
        public string Mobile { get; set; }
        //public List<User> Users { get; set; }
    }
}
