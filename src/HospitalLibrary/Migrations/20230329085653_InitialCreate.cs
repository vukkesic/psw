using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace HospitalLibrary.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "appointments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    StartTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    EndTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    DoctorId = table.Column<int>(type: "integer", nullable: false),
                    PatientId = table.Column<int>(type: "integer", nullable: false),
                    Canceled = table.Column<bool>(type: "boolean", nullable: false),
                    CancelationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Used = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_appointments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "rooms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Number = table.Column<string>(type: "text", nullable: false),
                    Floor = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_rooms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Surname = table.Column<string>(type: "text", nullable: true),
                    DateOfBirth = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: true),
                    Username = table.Column<string>(type: "text", nullable: true),
                    Password = table.Column<string>(type: "text", nullable: true),
                    Phone = table.Column<string>(type: "text", nullable: true),
                    Gender = table.Column<int>(type: "integer", nullable: false),
                    ProfileImage = table.Column<string>(type: "text", nullable: true),
                    Role = table.Column<int>(type: "integer", nullable: false),
                    Discriminator = table.Column<string>(type: "text", nullable: false),
                    LicenseNumber = table.Column<string>(type: "text", nullable: true),
                    Blocked = table.Column<bool>(type: "boolean", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "healthdata",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    BloodPresure = table.Column<string>(type: "text", nullable: true),
                    BloodSugar = table.Column<string>(type: "text", nullable: true),
                    BodyFatPercentage = table.Column<string>(type: "text", nullable: true),
                    Weight = table.Column<string>(type: "text", nullable: true),
                    PatientId = table.Column<int>(type: "integer", nullable: false),
                    MeasurementTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_healthdata", x => x.Id);
                    table.ForeignKey(
                        name: "FK_healthdata_users_PatientId",
                        column: x => x.PatientId,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "appointments",
                columns: new[] { "Id", "CancelationTime", "Canceled", "DoctorId", "EndTime", "PatientId", "StartTime", "Used" },
                values: new object[] { 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 2, new DateTime(2023, 5, 20, 10, 0, 0, 0, DateTimeKind.Unspecified), 1, new DateTime(2023, 5, 20, 9, 30, 0, 0, DateTimeKind.Unspecified), false });

            migrationBuilder.InsertData(
                table: "rooms",
                columns: new[] { "Id", "Floor", "Number" },
                values: new object[,]
                {
                    { 1, 1, "101A" },
                    { 2, 2, "204" },
                    { 3, 3, "305B" }
                });

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "Id", "DateOfBirth", "Discriminator", "Email", "Gender", "LicenseNumber", "Name", "Password", "Phone", "ProfileImage", "Role", "Surname", "Username" },
                values: new object[,]
                {
                    { 2, new DateTime(1967, 7, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), "Doctor", "miki@gmail.com", 0, "123dr2009", "Miki", "123", "0691202148", "", 1, "Mikic", "miki@gmail.com" },
                    { 3, new DateTime(1991, 2, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "Doctor", "roki@gmail.com", 0, "198r2009", "Roki", "123", "06312909304", "", 1, "Rokic", "roki@gmail.com" }
                });

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "Id", "Blocked", "DateOfBirth", "Discriminator", "Email", "Gender", "Name", "Password", "Phone", "ProfileImage", "Role", "Surname", "Username" },
                values: new object[,]
                {
                    { 1, false, new DateTime(2018, 7, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), "Patient", "vuk@mail.com", 0, "Vuk", "123", "06312212", "", 0, "Kesic", "vuk@mail.com" },
                    { 5, false, new DateTime(1988, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Patient", "maria@mail.com", 1, "Maria", "123", "06893232", "", 0, "Rossi", "maria@mail.com" }
                });

            migrationBuilder.InsertData(
                table: "healthdata",
                columns: new[] { "Id", "BloodPresure", "BloodSugar", "BodyFatPercentage", "MeasurementTime", "PatientId", "Weight" },
                values: new object[] { 1, "120/80", "12", "17", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "102" });

            migrationBuilder.CreateIndex(
                name: "IX_healthdata_PatientId",
                table: "healthdata",
                column: "PatientId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "appointments");

            migrationBuilder.DropTable(
                name: "healthdata");

            migrationBuilder.DropTable(
                name: "rooms");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
