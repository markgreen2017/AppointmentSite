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
        private readonly AppointmentSiteContext _context;

        public AppointmentsManager(AppointmentSiteContext context)
        {
            _context = context;
        }

        public bool CreateAppointment(Appointment appointment)
        {
            // If the appointment is valid, return true, otherwise false
            if (ValidAppointment(appointment.StartDateTime, appointment.duration))
            {
                _context.Add(appointment);
                _context.SaveChanges();
                return true;
            }

            return false;
        }

        public bool EditAppointment(Appointment appointment)
        {
            _context.Update(appointment);
            _context.SaveChanges();
            return true;
        }

        public void DeleteAppointment(int? id)
        {
            var appointment =  _context.Appointments.Find(id);
            _context.Appointments.Remove(appointment);
            _context.SaveChanges();
        }

        // TODO: Redo this via overload to search via eMail and last name
        public Appointment GetAppointment(int? id)
        {
            
            var appointment = _context.Appointments.FirstOrDefault(m => m.Id == id);
            return appointment;
        }

        public List<Appointment> GetAppointments()
        {
            var appointments = _context.Appointments.ToList();

            return appointments;
        }

        private bool ValidAppointment(DateTime start, int duration)
        {
            if (duration <= 60 && AppointmentAvailable(start, duration))
                return true;

            return false;
        }

        private bool AppointmentAvailable(DateTime start, int duration)
        {
            var possibleConflicts = from a in _context.Appointments
                                    where !(((a.StartDateTime.AddMinutes(duration) < start))
                                    || (start.AddMinutes(duration) < a.StartDateTime))
                                    select a;


            if (possibleConflicts.FirstOrDefault() == null)
                return true;

            return false;
        }

        private bool AppointmentExists(int id)
        {
            return _context.Appointments.Any(m => m.Id == id);
        }
    }
}
