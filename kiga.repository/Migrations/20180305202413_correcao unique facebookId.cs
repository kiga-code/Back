using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace kiga.repository.Migrations
{
    public partial class correcaouniquefacebookId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "token",
                table: "Usuarios",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "facebookId",
                table: "Usuarios",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_facebookId",
                table: "Usuarios",
                column: "facebookId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Usuarios_facebookId",
                table: "Usuarios");

            migrationBuilder.AlterColumn<string>(
                name: "token",
                table: "Usuarios",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "facebookId",
                table: "Usuarios",
                nullable: false,
                oldClrType: typeof(string));
        }
    }
}
