using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AppointmentSite.Data;
using AppointmentSite.Models;

namespace AppointmentSite.Controllers
{
    public class AppointmentsController : Controller
    {
        private readonly AppointmentsManager _apptsmanager;

        public AppointmentsController(AppointmentsManager apptsmanager)
        {
            _apptsmanager = apptsmanager;
        }

        // GET: Appointments
        public IActionResult Index()
        {
            return View(_apptsmanager.GetAppointments());
        }

        // GET: Appointments/Details/5
        // TODO: Fix this to only give details via eMail and last name
        public IActionResult Details(int? id, string eMail)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appointment = _apptsmanager.GetAppointment(id);
            
            if (appointment == null)
            {
                return NotFound();
            }

            return View(appointment);
        }

        // GET: Appointments/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Appointment appointment)
        {
            if (ModelState.IsValid)
            {
                if (_apptsmanager.CreateAppointment(appointment))
                {
                    return RedirectToAction("Details", new { appointment.Id, appointment.EmailAddress }); // Send them to the details page for their appointment
                }
                ModelState.AddModelError("StartDateTime", "The appointment entered is not available. Please try again.");
            }   
            return View(appointment);
        }

        // GET: Appointments/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appointment = _apptsmanager.GetAppointment(id);

            if (appointment == null)
            {
                return NotFound();
            }
            return View(appointment);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Appointment appointment)
        {
            if (id != appointment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if (_apptsmanager.EditAppointment(appointment))
                {
                    return RedirectToAction(nameof(Index));
                } 
            }
            return View(appointment);
        }

        // GET: Appointments/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appointment = _apptsmanager.GetAppointment(id);

            if (appointment == null)
            {
                return NotFound();
            }

            return View(appointment);
        }

        // POST: Appointments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _apptsmanager.DeleteAppointment(id);
            return RedirectToAction(nameof(Index));
        }

    }
}
