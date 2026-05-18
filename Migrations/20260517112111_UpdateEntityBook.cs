using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API_Bookstore.Migrations
{
    /// <inheritdoc />
    public partial class UpdateEntityBook : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CreateAt",
                table: "Books",
                newName: "CreatedAt");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "Books",
                newName: "CreateAt");
        }
    }
}
