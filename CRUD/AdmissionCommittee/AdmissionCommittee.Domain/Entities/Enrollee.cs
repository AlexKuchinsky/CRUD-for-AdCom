using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.Linq;

namespace AdmissionCommittee.Domain.Entities
{
    public class Enrollee
    {
        [HiddenInput(DisplayValue = false)]
        public int EnrolleeId { get; set; }

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

        [Display(Name = "Date of birth")]
        public DateTime DateOfBirth { get; set; }

        [Range(0, 100, ErrorMessage = "Please enter number between 0 and 100")]
        [Display(Name = "CT Language")]
        public virtual int CTLanguage { get; set; }

        [Range(0, 100, ErrorMessage = "Please enter number between 0 and 100")]
        [Display(Name = "CT First subject")]
        public virtual int CTFirstSubject { get; set; }

        [Required]
        [Range(0, 100, ErrorMessage = "Please enter number between 0 and 100")]
        [Display(Name = "CT Second subject")]
        public virtual int CTSecondSubject { get; set; }

        public virtual IList<EnrolleeToSubject> Marks { get; set; }

        [RegularExpression("\\+\\d{3} \\d{2} \\d{7}", ErrorMessage = "Please enter correct phone number in the format \"+### ## #######\"")]
        [Display(Name = "Mobile phone (in format +123 45 6789012)")]
        public string Phone { get; set; }

        [Display(Name = "Education level")]
        public EducationLevel EducationLevel { get; set; }

        //public UniversitySpecialty UniversitySpecialty { get; set; }

        public Enrollee()
        {
            Marks = new List<EnrolleeToSubject>();
        }

        public virtual Address Address { get; set; }

        public int GetCTSum()
        {
            return CTLanguage + CTFirstSubject + CTSecondSubject + (int)(GetGPA()*10);
        }

        public double GetGPA()
        {
            return Marks.Select(subject => subject.Mark).Sum() /
                (double)Marks.Count;
        }
    }
}
