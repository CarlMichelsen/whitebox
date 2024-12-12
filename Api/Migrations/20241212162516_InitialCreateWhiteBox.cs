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
                name: "white_box");

            migrationBuilder.CreateTable(
                name: "chat_configuration",
                schema: "white_box",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<long>(type: "bigint", nullable: false),
                    default_system_message = table.Column<string>(type: "character varying(102400)", maxLength: 102400, nullable: true),
                    selected_model_identifier = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    max_tokens = table.Column<int>(type: "integer", nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_chat_configuration", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "user",
                schema: "white_box",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    authentication_method = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: false),
                    authentication_id = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    chat_configuration_id = table.Column<Guid>(type: "uuid", nullable: false),
                    first_login_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user", x => x.id);
                    table.ForeignKey(
                        name: "fk_user_chat_configuration_chat_configuration_id",
                        column: x => x.chat_configuration_id,
                        principalSchema: "white_box",
                        principalTable: "chat_configuration",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "content",
                schema: "white_box",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    message_id = table.Column<Guid>(type: "uuid", nullable: false),
                    type = table.Column<int>(type: "integer", nullable: false),
                    value = table.Column<string>(type: "character varying(102400)", maxLength: 102400, nullable: false),
                    sort_order = table.Column<int>(type: "integer", nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_content", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "conversation",
                schema: "white_box",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    summary = table.Column<string>(type: "character varying(1024)", maxLength: 1024, nullable: true),
                    system_message = table.Column<string>(type: "character varying(102400)", maxLength: 102400, nullable: true),
                    creator_id = table.Column<long>(type: "bigint", nullable: false),
                    created_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    last_altered_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    last_appended_message_id = table.Column<Guid>(type: "uuid", nullable: true),
                    deleted_at_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_conversation", x => x.id);
                    table.ForeignKey(
                        name: "fk_conversation_user_creator_id",
                        column: x => x.creator_id,
                        principalSchema: "white_box",
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "message",
                schema: "white_box",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    conversation_id = table.Column<Guid>(type: "uuid", nullable: false),
                    prompt_id = table.Column<Guid>(type: "uuid", nullable: true),
                    previous_message_id = table.Column<Guid>(type: "uuid", nullable: true),
                    created_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    deleted_at_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_message", x => x.id);
                    table.ForeignKey(
                        name: "fk_message_conversation_conversation_id",
                        column: x => x.conversation_id,
                        principalSchema: "white_box",
                        principalTable: "conversation",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_message_message_previous_message_id",
                        column: x => x.previous_message_id,
                        principalSchema: "white_box",
                        principalTable: "message",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "prompt",
                schema: "white_box",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    usage_id = table.Column<Guid>(type: "uuid", nullable: true),
                    user_id = table.Column<long>(type: "bigint", nullable: false),
                    prompt_json = table.Column<string>(type: "jsonb", maxLength: 204800, nullable: false),
                    prompt_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    stream = table.Column<bool>(type: "boolean", nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_prompt", x => x.id);
                    table.ForeignKey(
                        name: "fk_prompt_user_user_id",
                        column: x => x.user_id,
                        principalSchema: "white_box",
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "usage",
                schema: "white_box",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    provider = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    initial_model_identifier = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    specific_model_identifier = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    prompt_id = table.Column<Guid>(type: "uuid", nullable: false),
                    completion = table.Column<string>(type: "character varying(102400)", maxLength: 102400, nullable: false),
                    input_tokens = table.Column<int>(type: "integer", nullable: false),
                    output_tokens = table.Column<int>(type: "integer", nullable: false),
                    complete_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_usage", x => x.id);
                    table.ForeignKey(
                        name: "fk_usage_prompt_prompt_id",
                        column: x => x.prompt_id,
                        principalSchema: "white_box",
                        principalTable: "prompt",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_content_message_id",
                schema: "white_box",
                table: "content",
                column: "message_id");

            migrationBuilder.CreateIndex(
                name: "ix_conversation_creator_id",
                schema: "white_box",
                table: "conversation",
                column: "creator_id");

            migrationBuilder.CreateIndex(
                name: "ix_conversation_deleted_at_utc",
                schema: "white_box",
                table: "conversation",
                column: "deleted_at_utc",
                filter: "\"conversation\".\"deleted_at_utc\" IS NULL");

            migrationBuilder.CreateIndex(
                name: "ix_conversation_last_appended_message_id",
                schema: "white_box",
                table: "conversation",
                column: "last_appended_message_id");

            migrationBuilder.CreateIndex(
                name: "ix_message_conversation_id",
                schema: "white_box",
                table: "message",
                column: "conversation_id");

            migrationBuilder.CreateIndex(
                name: "ix_message_deleted_at_utc",
                schema: "white_box",
                table: "message",
                column: "deleted_at_utc",
                filter: "\"message\".\"deleted_at_utc\" IS NULL");

            migrationBuilder.CreateIndex(
                name: "ix_message_previous_message_id",
                schema: "white_box",
                table: "message",
                column: "previous_message_id");

            migrationBuilder.CreateIndex(
                name: "ix_message_prompt_id",
                schema: "white_box",
                table: "message",
                column: "prompt_id");

            migrationBuilder.CreateIndex(
                name: "ix_prompt_usage_id",
                schema: "white_box",
                table: "prompt",
                column: "usage_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_prompt_user_id",
                schema: "white_box",
                table: "prompt",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_usage_prompt_id",
                schema: "white_box",
                table: "usage",
                column: "prompt_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_user_chat_configuration_id",
                schema: "white_box",
                table: "user",
                column: "chat_configuration_id",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "fk_content_message_message_id",
                schema: "white_box",
                table: "content",
                column: "message_id",
                principalSchema: "white_box",
                principalTable: "message",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_conversation_message_last_appended_message_id",
                schema: "white_box",
                table: "conversation",
                column: "last_appended_message_id",
                principalSchema: "white_box",
                principalTable: "message",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_message_prompt_prompt_id",
                schema: "white_box",
                table: "message",
                column: "prompt_id",
                principalSchema: "white_box",
                principalTable: "prompt",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_prompt_usage_usage_id",
                schema: "white_box",
                table: "prompt",
                column: "usage_id",
                principalSchema: "white_box",
                principalTable: "usage",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_conversation_message_last_appended_message_id",
                schema: "white_box",
                table: "conversation");

            migrationBuilder.DropForeignKey(
                name: "fk_prompt_user_user_id",
                schema: "white_box",
                table: "prompt");

            migrationBuilder.DropForeignKey(
                name: "fk_usage_prompt_prompt_id",
                schema: "white_box",
                table: "usage");

            migrationBuilder.DropTable(
                name: "content",
                schema: "white_box");

            migrationBuilder.DropTable(
                name: "message",
                schema: "white_box");

            migrationBuilder.DropTable(
                name: "conversation",
                schema: "white_box");

            migrationBuilder.DropTable(
                name: "user",
                schema: "white_box");

            migrationBuilder.DropTable(
                name: "chat_configuration",
                schema: "white_box");

            migrationBuilder.DropTable(
                name: "prompt",
                schema: "white_box");

            migrationBuilder.DropTable(
                name: "usage",
                schema: "white_box");
        }
    }
}