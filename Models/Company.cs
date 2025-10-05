using System.ComponentModel.DataAnnotations;

namespace Lab1.Models
{
    public class Company
    {

        public int Id { get; set; }

        //company name


        [Required]
        [StringLength(150)]
        [Display(Name = "Company Name")]
        public string Name { get; set; }

        //years in buisness

        [Required]
        [Display(Name = "Years In Business")]
        [Range(0, 1000, ErrorMessage = "Enter a valid number of years")]
        public int YearsInBusiness { get; set; }

        //website

        [Required]
        [Display(Name = "Website")]
        [DataType(DataType.Url)]
        public string Website { get; set; }

        //province optional

        [Display(Name = "Province")]
        [StringLength(100)]
        public string Province { get; set; } 
    }
}
