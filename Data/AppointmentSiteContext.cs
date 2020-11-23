using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppointmentSite.Models;

namespace AppointmentSite.Data
{
    public class AppointmentSiteContext : DbContext
    {
        public AppointmentSiteContext(DbContextOptions<AppointmentSiteContext> options) : base(options)
        {
        }

        public DbSet<Appointment> Appointments { get; set; }
    }
}
