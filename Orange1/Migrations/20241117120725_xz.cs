using Microsoft.EntityFrameworkCore.Migrations;



#nullable disable

namespace Orange1.Migrations
{
    /// <inheritdoc />
    public partial class xz : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OrderDate",
                table: "productInCards",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "OrderTime",
                table: "productInCards",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrderDate",
                table: "productInCards");

            migrationBuilder.DropColumn(
                name: "OrderTime",
                table: "productInCards");
        }
    }
}
