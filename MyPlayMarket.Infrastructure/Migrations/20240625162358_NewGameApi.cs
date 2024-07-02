using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyPlayMarket.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class NewGameApi : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Удаление зависимых объектов

            migrationBuilder.DropPrimaryKey(
                name: "PK_LocalUsers",
                table: "LocalUsers");

            // Создание временного столбца для копирования данных
            migrationBuilder.AddColumn<Guid>(
                name: "TempId",
                table: "LocalUsers",
                type: "uniqueidentifier",
                nullable: false,
                defaultValueSql: "NEWID()");

            // Копирование данных из старого столбца в новый временный
            migrationBuilder.Sql("UPDATE LocalUsers SET TempId = NEWID()");

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

            // Восстановление зависимых объектов

            migrationBuilder.AddColumn<string>(
                name: "PasswordHash",
                table: "LocalUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.DropColumn(
                 name: "Description",
                 table: "Games");

            migrationBuilder.RenameColumn(
                name: "Password",
                table: "LocalUsers",
                newName: "Surname");
            migrationBuilder.AlterColumn<double>(
                name: "Cost",
                table: "Games",
                type: "float",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateTable(
                name: "GameScreenshots",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GameId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameScreenshots", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GameScreenshots_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Genres",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genres", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Platforms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Platforms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GameGenre",
                columns: table => new
                {
                    GameId = table.Column<int>(type: "int", nullable: false),
                    GenreId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameGenre", x => new { x.GameId, x.GenreId });
                    table.ForeignKey(
                        name: "FK_GameGenre_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GameGenre_Genres_GenreId",
                        column: x => x.GenreId,
                        principalTable: "Genres",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GamePlatforms",
                columns: table => new
                {
                    GameId = table.Column<int>(type: "int", nullable: false),
                    PlatformId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GamePlatforms", x => new { x.GameId, x.PlatformId });
                    table.ForeignKey(
                        name: "FK_GamePlatforms_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GamePlatforms_Platforms_PlatformId",
                        column: x => x.PlatformId,
                        principalTable: "Platforms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GameTags",
                columns: table => new
                {
                    GameId = table.Column<int>(type: "int", nullable: false),
                    TagId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameTags", x => new { x.GameId, x.TagId });
                    table.ForeignKey(
                        name: "FK_GameTags_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GameTags_Tags_TagId",
                        column: x => x.TagId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GameGenre_GenreId",
                table: "GameGenre",
                column: "GenreId");

            migrationBuilder.CreateIndex(
                name: "IX_GamePlatforms_PlatformId",
                table: "GamePlatforms",
                column: "PlatformId");

            migrationBuilder.CreateIndex(
                name: "IX_GameScreenshots_GameId",
                table: "GameScreenshots",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_GameTags_TagId",
                table: "GameTags",
                column: "TagId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GameGenre");

            migrationBuilder.DropTable(
                name: "GamePlatforms");

            migrationBuilder.DropTable(
                name: "GameScreenshots");

            migrationBuilder.DropTable(
                name: "GameTags");

            migrationBuilder.DropTable(
                name: "Genres");

            migrationBuilder.DropTable(
                name: "Platforms");

            migrationBuilder.DropTable(
                name: "Tags");

            migrationBuilder.DropColumn(
                name: "PasswordHash",
                table: "LocalUsers");

            

            migrationBuilder.AddColumn<int>(
                name: "TempId",
                table: "LocalUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.Sql("UPDATE LocalUsers SET TempId = Id");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "LocalUsers");

            migrationBuilder.RenameColumn(
                name: "TempId",
                table: "LocalUsers",
                newName: "Id");

            migrationBuilder.AlterColumn<string>(
                name: "Cost",
                table: "Games",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

        }
    }

}
