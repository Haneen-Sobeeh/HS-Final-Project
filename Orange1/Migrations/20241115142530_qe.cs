using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Orange1.Migrations
{
    /// <inheritdoc />
    public partial class qe : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Tastimonials",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Tastimonials_UserId",
                table: "Tastimonials",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tastimonials_AspNetUsers_UserId",
                table: "Tastimonials",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tastimonials_AspNetUsers_UserId",
                table: "Tastimonials");

            migrationBuilder.DropIndex(
                name: "IX_Tastimonials_UserId",
                table: "Tastimonials");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Tastimonials",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
