using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CareGuide.Infra.Migrations
{
    /// <inheritdoc />
    public partial class FixPersonRelationshipsAndPersonPhoneCompositeKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_person_phones_persons_person_id",
                table: "person_phones");

            migrationBuilder.DropForeignKey(
                name: "FK_person_phones_phones_phone_id",
                table: "person_phones");

            migrationBuilder.DropForeignKey(
                name: "FK_users_persons_person_id",
                table: "users");

            migrationBuilder.DropIndex(
                name: "IX_users_person_id",
                table: "users");

            migrationBuilder.DropColumn(
                name: "created_at",
                table: "person_phones");

            migrationBuilder.DropColumn(
                name: "id",
                table: "person_phones");

            migrationBuilder.DropColumn(
                name: "is_active",
                table: "person_phones");

            migrationBuilder.DropColumn(
                name: "updated_at",
                table: "person_phones");

            migrationBuilder.AlterColumn<Guid>(
                name: "person_id",
                table: "person_diseases",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_users_person_id",
                table: "users",
                column: "person_id",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "fk_person_phone_person",
                table: "person_phones",
                column: "person_id",
                principalTable: "persons",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_person_phone_phone",
                table: "person_phones",
                column: "phone_id",
                principalTable: "phones",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_user_person",
                table: "users",
                column: "person_id",
                principalTable: "persons",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_person_phone_person",
                table: "person_phones");

            migrationBuilder.DropForeignKey(
                name: "fk_person_phone_phone",
                table: "person_phones");

            migrationBuilder.DropForeignKey(
                name: "fk_user_person",
                table: "users");

            migrationBuilder.DropIndex(
                name: "IX_users_person_id",
                table: "users");

            migrationBuilder.AddColumn<DateTime>(
                name: "created_at",
                table: "person_phones",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "id",
                table: "person_phones",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<bool>(
                name: "is_active",
                table: "person_phones",
                type: "boolean",
                nullable: false,
                defaultValue: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "updated_at",
                table: "person_phones",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<Guid>(
                name: "person_id",
                table: "person_diseases",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.CreateIndex(
                name: "IX_users_person_id",
                table: "users",
                column: "person_id");

            migrationBuilder.AddForeignKey(
                name: "FK_person_phones_persons_person_id",
                table: "person_phones",
                column: "person_id",
                principalTable: "persons",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_person_phones_phones_phone_id",
                table: "person_phones",
                column: "phone_id",
                principalTable: "phones",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_users_persons_person_id",
                table: "users",
                column: "person_id",
                principalTable: "persons",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
