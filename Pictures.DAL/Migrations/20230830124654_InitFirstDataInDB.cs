using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Pictures.DAL.Migrations
{
    /// <inheritdoc />
    public partial class InitFirstDataInDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "User",
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
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Picture",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Picture", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Picture_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "Email", "Login", "Name", "Password", "Role", "Surname" },
                values: new object[,]
                {
                    { 1, "Alex@mail.com", "Alex123", "Alex", "12345", 0, "Xela" },
                    { 2, "Daniel@mail.com", "Daniel321", "Daniel", "54321", 0, "Lainad" }
                });

            migrationBuilder.InsertData(
                table: "Picture",
                columns: new[] { "Id", "Address", "Name", "UserId" },
                values: new object[,]
                {
                    { 1, "/img/lotos.jpg", "Lotos", 1 },
                    { 2, "/img/night city.jpg", "Night city", 1 },
                    { 3, "/img/puppy.jpg", "Puppy", 1 },
                    { 4, "/img/snowflake.jpg", "Snowflake", 1 },
                    { 5, "/img/misty forest.jpg", "Misty forest", 1 },
                    { 6, "/img/rabbit.jpg", "Rabbit", 1 },
                    { 7, "/img/tulip.jpg", "Tulip", 2 },
                    { 8, "/img/zurich.jpg", "Zurich", 2 },
                    { 9, "/img/evening sea.jpg", "Evening sea", 2 },
                    { 10, "/img/bear.jpg", "Bear", 2 },
                    { 11, "/img/cat.jpg", "Cat", 2 },
                    { 12, "/img/helix nebula.jpg", "Helix nebula", 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Picture_UserId",
                table: "Picture",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Picture");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
