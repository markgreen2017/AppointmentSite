using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AppointmentSite.Models
{
    public class Appointment
    {
        public int Id { get; set; }

        [Display(Name = "First Name")]
        [Required(ErrorMessage = "Please enter your first name.")]
        [StringLength(100, ErrorMessage = "The first name entered is too long. Please try again.")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "Please enter your last name.")]
        [StringLength(100, ErrorMessage = "The last name entered is too long. Please try again.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Please enter the subject of your appointment.")]
        [StringLength(100, ErrorMessage = "The subject entered is too long. Please try again.")]
        public string Subject { get; set; }

        [Display(Name = "Phone Number")]
        [StringLength(20, ErrorMessage = "The phone number entered is too long. Please try again.")]
        [Phone(ErrorMessage = "Invalid Phone Number")]
        [Required(ErrorMessage = "Please enter a valid phone number.")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Email Address")]
        [StringLength(300, ErrorMessage = "The e-mail address entered is too long. Please try again.")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [Required(ErrorMessage = "Please enter a valid email address.")]
        public string EmailAddress { get; set; }

        [Display(Name = "Date and Time")]
        [DataType(DataType.DateTime)]
        [FutureDate(ErrorMessage = "Please enter a date in the future.")]
        [Required(ErrorMessage = "Please enter a valid date and time.")]
        public DateTime StartDateTime { get; set; }

        [Display(Name = "Duration (Minutes)")]
        [Range(1, 60, ErrorMessage = "Please enter a duration between 1 and 60 minutes.")]
        [Required(ErrorMessage = "Please enter a duration.")]
        public int duration { get; set; }

        [Required(ErrorMessage = "Please enter notes about your appointment.")]
        [StringLength(1000, ErrorMessage = "The notes entered are too long. Please try again.")]
        public string Notes { get; set; }
    }

    // Validation attribute to ensure that a DateTime is a valid DateTime in the future (from submission)
    public class FutureDate : ValidationAttribute
    {
        // Returns true if the DateTime provided is greater than or equal to the current date and time
        public override bool IsValid(object value)
        {
            DateTime date = Convert.ToDateTime(value);
            return date >= DateTime.Now;
        }
    }
}
