using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace AdmissionCommittee.Domain.Entities
{
    public class Enrollee
    {
        [HiddenInput(DisplayValue = false)]
        public int EnrolleeID { get; set; }

        [Required(ErrorMessage = "Please enter first name")]
        [Display(Name = "First name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please enter last name")]
        [Display(Name = "Last name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Please enter the patronymic")]
        public string Patronymic { get; set; }

        [RegularExpression("([A-Z]|[a-z]){2}\\d{7}", ErrorMessage = "Please enter correct number and series of passport")]
        [Display(Name = "Number and series of passport")]
        public string PassportNumber { get; set; }

        [RegularExpression("\\d{2} \\d{2} \\d{4}", ErrorMessage = "Please enter the date in the format \"DD MM YYYY\"")]
        [Display(Name = "Date of birth (in the format DD MM YYYY)")]
        public string DateOfBirth { get; set; }


        [Range(0, 100, ErrorMessage = "Please enter number between 0 and 100")]
        [Display(Name = "CT Language")]
        public int CTLanguage { get; set; }

        [Range(0, 100, ErrorMessage = "Please enter number between 0 and 100")]
        [Display(Name = "CT First subject")]
        public int CTFirstSubject { get; set; }

        [Range(0, 100, ErrorMessage = "Please enter number between 0 and 100")]
        [Display(Name = "CT Second subject")]
        public int CTSecondSubject { get; set; }

        [Range(0, 100, ErrorMessage = "Please enter number between 0 and 100")]
        public int GPA { get; set; }

        [Required(ErrorMessage = "Please enter a city")]
        public string City { get; set; }

        [Required(ErrorMessage = "Please enter an address")]
        public string Address { get; set; }

        [RegularExpression("\\+\\d{3} \\d{2} \\d{7}", ErrorMessage = "Please enter correct phone number in the format \"+### ## #######\"")]
        [Display(Name = "Mobile phone (in format +123 45 6789012)")]
        public string Phone { get; set; }
    }
}
