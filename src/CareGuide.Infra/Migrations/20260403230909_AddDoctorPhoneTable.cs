using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CareGuide.Infra.Migrations
{
    /// <inheritdoc />
    public partial class AddDoctorPhoneTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "doctor_phones",
                columns: table => new
                {
                    doctor_id = table.Column<Guid>(type: "uuid", nullable: false),
                    phone_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_doctor_phones", x => new { x.doctor_id, x.phone_id });
                    table.ForeignKey(
                        name: "fk_doctor_phone_doctor",
                        column: x => x.doctor_id,
                        principalTable: "doctors",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_doctor_phone_phone",
                        column: x => x.phone_id,
                        principalTable: "phones",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_doctor_phones_phone_id",
                table: "doctor_phones",
                column: "phone_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "doctor_phones");
        }
    }
}
