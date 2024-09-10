using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ElenaMartiniKreationen.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class UserTypeTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Role",
                table: "UserProfile");

            migrationBuilder.AddColumn<int>(
                name: "UserTypeId",
                table: "UserProfile",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "UserType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false),
                    State = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserType", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "UserType",
                columns: new[] { "Id", "Description", "State" },
                values: new object[,]
                {
                    { 1, "Cliente Normal", true },
                    { 2, "Cliente Especial", true }
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserProfile_UserTypeId",
                table: "UserProfile",
                column: "UserTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserProfile_UserType_UserTypeId",
                table: "UserProfile",
                column: "UserTypeId",
                principalTable: "UserType",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserProfile_UserType_UserTypeId",
                table: "UserProfile");

            migrationBuilder.DropTable(
                name: "UserType");

            migrationBuilder.DropIndex(
                name: "IX_UserProfile_UserTypeId",
                table: "UserProfile");

            migrationBuilder.DropColumn(
                name: "UserTypeId",
                table: "UserProfile");

            migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "UserProfile",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
