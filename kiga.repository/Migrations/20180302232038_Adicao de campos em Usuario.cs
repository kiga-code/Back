using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace kiga.repository.Migrations
{
    public partial class AdicaodecamposemUsuario : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IdUsuario",
                table: "Usuarios",
                newName: "IdUser");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Usuarios",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "Usuarios",
                maxLength: 120,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserToken",
                table: "Usuarios",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "created_at",
                table: "Usuarios",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "UserToken",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "created_at",
                table: "Usuarios");

            migrationBuilder.RenameColumn(
                name: "IdUser",
                table: "Usuarios",
                newName: "IdUsuario");
        }
    }
}
