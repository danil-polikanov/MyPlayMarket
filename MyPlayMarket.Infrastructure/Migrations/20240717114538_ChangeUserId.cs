using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyPlayMarket.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangeUserId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Удаление зависимых объектов

            migrationBuilder.DropPrimaryKey(
                name: "PK_LocalUsers",
                table: "LocalUsers");

            // Создание временного столбца для копирования данных
            migrationBuilder.AddColumn<int>(
                name: "TempId",
                table: "LocalUsers",
                type: "int",
                nullable: false,
                defaultValue:0).Annotation("SqlServer:Identity", "1, 1");


            // Удаление старого столбца
            migrationBuilder.DropColumn(
                name: "Id",
                table: "LocalUsers");

            // Переименование временного столбца в старый
            migrationBuilder.RenameColumn(
                name: "TempId",
                table: "LocalUsers",
                newName: "Id");

            // Создание первичного ключа заново
            migrationBuilder.AddPrimaryKey(
                name: "PK_LocalUsers",
                table: "LocalUsers",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.AddColumn<Guid>(
                name: "TempId",
                table: "LocalUsers",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: "NEWID()");

            migrationBuilder.Sql("UPDATE LocalUsers SET TempId = Id");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "LocalUsers");

            migrationBuilder.RenameColumn(
                name: "TempId",
                table: "LocalUsers",
                newName: "Id");
        }
    }
}
