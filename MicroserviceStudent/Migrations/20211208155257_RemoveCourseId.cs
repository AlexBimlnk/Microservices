using Microsoft.EntityFrameworkCore.Migrations;

namespace MicroserviceStudent.Migrations
{
    public partial class RemoveCourseId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CourseId",
                table: "Students");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "CourseId",
                table: "Students",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }
    }
}
