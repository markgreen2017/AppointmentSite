using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppointmentSite.Data;
using AppointmentSite.Models;

namespace AppointmentSite
{
    public class CreateAppointmentLogic
    {
        public bool validAppointment(DateTime start, DateTime end, AppointmentSiteContext context)
        {
            //if (end.Subtract(start).TotalMinutes <= 60)
            {

            }
            return false;
        }

        public bool appointmentAvailable(DateTime start, DateTime end, AppointmentSiteContext context)
        {
            var possibleConflicts = new List<Appointments>();

            //possibleConflicts = context.Appointments.Where(a =>)
            return false;
        }
    }
}
