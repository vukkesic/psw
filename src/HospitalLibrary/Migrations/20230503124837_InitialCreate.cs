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
                name: "blogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: true),
                    Text = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_blogs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "blooddonationnotifications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: true),
                    Text = table.Column<string>(type: "text", nullable: true),
                    StartTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    EndTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Location = table.Column<string>(type: "text", nullable: true),
                    Status = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_blooddonationnotifications", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "notifications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: true),
                    Text = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_notifications", x => x.Id);
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
                name: "specializations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SpecName = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_specializations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "referralletters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PatientId = table.Column<int>(type: "integer", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    SpecializationId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_referralletters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_referralletters_specializations_SpecializationId",
                        column: x => x.SpecializationId,
                        principalTable: "specializations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                    SpecializationId = table.Column<int>(type: "integer", nullable: true),
                    Blocked = table.Column<bool>(type: "boolean", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_users_specializations_SpecializationId",
                        column: x => x.SpecializationId,
                        principalTable: "specializations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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

            migrationBuilder.CreateTable(
                name: "menstrualdata",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    LastPeriod = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    NextPeriod = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ApproxOvulationDay = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    PatientId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_menstrualdata", x => x.Id);
                    table.ForeignKey(
                        name: "FK_menstrualdata_users_PatientId",
                        column: x => x.PatientId,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "examinations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DiagnosisCode = table.Column<string>(type: "text", nullable: true),
                    DiagnosisDescription = table.Column<string>(type: "text", nullable: true),
                    DoctorId = table.Column<int>(type: "integer", nullable: false),
                    PatientId = table.Column<int>(type: "integer", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    HealthDataId = table.Column<int>(type: "integer", nullable: true),
                    Prescription = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_examinations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_examinations_healthdata_HealthDataId",
                        column: x => x.HealthDataId,
                        principalTable: "healthdata",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "appointments",
                columns: new[] { "Id", "CancelationTime", "Canceled", "DoctorId", "EndTime", "PatientId", "StartTime", "Used" },
                values: new object[] { 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 2, new DateTime(2023, 5, 20, 10, 0, 0, 0, DateTimeKind.Unspecified), 1, new DateTime(2023, 5, 20, 9, 30, 0, 0, DateTimeKind.Unspecified), false });

            migrationBuilder.InsertData(
                table: "blogs",
                columns: new[] { "Id", "Text", "Title" },
                values: new object[] { 1, "If you are worried about whether your child needs to gain weight, it’s very important to check with your doctor before getting to work on fattening them up. It’s entirely possible that your child’s weight is absolutely fine. Given that one in five children in the US is obese and another one in six is overweight, it’s easy to see how a parent might think their child is too thin in comparison. One way to find out if your child’s weight is healthy is to check their body mass index, a calculation using height and weight that is used for children ages 2 and up.", "Does your child need to gain weight?" });

            migrationBuilder.InsertData(
                table: "notifications",
                columns: new[] { "Id", "Text", "Title" },
                values: new object[,]
                {
                    { 1, "With our team, you recieve medical tests and physician consultations in just a few days. This means a diagnosis comes fast, and treatments, surgery included, can be scheduled very quickly. We take care of you during the entire treatment.", "Quick scheduling" },
                    { 2, "Taking a perspective of frontline health workers as internal clients within health systems, this study explored how perceived injustice in policy and organizational matters influence frontline health worker motivation and the consequent effect on workers' attitudes and performance in delivering maternal and neonatal health care in public hospitals. It consisted of an ethnographic study in two public hospitals in Southern Ghana. Participant observation, conversation and in-depth interviews were conducted over a 16-month period. Ethical approval and consent were obtained from relevant persons and authorities. Qualitative analysis software Nvivo 8 was used for coding and analysis of data. Main themes identified in the analysis form the basis for interpreting and reporting study findings. Findings showed that most workers perceived injustice in distributive, procedural and interactional dimensions at various levels in the health system. At the national policy level this included poor conditions of service. At the hospital level, it included perceived inequity in distribution of incentives, lack of protection and respect for workers.", "Your health is our concern" }
                });

            migrationBuilder.InsertData(
                table: "rooms",
                columns: new[] { "Id", "Floor", "Number" },
                values: new object[,]
                {
                    { 2, 2, "204" },
                    { 1, 1, "101A" },
                    { 3, 3, "305B" }
                });

            migrationBuilder.InsertData(
                table: "specializations",
                columns: new[] { "Id", "SpecName" },
                values: new object[,]
                {
                    { 11, "Obstetrics and gynecology" },
                    { 12, "Ophthalmology" },
                    { 13, "Pathology" },
                    { 14, "Pediatrics" },
                    { 5, "Emergency medicine" },
                    { 16, "Preventive medicine" },
                    { 10, "Nuclear medicine" },
                    { 17, "Psychiatry" },
                    { 18, "Radiation oncology" },
                    { 15, "Physical medicine and rehabilitation" },
                    { 9, "Neurology" },
                    { 20, "Urology" },
                    { 7, "Internal medicine" },
                    { 6, "Family medicine" },
                    { 19, "Surgery" },
                    { 4, "Diagnostic radiology" },
                    { 3, "Dermatology" },
                    { 2, "Anesthesiology" },
                    { 1, "Allergy and immunology" },
                    { 8, "Medical genetics" }
                });

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "Id", "Blocked", "DateOfBirth", "Discriminator", "Email", "Gender", "Name", "Password", "Phone", "ProfileImage", "Role", "Surname", "Username" },
                values: new object[,]
                {
                    { 5, false, new DateTime(1988, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Patient", "maria@mail.com", 1, "Maria", "123", "06893232", "", 0, "Rossi", "maria@mail.com" },
                    { 1, false, new DateTime(2018, 7, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), "Patient", "vuk@mail.com", 0, "Vuk", "123", "06312212", "", 0, "Kesic", "vuk@mail.com" }
                });

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "Id", "DateOfBirth", "Discriminator", "Email", "Gender", "Name", "Password", "Phone", "ProfileImage", "Role", "Surname", "Username" },
                values: new object[] { 4, new DateTime(1987, 11, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "Admin", "angel@mail.com", 0, "Angel", "123", "066119128", "", 2, "Di Maria", "angel@mail.com" });

            migrationBuilder.InsertData(
                table: "healthdata",
                columns: new[] { "Id", "BloodPresure", "BloodSugar", "BodyFatPercentage", "MeasurementTime", "PatientId", "Weight" },
                values: new object[] { 1, "120/80", "12", "17", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "102" });

            migrationBuilder.InsertData(
                table: "menstrualdata",
                columns: new[] { "Id", "ApproxOvulationDay", "LastPeriod", "NextPeriod", "PatientId" },
                values: new object[] { 1, new DateTime(2023, 4, 3, 10, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 3, 20, 9, 30, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 4, 23, 10, 0, 0, 0, DateTimeKind.Unspecified), 5 });

            migrationBuilder.InsertData(
                table: "referralletters",
                columns: new[] { "Id", "IsActive", "PatientId", "SpecializationId" },
                values: new object[] { 1, true, 1, 1 });

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "Id", "DateOfBirth", "Discriminator", "Email", "Gender", "LicenseNumber", "Name", "Password", "Phone", "ProfileImage", "Role", "SpecializationId", "Surname", "Username" },
                values: new object[,]
                {
                    { 2, new DateTime(1967, 7, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), "Doctor", "miki@gmail.com", 0, "123dr2009", "Miki", "123", "0691202148", "", 1, 1, "Mikic", "miki@gmail.com" },
                    { 7, new DateTime(1990, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Doctor", "anja@gmail.com", 1, "14567sd8", "Anja", "123", "0604489354", "", 1, 1, "Ilic", "anja@gmail.com" },
                    { 3, new DateTime(1991, 2, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "Doctor", "roki@gmail.com", 0, "198r2009", "Roki", "123", "06312909304", "", 1, 2, "Rokic", "roki@gmail.com" },
                    { 6, new DateTime(1995, 5, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "Doctor", "dunja@gmail.com", 1, "138r2014", "Dunja", "123", "0656757304", "", 1, 6, "Jovanovic", "dunja@gmail.com" }
                });

            migrationBuilder.InsertData(
                table: "examinations",
                columns: new[] { "Id", "Date", "DiagnosisCode", "DiagnosisDescription", "DoctorId", "HealthDataId", "PatientId", "Prescription" },
                values: new object[] { 1, new DateTime(2023, 1, 24, 10, 0, 0, 0, DateTimeKind.Unspecified), "1AFA", "Dijabetes tipa 2", 2, 1, 1, "Ishrana za dijabeticare." });

            migrationBuilder.CreateIndex(
                name: "IX_examinations_HealthDataId",
                table: "examinations",
                column: "HealthDataId");

            migrationBuilder.CreateIndex(
                name: "IX_healthdata_PatientId",
                table: "healthdata",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_menstrualdata_PatientId",
                table: "menstrualdata",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_referralletters_SpecializationId",
                table: "referralletters",
                column: "SpecializationId");

            migrationBuilder.CreateIndex(
                name: "IX_users_SpecializationId",
                table: "users",
                column: "SpecializationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "appointments");

            migrationBuilder.DropTable(
                name: "blogs");

            migrationBuilder.DropTable(
                name: "blooddonationnotifications");

            migrationBuilder.DropTable(
                name: "examinations");

            migrationBuilder.DropTable(
                name: "menstrualdata");

            migrationBuilder.DropTable(
                name: "notifications");

            migrationBuilder.DropTable(
                name: "referralletters");

            migrationBuilder.DropTable(
                name: "rooms");

            migrationBuilder.DropTable(
                name: "healthdata");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "specializations");
        }
    }
}
