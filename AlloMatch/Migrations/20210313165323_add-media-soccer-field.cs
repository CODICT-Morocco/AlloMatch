using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AlloMatch.Migrations
{
    public partial class addmediasoccerfield : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Media_Organisation_OrganisationId",
                table: "Media");

            migrationBuilder.DropForeignKey(
                name: "FK_Media_SoccerField_SoccerFieldId",
                table: "Media");

            migrationBuilder.DropIndex(
                name: "IX_Media_OrganisationId",
                table: "Media");

            migrationBuilder.DropIndex(
                name: "IX_Media_SoccerFieldId",
                table: "Media");

            migrationBuilder.DropColumn(
                name: "OrganisationId",
                table: "Media");

            migrationBuilder.DropColumn(
                name: "SoccerFieldId",
                table: "Media");

            migrationBuilder.CreateTable(
                name: "SoccerFieldMedia",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    SoccierFieldId = table.Column<long>(type: "bigint", nullable: false),
                    MediaId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedBy = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SoccerFieldMedia", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SoccerFieldMedia_Media_MediaId",
                        column: x => x.MediaId,
                        principalTable: "Media",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SoccerFieldMedia_SoccerField_SoccierFieldId",
                        column: x => x.SoccierFieldId,
                        principalTable: "SoccerField",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SoccerFieldMedia_MediaId",
                table: "SoccerFieldMedia",
                column: "MediaId");

            migrationBuilder.CreateIndex(
                name: "IX_SoccerFieldMedia_SoccierFieldId",
                table: "SoccerFieldMedia",
                column: "SoccierFieldId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SoccerFieldMedia");

            migrationBuilder.AddColumn<long>(
                name: "OrganisationId",
                table: "Media",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "SoccerFieldId",
                table: "Media",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Media_OrganisationId",
                table: "Media",
                column: "OrganisationId");

            migrationBuilder.CreateIndex(
                name: "IX_Media_SoccerFieldId",
                table: "Media",
                column: "SoccerFieldId");

            migrationBuilder.AddForeignKey(
                name: "FK_Media_Organisation_OrganisationId",
                table: "Media",
                column: "OrganisationId",
                principalTable: "Organisation",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Media_SoccerField_SoccerFieldId",
                table: "Media",
                column: "SoccerFieldId",
                principalTable: "SoccerField",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
