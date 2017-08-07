using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Econoterm.Migrations
{
    public partial class Calculos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Calculos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Aislante = table.Column<int>(nullable: false),
                    Ambiental = table.Column<double>(nullable: false),
                    Astm = table.Column<bool>(nullable: false),
                    Diametro = table.Column<double>(nullable: false),
                    Emisividad = table.Column<double>(nullable: false),
                    Ener = table.Column<bool>(nullable: false),
                    Forma = table.Column<int>(nullable: false),
                    Operacion = table.Column<double>(nullable: false),
                    Superficial = table.Column<double>(nullable: false),
                    UserId = table.Column<string>(nullable: true),
                    Viento = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Calculos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Calculos_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Calculos_UserId",
                table: "Calculos",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Calculos");
        }
    }
}
