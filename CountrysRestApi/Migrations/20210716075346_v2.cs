using Microsoft.EntityFrameworkCore.Migrations;

namespace CountrysRestApi.Migrations
{
    public partial class v2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "countries",
                keyColumn: "alpha",
                keyValue: "MX",
                columns: new[] { "name", "numeric_code" },
                values: new object[] { "Mexico", 1 });

            migrationBuilder.InsertData(
                table: "countries",
                columns: new[] { "alpha", "independent", "name", "numeric_code" },
                values: new object[,]
                {
                    { "HN", (short)1, "Honduras", 11 },
                    { "HT", (short)1, "Haiti", 10 },
                    { "AR", (short)1, "Argentina", 8 },
                    { "AQ", (short)1, "Antarctica", 7 },
                    { "GN", (short)1, "Guinea", 9 },
                    { "AO", (short)1, "Angola", 5 },
                    { "DZ", (short)1, "Algeria", 4 },
                    { "AX", (short)1, "Aland Islands", 3 },
                    { "GT", (short)1, "Guatemala", 2 },
                    { "AI", (short)1, "Anguilla", 6 }
                });

            migrationBuilder.InsertData(
                table: "subdivisions",
                columns: new[] { "code", "alpha", "name" },
                values: new object[,]
                {
                    { "AO-CNN", "AO", "Cunene" },
                    { "AO-BGO	", "AO", "Bengo" },
                    { "AO-BGU", "AO", "Benguela" },
                    { "AO-BIE", "AO", "Bié" },
                    { "AO-CAB", "AO", "Cabinda" }
                });

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "id", "email", "password", "role", "salt" },
                values: new object[] { 1, "test@domain.com", "CBBCC5FE04B937F376DC38BD824D32397B67FFBE3583747ADDD3058BEEB7264C6A74B826FB2EEB2C4BA9ADDCA3150FAF610822560717B5994489817D27FF0F4C", "Admin", "7278f1d5-b52a-4a0e-93b8-f57bad21adba" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "alpha",
                keyValue: "AI");

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "alpha",
                keyValue: "AO");

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "alpha",
                keyValue: "AQ");

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "alpha",
                keyValue: "AR");

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "alpha",
                keyValue: "AX");

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "alpha",
                keyValue: "DZ");

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "alpha",
                keyValue: "GN");

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "alpha",
                keyValue: "GT");

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "alpha",
                keyValue: "HN");

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "alpha",
                keyValue: "HT");

            migrationBuilder.DeleteData(
                table: "subdivisions",
                keyColumn: "code",
                keyValue: "AO-BGO	");

            migrationBuilder.DeleteData(
                table: "subdivisions",
                keyColumn: "code",
                keyValue: "AO-BGU");

            migrationBuilder.DeleteData(
                table: "subdivisions",
                keyColumn: "code",
                keyValue: "AO-BIE");

            migrationBuilder.DeleteData(
                table: "subdivisions",
                keyColumn: "code",
                keyValue: "AO-CAB");

            migrationBuilder.DeleteData(
                table: "subdivisions",
                keyColumn: "code",
                keyValue: "AO-CNN");

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.UpdateData(
                table: "countries",
                keyColumn: "alpha",
                keyValue: "MX",
                columns: new[] { "name", "numeric_code" },
                values: new object[] { "MEXICO", 52 });
        }
    }
}
