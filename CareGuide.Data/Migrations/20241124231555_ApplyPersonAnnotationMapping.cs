using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CareGuide.Data.Migrations
{
    /// <inheritdoc />
    public partial class ApplyPersonAnnotationMapping : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PersonAnnotations_person_PersonId",
                table: "PersonAnnotations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PersonAnnotations",
                table: "PersonAnnotations");

            migrationBuilder.RenameTable(
                name: "PersonAnnotations",
                newName: "person_annotation");

            migrationBuilder.RenameColumn(
                name: "Register",
                table: "person_annotation",
                newName: "register");

            migrationBuilder.RenameColumn(
                name: "Details",
                table: "person_annotation",
                newName: "details");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "person_annotation",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "PersonId",
                table: "person_annotation",
                newName: "person_id");

            migrationBuilder.RenameColumn(
                name: "FileUrl",
                table: "person_annotation",
                newName: "file_url");

            migrationBuilder.RenameIndex(
                name: "IX_PersonAnnotations_PersonId",
                table: "person_annotation",
                newName: "IX_person_annotation_person_id");

            migrationBuilder.AlterColumn<string>(
                name: "file_url",
                table: "person_annotation",
                type: "character varying(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_person_annotation",
                table: "person_annotation",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_person_annotation_person",
                table: "person_annotation",
                column: "person_id",
                principalTable: "person",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_person_annotation_person",
                table: "person_annotation");

            migrationBuilder.DropPrimaryKey(
                name: "PK_person_annotation",
                table: "person_annotation");

            migrationBuilder.RenameTable(
                name: "person_annotation",
                newName: "PersonAnnotations");

            migrationBuilder.RenameColumn(
                name: "register",
                table: "PersonAnnotations",
                newName: "Register");

            migrationBuilder.RenameColumn(
                name: "details",
                table: "PersonAnnotations",
                newName: "Details");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "PersonAnnotations",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "person_id",
                table: "PersonAnnotations",
                newName: "PersonId");

            migrationBuilder.RenameColumn(
                name: "file_url",
                table: "PersonAnnotations",
                newName: "FileUrl");

            migrationBuilder.RenameIndex(
                name: "IX_person_annotation_person_id",
                table: "PersonAnnotations",
                newName: "IX_PersonAnnotations_PersonId");

            migrationBuilder.AlterColumn<string>(
                name: "FileUrl",
                table: "PersonAnnotations",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_PersonAnnotations",
                table: "PersonAnnotations",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PersonAnnotations_person_PersonId",
                table: "PersonAnnotations",
                column: "PersonId",
                principalTable: "person",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
