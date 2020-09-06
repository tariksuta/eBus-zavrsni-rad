using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace eBus.WebApi.Migrations
{
    public partial class dodatnipodaci : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "Slika",
                table: "Kompanija",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Karta",
                columns: new[] { "ID", "AngazujeID", "BrojKarte", "DatumIzdavanja", "SjedisteID", "VrijemeDolaska", "VrijemePolaska" },
                values: new object[,]
                {
                    { 35, 1, "pewdgkbcgy", new DateTime(2020, 9, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, new TimeSpan(0, 10, 0, 0, 0), new TimeSpan(0, 8, 0, 0, 0) },
                    { 36, 1, "tzldgkicgo", new DateTime(2020, 9, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, new TimeSpan(0, 10, 0, 0, 0), new TimeSpan(0, 8, 0, 0, 0) },
                    { 37, 2, "kjldpkicgo", new DateTime(2020, 9, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 45, new TimeSpan(0, 12, 0, 0, 0), new TimeSpan(0, 10, 0, 0, 0) },
                    { 38, 1, "tzldgkicgo", new DateTime(2020, 9, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, new TimeSpan(0, 10, 0, 0, 0), new TimeSpan(0, 8, 0, 0, 0) },
                    { 39, 2, "zzltgkixgo", new DateTime(2020, 9, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), 45, new TimeSpan(0, 12, 0, 0, 0), new TimeSpan(0, 10, 0, 0, 0) },
                    { 40, 2, "pzedhkicgo", new DateTime(2020, 9, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 45, new TimeSpan(0, 10, 0, 0, 0), new TimeSpan(0, 8, 0, 0, 0) }
                });

            migrationBuilder.UpdateData(
                table: "KorisniciUloge",
                keyColumn: "ID",
                keyValue: 1,
                column: "DatumIzmjene",
                value: new DateTime(2020, 9, 6, 15, 6, 29, 255, DateTimeKind.Local).AddTicks(8268));

            migrationBuilder.UpdateData(
                table: "KorisniciUloge",
                keyColumn: "ID",
                keyValue: 2,
                column: "DatumIzmjene",
                value: new DateTime(2020, 9, 6, 15, 6, 29, 260, DateTimeKind.Local).AddTicks(8131));

            migrationBuilder.UpdateData(
                table: "Notifikacije",
                keyColumn: "ID",
                keyValue: 1,
                column: "DatumSlanja",
                value: new DateTime(2020, 9, 6, 15, 6, 29, 265, DateTimeKind.Local).AddTicks(8051));

            migrationBuilder.UpdateData(
                table: "Novosti",
                keyColumn: "ID",
                keyValue: 1,
                column: "DatumObjave",
                value: new DateTime(2020, 9, 6, 15, 6, 29, 265, DateTimeKind.Local).AddTicks(4447));

            migrationBuilder.UpdateData(
                table: "Putnik",
                keyColumn: "ID",
                keyValue: 1,
                column: "DatumRegistracije",
                value: new DateTime(2020, 9, 6, 15, 6, 29, 261, DateTimeKind.Local).AddTicks(8734));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Karta",
                keyColumn: "ID",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "Karta",
                keyColumn: "ID",
                keyValue: 36);

            migrationBuilder.DeleteData(
                table: "Karta",
                keyColumn: "ID",
                keyValue: 37);

            migrationBuilder.DeleteData(
                table: "Karta",
                keyColumn: "ID",
                keyValue: 38);

            migrationBuilder.DeleteData(
                table: "Karta",
                keyColumn: "ID",
                keyValue: 39);

            migrationBuilder.DeleteData(
                table: "Karta",
                keyColumn: "ID",
                keyValue: 40);

            migrationBuilder.DropColumn(
                name: "Slika",
                table: "Kompanija");

            migrationBuilder.UpdateData(
                table: "KorisniciUloge",
                keyColumn: "ID",
                keyValue: 1,
                column: "DatumIzmjene",
                value: new DateTime(2020, 7, 30, 13, 53, 29, 59, DateTimeKind.Local).AddTicks(3497));

            migrationBuilder.UpdateData(
                table: "KorisniciUloge",
                keyColumn: "ID",
                keyValue: 2,
                column: "DatumIzmjene",
                value: new DateTime(2020, 7, 30, 13, 53, 29, 63, DateTimeKind.Local).AddTicks(8469));

            migrationBuilder.UpdateData(
                table: "Notifikacije",
                keyColumn: "ID",
                keyValue: 1,
                column: "DatumSlanja",
                value: new DateTime(2020, 7, 30, 13, 53, 29, 72, DateTimeKind.Local).AddTicks(4509));

            migrationBuilder.UpdateData(
                table: "Novosti",
                keyColumn: "ID",
                keyValue: 1,
                column: "DatumObjave",
                value: new DateTime(2020, 7, 30, 13, 53, 29, 71, DateTimeKind.Local).AddTicks(7217));

            migrationBuilder.UpdateData(
                table: "Putnik",
                keyColumn: "ID",
                keyValue: 1,
                column: "DatumRegistracije",
                value: new DateTime(2020, 7, 30, 13, 53, 29, 65, DateTimeKind.Local).AddTicks(4499));
        }
    }
}
