using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class AddMovieDetails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Details_BackdropPath",
                table: "Movies",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<long>(
                name: "Details_Budget",
                table: "Movies",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "Details_Homepage",
                table: "Movies",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Details_ImdbId",
                table: "Movies",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Details_OriginCountry",
                table: "Movies",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<long>(
                name: "Details_Revenue",
                table: "Movies",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<int>(
                name: "Details_Runtime",
                table: "Movies",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Details_Status",
                table: "Movies",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Details_Tagline",
                table: "Movies",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Details_BackdropPath",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "Details_Budget",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "Details_Homepage",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "Details_ImdbId",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "Details_OriginCountry",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "Details_Revenue",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "Details_Runtime",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "Details_Status",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "Details_Tagline",
                table: "Movies");
        }
    }
}
