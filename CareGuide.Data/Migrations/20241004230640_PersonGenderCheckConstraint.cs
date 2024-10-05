using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CareGuide.Data.Migrations
{
    /// <inheritdoc />
    public partial class PersonGenderCheckConstraint : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddCheckConstraint(
                name: "CK_Person_Gender",
                table: "person",
                sql: "gender IN ('M', 'F', 'O')");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_Person_Gender",
                table: "person");
        }
    }
}
