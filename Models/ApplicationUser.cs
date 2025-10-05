using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Lab1.Models
{
    public class ApplicationUser : IdentityUser
    {
        //First Name
        [Required]
        [Display(Name = "First Name")]
        [StringLength(50)]
        public string FirstName { get; set; }

        //First Name
        [Required]
        [Display(Name = "Last Name")]
        [StringLength(50)]
        public string LastName { get; set; }

        // City is optional per requirements
        [Display(Name = "City")]
        [StringLength(100)]
        public string City { get; set; }
    }
}
