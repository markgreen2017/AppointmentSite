using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppointmentSite.Data;
using AppointmentSite.Models;

namespace AppointmentSite
{
    public class AppointmentsManager
    {
        public static bool validAppointment(DateTime start, int duration, AppointmentSiteContext context)
        {
            if (duration <= 60 && appointmentAvailable(start, duration, context))
                return true;

            return false;
        }

        private static bool appointmentAvailable(DateTime start, int duration, AppointmentSiteContext context)
        {
            var possibleConflicts = from a in context.Appointments
                                    where !(((a.StartDateTime.AddMinutes(duration) < start))
                                    || (start.AddMinutes(duration) < a.StartDateTime))
                                    select a;


            if (possibleConflicts.FirstOrDefault() == null)
                return true;

            return false;
        }
    }
}
