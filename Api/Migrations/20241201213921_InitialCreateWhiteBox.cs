using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Api.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreateWhiteBox : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "whitebox");

            migrationBuilder.CreateTable(
                name: "ChatConfiguration",
                schema: "whitebox",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    DefaultSystemMessage = table.Column<string>(type: "character varying(102400)", maxLength: 102400, nullable: true),
                    SelectedModelIdentifier = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    MaxTokens = table.Column<int>(type: "integer", nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatConfiguration", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                schema: "whitebox",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AuthenticationMethod = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: false),
                    AuthenticationId = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    ChatConfigurationId = table.Column<Guid>(type: "uuid", nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                    table.ForeignKey(
                        name: "FK_User_ChatConfiguration_ChatConfigurationId",
                        column: x => x.ChatConfigurationId,
                        principalSchema: "whitebox",
                        principalTable: "ChatConfiguration",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_User_ChatConfigurationId",
                schema: "whitebox",
                table: "User",
                column: "ChatConfigurationId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "User",
                schema: "whitebox");

            migrationBuilder.DropTable(
                name: "ChatConfiguration",
                schema: "whitebox");
        }
    }
}
