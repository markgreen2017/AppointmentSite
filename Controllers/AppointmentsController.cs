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

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            LoginManager login = new LoginManager();

            if (login.ValidateLogin(username, password))
            {
                return View("Index", _apptsmanager.GetAppointments());
            }

            return RedirectToAction(nameof(Login));
        }

        public IActionResult Details(int? id, string lastName)
        {
            if (id == null || lastName == null)
            {
                return NotFound();
            }

            var appointment = _apptsmanager.GetAppointment(id, lastName);
            
            if (appointment == null)
            {
                return NotFound();
            }

            return View(appointment);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Appointment appointment)
        {
            if (ModelState.IsValid) // Ensure that the appointment information entered is valid
            {
                if (_apptsmanager.CreateAppointment(appointment)) // Check if the appointment was successfully created
                {
                    return RedirectToAction("Details", new { appointment.Id, appointment.LastName }); // Send them to the details page for their appointment
                }
                ModelState.AddModelError("StartDateTime", "The appointment entered is not available. Please try again.");
            }   
            return View(appointment); // Refresh the page with model error indicating appointment was not created correctly
        }

        public IActionResult Edit(int? id, string lastName)
        {
            if (id == null || lastName == null)
            {
                return NotFound();
            }

            var appointment = _apptsmanager.GetAppointment(id, lastName);

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
                    return RedirectToAction("Details", new { id, appointment.LastName });
                }
                ModelState.AddModelError("StartDateTime", "The appointment entered is not available. Please try again.");
            }
            return View(appointment);
        }

        public IActionResult Delete(int? id, string lastName)
        {
            if (id == null || lastName == null)
            {
                return NotFound();
            }

            var appointment = _apptsmanager.GetAppointment(id, lastName);

            if (appointment == null)
            {
                return NotFound();
            }

            return View(appointment);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id, bool isManager = false)
        {
            // Present the user an error message if they cannot delete the given appointment
            if (!(_apptsmanager.DeleteAppointment(id, isManager)))
            {
                TempData["DeletionError"] = "Error: The appointment you are trying to delete is too close to its scheduled time.";
                return RedirectToAction("Details", new { id = id });
            }
            return RedirectToAction(nameof(Create));
        }
    }
}
