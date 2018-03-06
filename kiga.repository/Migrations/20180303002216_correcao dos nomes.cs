using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace kiga.repository.Migrations
{
    public partial class correcaodosnomes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "Usuarios",
                newName: "lastName");

            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "Usuarios",
                newName: "firstName");

            migrationBuilder.RenameColumn(
                name: "FacebookId",
                table: "Usuarios",
                newName: "facebookId");

            migrationBuilder.RenameColumn(
                name: "UserToken",
                table: "Usuarios",
                newName: "token");

            migrationBuilder.RenameColumn(
                name: "IdUser",
                table: "Usuarios",
                newName: "id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "lastName",
                table: "Usuarios",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "firstName",
                table: "Usuarios",
                newName: "FirstName");

            migrationBuilder.RenameColumn(
                name: "facebookId",
                table: "Usuarios",
                newName: "FacebookId");

            migrationBuilder.RenameColumn(
                name: "token",
                table: "Usuarios",
                newName: "UserToken");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Usuarios",
                newName: "IdUser");
        }
    }
}
