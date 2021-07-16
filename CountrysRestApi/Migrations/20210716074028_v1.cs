using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CountrysRestApi.Migrations
{
    public partial class v1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "countries",
                columns: table => new
                {
                    alpha = table.Column<string>(type: "varchar(2)", nullable: false),
                    name = table.Column<string>(type: "varchar(50)", nullable: false),
                    numeric_code = table.Column<int>(type: "int(3)", nullable: false, defaultValueSql: "'0'"),
                    independent = table.Column<short>(type: "SMALLINT(2)", nullable: false, defaultValueSql: "'1'")
                },
                constraints: table =>
                {
                    table.PrimaryKey("alpha", x => x.alpha);
                });

            migrationBuilder.CreateTable(
                name: "subdivisions",
                columns: table => new
                {
                    code = table.Column<string>(type: "varchar(10)", nullable: false),
                    alpha = table.Column<string>(type: "varchar(3)", nullable: false),
                    name = table.Column<string>(type: "varchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("code", x => x.code);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    email = table.Column<string>(type: "varchar(50)", nullable: false, defaultValueSql: "'0'"),
                    salt = table.Column<string>(type: "varchar(36)", nullable: false, defaultValueSql: "'0'"),
                    password = table.Column<string>(type: "varchar(128)", nullable: false, defaultValueSql: "'0'"),
                    role = table.Column<string>(type: "varchar(20)", nullable: false, defaultValueSql: "'0'")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.id);
                });

            migrationBuilder.InsertData(
                table: "countries",
                columns: new[] { "alpha", "independent", "name", "numeric_code" },
                values: new object[] { "MX", (short)1, "MEXICO", 52 });

            migrationBuilder.CreateIndex(
                name: "IX_EMAIL",
                table: "users",
                column: "email");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "countries");

            migrationBuilder.DropTable(
                name: "subdivisions");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
