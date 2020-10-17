using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AppointmentSite.Models
{
    public class Appointments
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter your name.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter the subject of your appointment.")]
        public string Subject { get; set; }

        [Display(Name = "Phone Number")]
        [Phone(ErrorMessage = "Invalid Phone Number")]
        [Required(ErrorMessage = "Please enter a valid phone number.")]
        public int PhoneNumber { get; set; }

        [Display(Name = "Email Address")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [Required(ErrorMessage = "Please enter a valid email address.")]
        public string EmailAddress { get; set; }

        [Display(Name = "Date and Time")]
        [DataType(DataType.DateTime)]
        [Required(ErrorMessage = "Please enter a valid date and time.")]
        public DateTime AppointmentDateTime { get; set; }

        public string Notes { get; set; }
    }
}
