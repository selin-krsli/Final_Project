using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieApp.DATA.Migrations
{
    /// <inheritdoc />
    public partial class AddColumnMovieIsHome : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsHome",
                table: "Movies",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsHome",
                table: "Movies");
        }
    }
}
