using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Conversions",
                columns: new[] { "Id", "Name", "Value" },
                values: new object[,]
                {
                    { 1, "A4 Black", 0.05m },
                    { 2, "A4 Color", 0.15m },
                    { 3, "A3 Black", 0.10m },
                    { 4, "A3 Color", 0.30m }
                });

            migrationBuilder.InsertData(
                table: "Groups",
                columns: new[] { "Id", "Acronym", "IsDeleted", "Name" },
                values: new object[,]
                {
                    { 1, "604-FT-F", false, "604 Full Time French" },
                    { 2, "604-FT-D", false, "604 Full Time Deutsch" },
                    { 3, "604-PT-F", false, "604 Part Time French" },
                    { 4, "604-PT-D", false, "604 Part Time Deutsch" },
                    { 5, "604-ALL", false, "604 All Students" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Address", "Email", "FirstName", "Gender", "IsDeleted", "LastName", "Username" },
                values: new object[,]
                {
                    { 1, "Rue de la Gare 12, 1950 Sion", "david.marsoni@students.hevs.ch", "David", "Mr", false, "Marsoni", "david.marsoni" },
                    { 2, "Rue de la Gare 12, 1950 Sion", "mathias.pitteloud@students.hevs.ch", "Mathias", "Mr", false, "Pitteloud", "mathias.pitteloud" },
                    { 3, "Rue de la Gare 12, 1950 Sion", "thomas.biselx@students.hevs.ch", "Thomas", "Mr", false, "Biselx", "thomas.biselx" },
                    { 4, "Rue de la Gare 12, 1950 Sion", "jonathan.araujo@students.hevs.ch", "Jonathan", "Mr", false, "Araújo", "jonathan.araujo" },
                    { 5, "Rue de la Gare 12, 1950 Sion", "johanna.summermatter@students.hevs.ch", "Johanna", "Mss", false, "Summermatter", "johanna.summermatter" },
                    { 6, "Rue de la Gare 12, 1950 Sion", "dylan.sanderson@students.hevs.ch", "Dylan", "Mr", false, "Sanderson", "dylan.sanderson" },
                    { 7, "Rue de la Gare 12, 1950 Sion", "zanya.fernandezrodriguez@students.hevs.ch", "Zanya", "Mss", false, "Fernández Rodríguez", "zanya.fernandezrodriguez" },
                    { 8, "Rue de la Gare 12, 1950 Sion", "johann.vonroten@students.hevs.ch", "Johann", "Mr", false, "Von Roten", "johann.vonroten" }
                });

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "Id", "Balance", "CreatedAt", "UpdatedAt", "UserId" },
                values: new object[,]
                {
                    { 1, 10m, null, null, 1 },
                    { 2, 10m, null, null, 2 },
                    { 3, 10m, null, null, 3 },
                    { 4, 10m, null, null, 4 },
                    { 5, 10m, null, null, 5 },
                    { 6, 10m, null, null, 6 },
                    { 7, 10m, null, null, 7 },
                    { 8, 10m, null, null, 8 }
                });

            migrationBuilder.InsertData(
                table: "UserGroups",
                columns: new[] { "Id", "GroupId", "UserId" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 2, 1, 2 },
                    { 3, 1, 3 },
                    { 4, 1, 4 },
                    { 5, 2, 5 },
                    { 6, 3, 6 },
                    { 7, 1, 7 },
                    { 8, 3, 8 },
                    { 9, 5, 1 },
                    { 10, 5, 2 },
                    { 11, 5, 3 },
                    { 12, 5, 4 },
                    { 13, 5, 5 },
                    { 14, 5, 6 },
                    { 15, 5, 7 },
                    { 16, 5, 8 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Conversions",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Conversions",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "UserGroups",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "UserGroups",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "UserGroups",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "UserGroups",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "UserGroups",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "UserGroups",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "UserGroups",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "UserGroups",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "UserGroups",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "UserGroups",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "UserGroups",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "UserGroups",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "UserGroups",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "UserGroups",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "UserGroups",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "UserGroups",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 8);
        }
    }
}
