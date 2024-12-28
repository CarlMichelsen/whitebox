using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Api.Migrations
{
    /// <inheritdoc />
    public partial class ContentSoftDeletableMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "deleted_at_utc",
                schema: "white_box",
                table: "content",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "ix_content_deleted_at_utc",
                schema: "white_box",
                table: "content",
                column: "deleted_at_utc",
                filter: "\"content\".\"deleted_at_utc\" IS NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_content_deleted_at_utc",
                schema: "white_box",
                table: "content");

            migrationBuilder.DropColumn(
                name: "deleted_at_utc",
                schema: "white_box",
                table: "content");
        }
    }
}
