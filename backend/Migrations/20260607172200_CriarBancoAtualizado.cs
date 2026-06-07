using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace curriculumApi.Migrations
{
    /// <inheritdoc />
    public partial class CriarBancoAtualizado : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Curriculum",
                table: "Curriculum");

            migrationBuilder.RenameTable(
                name: "Curriculum",
                newName: "Curricula");

            migrationBuilder.AddColumn<string>(
                name: "PersonalInfo_Email",
                table: "Curricula",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PersonalInfo_FullName",
                table: "Curricula",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "PersonalInfo_Id",
                table: "Curricula",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "PersonalInfo_Phone",
                table: "Curricula",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PersonalInfo_Summary",
                table: "Curricula",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Curricula",
                table: "Curricula",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Educations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CurriculumModelId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Institution = table.Column<string>(type: "TEXT", nullable: false),
                    Degree = table.Column<string>(type: "TEXT", nullable: false),
                    Start = table.Column<DateTime>(type: "TEXT", nullable: true),
                    End = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Description = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Educations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Educations_Curricula_CurriculumModelId",
                        column: x => x.CurriculumModelId,
                        principalTable: "Curricula",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Experiences",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CurriculumModelId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Company = table.Column<string>(type: "TEXT", nullable: false),
                    Role = table.Column<string>(type: "TEXT", nullable: false),
                    Start = table.Column<DateTime>(type: "TEXT", nullable: true),
                    End = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Description = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Experiences", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Experiences_Curricula_CurriculumModelId",
                        column: x => x.CurriculumModelId,
                        principalTable: "Curricula",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Skills",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CurriculumModelId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Level = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Skills", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Skills_Curricula_CurriculumModelId",
                        column: x => x.CurriculumModelId,
                        principalTable: "Curricula",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Educations_CurriculumModelId",
                table: "Educations",
                column: "CurriculumModelId");

            migrationBuilder.CreateIndex(
                name: "IX_Experiences_CurriculumModelId",
                table: "Experiences",
                column: "CurriculumModelId");

            migrationBuilder.CreateIndex(
                name: "IX_Skills_CurriculumModelId",
                table: "Skills",
                column: "CurriculumModelId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Educations");

            migrationBuilder.DropTable(
                name: "Experiences");

            migrationBuilder.DropTable(
                name: "Skills");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Curricula",
                table: "Curricula");

            migrationBuilder.DropColumn(
                name: "PersonalInfo_Email",
                table: "Curricula");

            migrationBuilder.DropColumn(
                name: "PersonalInfo_FullName",
                table: "Curricula");

            migrationBuilder.DropColumn(
                name: "PersonalInfo_Id",
                table: "Curricula");

            migrationBuilder.DropColumn(
                name: "PersonalInfo_Phone",
                table: "Curricula");

            migrationBuilder.DropColumn(
                name: "PersonalInfo_Summary",
                table: "Curricula");

            migrationBuilder.RenameTable(
                name: "Curricula",
                newName: "Curriculum");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Curriculum",
                table: "Curriculum",
                column: "Id");
        }
    }
}
