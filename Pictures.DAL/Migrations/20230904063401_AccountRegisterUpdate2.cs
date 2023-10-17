using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Pictures.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AccountRegisterUpdate2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Picture_User_UserId",
                table: "Picture");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Picture",
                newName: "AccountId");

            migrationBuilder.RenameIndex(
                name: "IX_Picture_UserId",
                table: "Picture",
                newName: "IX_Picture_AccountId");

            migrationBuilder.CreateTable(
                name: "Account",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Login = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Account", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Account",
                columns: new[] { "Id", "Email", "Login", "Name", "Password", "Role", "Surname" },
                values: new object[,]
                {
                    { 1, "Alex@mail.com", "Alex123", "Alex", "12345", 0, "Xela" },
                    { 2, "Daniel@mail.com", "Daniel321", "Daniel", "54321", 0, "Lainad" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Picture_Account_AccountId",
                table: "Picture",
                column: "AccountId",
                principalTable: "Account",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Picture_Account_AccountId",
                table: "Picture");

            migrationBuilder.DropTable(
                name: "Account");

            migrationBuilder.RenameColumn(
                name: "AccountId",
                table: "Picture",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Picture_AccountId",
                table: "Picture",
                newName: "IX_Picture_UserId");

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Login = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "Email", "Login", "Name", "Password", "Role", "Surname" },
                values: new object[,]
                {
                    { 1, "Alex@mail.com", "Alex123", "Alex", "12345", 0, "Xela" },
                    { 2, "Daniel@mail.com", "Daniel321", "Daniel", "54321", 0, "Lainad" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Picture_User_UserId",
                table: "Picture",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
