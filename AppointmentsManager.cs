﻿using Microsoft.EntityFrameworkCore;
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
            // Check if the appointment is valid before adding it to the database
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
            if (ValidAppointment(appointment.StartDateTime, appointment.duration, appointment.Id))
            {
                _context.Update(appointment);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public void DeleteAppointment(int? id)
        {
            var appointment =  _context.Appointments.Find(id);
            _context.Appointments.Remove(appointment);
            _context.SaveChanges();
        }

        // TODO: Redo this via overload to search via appointment Id and last name
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

        // Returns a boolean representing whether or not an appointment with a given start DateTime and duration is both of acceptable duration and available
        private bool ValidAppointment(DateTime start, int duration, int? idToExclude = null)
        {
            if (duration <= 60 && AppointmentAvailable(start, duration, idToExclude))
                return true;

            return false;
        }

        // Returns a boolean representing whether or not an appointment with a given start DateTime and duration is available
        private bool AppointmentAvailable(DateTime start, int duration, int? idToExclude)
        {
            // Returns all appointments that could conflict with the start time and duration given
            // If an idToExclude is given, excludes the appointment with that Id from the query
            var possibleConflicts = from a in _context.Appointments
                                    where !(((a.StartDateTime.AddMinutes(duration) < start))
                                    || (start.AddMinutes(duration) < a.StartDateTime)) && (idToExclude == null ? true : a.Id != idToExclude)
                                    select a;

            // If no appointments that could conflict actually exist, return true, false otherwise
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
