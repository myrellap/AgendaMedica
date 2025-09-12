using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AgendaMedica.Data.Migrations
{
    /// <inheritdoc />
    public partial class Paciente : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pacientes_AspNetUsers_IdentityUserId",
                table: "Pacientes");

            migrationBuilder.DropIndex(
                name: "IX_Pacientes_IdentityUserId",
                table: "Pacientes");

            migrationBuilder.DropColumn(
                name: "IdentityUserId",
                table: "Pacientes");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Pacientes");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IdentityUserId",
                table: "Pacientes",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Pacientes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Pacientes_IdentityUserId",
                table: "Pacientes",
                column: "IdentityUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pacientes_AspNetUsers_IdentityUserId",
                table: "Pacientes",
                column: "IdentityUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
