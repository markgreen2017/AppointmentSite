using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AppointmentSite.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Appointments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    Subject = table.Column<string>(nullable: false),
                    PhoneNumber = table.Column<string>(maxLength: 20, nullable: false),
                    EmailAddress = table.Column<string>(maxLength: 20, nullable: false),
                    StartDateTime = table.Column<DateTime>(nullable: false),
                    duration = table.Column<int>(nullable: false),
                    Notes = table.Column<string>(maxLength: 1000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appointments", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Appointments");
        }
    }
}
