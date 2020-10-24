using Microsoft.EntityFrameworkCore.Migrations;

namespace AppointmentSite.Migrations
{
    public partial class CreateAppointmentBranch : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "Appointments",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "PhoneNumber",
                table: "Appointments",
                type: "int",
                nullable: false,
                oldClrType: typeof(string));
        }
    }
}
