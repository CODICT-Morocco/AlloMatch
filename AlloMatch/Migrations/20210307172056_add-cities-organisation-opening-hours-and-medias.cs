using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AlloMatch.Migrations
{
    public partial class addcitiesorganisationopeninghoursandmedias : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Organisation_AspNetUsers_ApplicationUserId",
                table: "Organisation");

            migrationBuilder.AddColumn<long>(
                name: "ThumbNailId",
                table: "SoccerField",
                type: "bigint",
                nullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "ApplicationUserId",
                table: "Organisation",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Organisation",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "CityId",
                table: "Organisation",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<bool>(
                name: "IsDefault",
                table: "Organisation",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Latitude",
                table: "Organisation",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Longitude",
                table: "Organisation",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ThumbNailId",
                table: "Organisation",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "City",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    CreatedBy = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_City", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Media",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    FileName = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    Size = table.Column<double>(type: "double", nullable: false),
                    OrganisationId = table.Column<long>(type: "bigint", nullable: true),
                    SoccerFieldId = table.Column<long>(type: "bigint", nullable: true),
                    CreatedBy = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Media", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Media_Organisation_OrganisationId",
                        column: x => x.OrganisationId,
                        principalTable: "Organisation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Media_SoccerField_SoccerFieldId",
                        column: x => x.SoccerFieldId,
                        principalTable: "SoccerField",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OpeningHour",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    OrganisationId = table.Column<long>(type: "bigint", nullable: false),
                    WeekDay = table.Column<int>(type: "int", nullable: false),
                    From = table.Column<TimeSpan>(type: "time(6)", nullable: false),
                    To = table.Column<TimeSpan>(type: "time(6)", nullable: false),
                    CreatedBy = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OpeningHour", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OpeningHour_Organisation_OrganisationId",
                        column: x => x.OrganisationId,
                        principalTable: "Organisation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SoccerField_ThumbNailId",
                table: "SoccerField",
                column: "ThumbNailId");

            migrationBuilder.CreateIndex(
                name: "IX_Organisation_CityId",
                table: "Organisation",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Organisation_ThumbNailId",
                table: "Organisation",
                column: "ThumbNailId");

            migrationBuilder.CreateIndex(
                name: "IX_Media_OrganisationId",
                table: "Media",
                column: "OrganisationId");

            migrationBuilder.CreateIndex(
                name: "IX_Media_SoccerFieldId",
                table: "Media",
                column: "SoccerFieldId");

            migrationBuilder.CreateIndex(
                name: "IX_OpeningHour_OrganisationId",
                table: "OpeningHour",
                column: "OrganisationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Organisation_AspNetUsers_ApplicationUserId",
                table: "Organisation",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Organisation_City_CityId",
                table: "Organisation",
                column: "CityId",
                principalTable: "City",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Organisation_Media_ThumbNailId",
                table: "Organisation",
                column: "ThumbNailId",
                principalTable: "Media",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SoccerField_Media_ThumbNailId",
                table: "SoccerField",
                column: "ThumbNailId",
                principalTable: "Media",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Organisation_AspNetUsers_ApplicationUserId",
                table: "Organisation");

            migrationBuilder.DropForeignKey(
                name: "FK_Organisation_City_CityId",
                table: "Organisation");

            migrationBuilder.DropForeignKey(
                name: "FK_Organisation_Media_ThumbNailId",
                table: "Organisation");

            migrationBuilder.DropForeignKey(
                name: "FK_SoccerField_Media_ThumbNailId",
                table: "SoccerField");

            migrationBuilder.DropTable(
                name: "City");

            migrationBuilder.DropTable(
                name: "Media");

            migrationBuilder.DropTable(
                name: "OpeningHour");

            migrationBuilder.DropIndex(
                name: "IX_SoccerField_ThumbNailId",
                table: "SoccerField");

            migrationBuilder.DropIndex(
                name: "IX_Organisation_CityId",
                table: "Organisation");

            migrationBuilder.DropIndex(
                name: "IX_Organisation_ThumbNailId",
                table: "Organisation");

            migrationBuilder.DropColumn(
                name: "ThumbNailId",
                table: "SoccerField");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "Organisation");

            migrationBuilder.DropColumn(
                name: "CityId",
                table: "Organisation");

            migrationBuilder.DropColumn(
                name: "IsDefault",
                table: "Organisation");

            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "Organisation");

            migrationBuilder.DropColumn(
                name: "Longitude",
                table: "Organisation");

            migrationBuilder.DropColumn(
                name: "ThumbNailId",
                table: "Organisation");

            migrationBuilder.AlterColumn<long>(
                name: "ApplicationUserId",
                table: "Organisation",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddForeignKey(
                name: "FK_Organisation_AspNetUsers_ApplicationUserId",
                table: "Organisation",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
