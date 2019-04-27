using Microsoft.EntityFrameworkCore.Migrations;

namespace HashBucket.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "hashValues",
                columns: table => new
                {
                    HashKey = table.Column<string>(nullable: false),
                    EncryptedValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_hashValues", x => x.HashKey);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "hashValues");
        }
    }
}
