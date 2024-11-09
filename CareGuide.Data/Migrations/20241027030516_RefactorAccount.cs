using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CareGuide.Data.Migrations
{
    /// <inheritdoc />
    public partial class RefactorAccount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_person_user_user_id",
                table: "person");

            migrationBuilder.DropIndex(
                name: "IX_person_user_id",
                table: "person");

            migrationBuilder.DropColumn(
                name: "session_token",
                table: "user");

            migrationBuilder.DropColumn(
                name: "register",
                table: "person");

            migrationBuilder.DropColumn(
                name: "user_id",
                table: "person");

            migrationBuilder.AddColumn<Guid>(
                name: "person_id",
                table: "user",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "picture",
                table: "person",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_user_person_id",
                table: "user",
                column: "person_id");

            migrationBuilder.AddForeignKey(
                name: "FK_user_person_person_id",
                table: "user",
                column: "person_id",
                principalTable: "person",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_user_person_person_id",
                table: "user");

            migrationBuilder.DropIndex(
                name: "IX_user_person_id",
                table: "user");

            migrationBuilder.DropColumn(
                name: "person_id",
                table: "user");

            migrationBuilder.DropColumn(
                name: "picture",
                table: "person");

            migrationBuilder.AddColumn<string>(
                name: "session_token",
                table: "user",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "register",
                table: "person",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "user_id",
                table: "person",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_person_user_id",
                table: "person",
                column: "user_id",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_person_user_user_id",
                table: "person",
                column: "user_id",
                principalTable: "user",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
