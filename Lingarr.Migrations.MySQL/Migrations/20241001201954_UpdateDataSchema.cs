﻿using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lingarr.Migrations.MySQL.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDataSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "media");

            migrationBuilder.AlterColumn<string>(
                name: "path",
                table: "seasons",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "media_hash",
                table: "movies",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
            migrationBuilder.Sql("ALTER TABLE movies MODIFY COLUMN media_hash longtext AFTER `path`;");

            migrationBuilder.AddColumn<string>(
                name: "media_hash",
                table: "episodes",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
            migrationBuilder.Sql("ALTER TABLE episodes MODIFY COLUMN media_hash longtext AFTER `path`;");
            
            migrationBuilder.CreateTable(
                name: "images",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    type = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    path = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    show_id = table.Column<int>(type: "int", nullable: true),
                    movie_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_images", x => x.id);
                    table.ForeignKey(
                        name: "fk_images_movies_movie_id",
                        column: x => x.movie_id,
                        principalTable: "movies",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_images_shows_show_id",
                        column: x => x.show_id,
                        principalTable: "shows",
                        principalColumn: "id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "ix_images_movie_id",
                table: "images",
                column: "movie_id");

            migrationBuilder.CreateIndex(
                name: "ix_images_show_id",
                table: "images",
                column: "show_id");
            
            migrationBuilder.InsertData(
                table: "settings",
                columns: new[] { "key", "value" },
                values: new object[,]
                {
                    { "automation_enabled", "false" },
                    { "service_type", "libretranslate" },
                    { "libretranslate_url", Environment.GetEnvironmentVariable("LIBRETRANSLATE_API") ?? "http://libretranslate:5000" },
                    { "max_translations_per_run", "10" },
                    { "deepl_api_key", "" },
                    { "translation_schedule", "0 4 * * *" },
                    { "translation_cycle", "false" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "images");

            migrationBuilder.DropColumn(
                name: "media_hash",
                table: "movies");

            migrationBuilder.DropColumn(
                name: "media_hash",
                table: "episodes");

            migrationBuilder.UpdateData(
                table: "seasons",
                keyColumn: "path",
                keyValue: null,
                column: "path",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "path",
                table: "seasons",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "media",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    movie_id = table.Column<int>(type: "int", nullable: true),
                    show_id = table.Column<int>(type: "int", nullable: true),
                    path = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    type = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_media", x => x.id);
                    table.ForeignKey(
                        name: "fk_media_movies_movie_id",
                        column: x => x.movie_id,
                        principalTable: "movies",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_media_shows_show_id",
                        column: x => x.show_id,
                        principalTable: "shows",
                        principalColumn: "id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "ix_media_movie_id",
                table: "media",
                column: "movie_id");

            migrationBuilder.CreateIndex(
                name: "ix_media_show_id",
                table: "media",
                column: "show_id");
            
            migrationBuilder.DeleteData(
                table: "settings",
                keyColumn: "key",
                keyValues: new object[]
                {
                    "automation_enabled",
                    "service_type",
                    "libretranslate_url",
                    "max_translations_per_run",
                    "deepl_api_key",
                    "translation_schedule",
                    "translation_cycle"
                });
        }
    }
}