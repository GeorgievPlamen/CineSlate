using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class ConfigureVersion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Version",
                table: "Users",
                newName: "xmin");

            migrationBuilder.RenameColumn(
                name: "Version",
                table: "Reviews",
                newName: "xmin");

            migrationBuilder.RenameColumn(
                name: "Version",
                table: "RefreshTokens",
                newName: "xmin");

            migrationBuilder.RenameColumn(
                name: "Version",
                table: "Movies",
                newName: "xmin");

            migrationBuilder.RenameColumn(
                name: "Version",
                table: "LikesModel",
                newName: "xmin");

            migrationBuilder.RenameColumn(
                name: "Version",
                table: "Genres",
                newName: "xmin");

            migrationBuilder.RenameColumn(
                name: "Version",
                table: "CommentModel",
                newName: "xmin");

            migrationBuilder.AlterColumn<uint>(
                name: "xmin",
                table: "Users",
                type: "xid",
                rowVersion: true,
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<uint>(
                name: "xmin",
                table: "Reviews",
                type: "xid",
                rowVersion: true,
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<uint>(
                name: "xmin",
                table: "RefreshTokens",
                type: "xid",
                rowVersion: true,
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<uint>(
                name: "xmin",
                table: "Movies",
                type: "xid",
                rowVersion: true,
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<uint>(
                name: "xmin",
                table: "LikesModel",
                type: "xid",
                rowVersion: true,
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<uint>(
                name: "xmin",
                table: "Genres",
                type: "xid",
                rowVersion: true,
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<uint>(
                name: "xmin",
                table: "CommentModel",
                type: "xid",
                rowVersion: true,
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "xmin",
                table: "Users",
                newName: "Version");

            migrationBuilder.RenameColumn(
                name: "xmin",
                table: "Reviews",
                newName: "Version");

            migrationBuilder.RenameColumn(
                name: "xmin",
                table: "RefreshTokens",
                newName: "Version");

            migrationBuilder.RenameColumn(
                name: "xmin",
                table: "Movies",
                newName: "Version");

            migrationBuilder.RenameColumn(
                name: "xmin",
                table: "LikesModel",
                newName: "Version");

            migrationBuilder.RenameColumn(
                name: "xmin",
                table: "Genres",
                newName: "Version");

            migrationBuilder.RenameColumn(
                name: "xmin",
                table: "CommentModel",
                newName: "Version");

            migrationBuilder.AlterColumn<long>(
                name: "Version",
                table: "Users",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(uint),
                oldType: "xid",
                oldRowVersion: true);

            migrationBuilder.AlterColumn<long>(
                name: "Version",
                table: "Reviews",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(uint),
                oldType: "xid",
                oldRowVersion: true);

            migrationBuilder.AlterColumn<long>(
                name: "Version",
                table: "RefreshTokens",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(uint),
                oldType: "xid",
                oldRowVersion: true);

            migrationBuilder.AlterColumn<long>(
                name: "Version",
                table: "Movies",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(uint),
                oldType: "xid",
                oldRowVersion: true);

            migrationBuilder.AlterColumn<long>(
                name: "Version",
                table: "LikesModel",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(uint),
                oldType: "xid",
                oldRowVersion: true);

            migrationBuilder.AlterColumn<long>(
                name: "Version",
                table: "Genres",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(uint),
                oldType: "xid",
                oldRowVersion: true);

            migrationBuilder.AlterColumn<long>(
                name: "Version",
                table: "CommentModel",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(uint),
                oldType: "xid",
                oldRowVersion: true);
        }
    }
}
