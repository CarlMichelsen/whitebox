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
                name: "chat_configuration",
                schema: "whitebox",
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
                schema: "whitebox",
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
                        principalSchema: "whitebox",
                        principalTable: "chat_configuration",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "content",
                schema: "whitebox",
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
                schema: "whitebox",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    system_message = table.Column<string>(type: "character varying(102400)", maxLength: 102400, nullable: false),
                    creator_id = table.Column<long>(type: "bigint", nullable: false),
                    created_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    last_altered_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    last_appended_message_id = table.Column<Guid>(type: "uuid", nullable: true),
                    summary = table.Column<string>(type: "character varying(1024)", maxLength: 1024, nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_conversation", x => x.id);
                    table.ForeignKey(
                        name: "fk_conversation_user_creator_id",
                        column: x => x.creator_id,
                        principalSchema: "whitebox",
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "message",
                schema: "whitebox",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    conversation_id = table.Column<Guid>(type: "uuid", nullable: false),
                    prompt_id = table.Column<Guid>(type: "uuid", nullable: true),
                    previous_message_id = table.Column<Guid>(type: "uuid", nullable: true),
                    created_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_message", x => x.id);
                    table.ForeignKey(
                        name: "fk_message_conversation_conversation_id",
                        column: x => x.conversation_id,
                        principalSchema: "whitebox",
                        principalTable: "conversation",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_message_message_previous_message_id",
                        column: x => x.previous_message_id,
                        principalSchema: "whitebox",
                        principalTable: "message",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "prompt",
                schema: "whitebox",
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
                        principalSchema: "whitebox",
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "usage",
                schema: "whitebox",
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
                        principalSchema: "whitebox",
                        principalTable: "prompt",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_content_message_id",
                schema: "whitebox",
                table: "content",
                column: "message_id");

            migrationBuilder.CreateIndex(
                name: "ix_conversation_creator_id",
                schema: "whitebox",
                table: "conversation",
                column: "creator_id");

            migrationBuilder.CreateIndex(
                name: "ix_conversation_last_appended_message_id",
                schema: "whitebox",
                table: "conversation",
                column: "last_appended_message_id");

            migrationBuilder.CreateIndex(
                name: "ix_message_conversation_id",
                schema: "whitebox",
                table: "message",
                column: "conversation_id");

            migrationBuilder.CreateIndex(
                name: "ix_message_previous_message_id",
                schema: "whitebox",
                table: "message",
                column: "previous_message_id");

            migrationBuilder.CreateIndex(
                name: "ix_message_prompt_id",
                schema: "whitebox",
                table: "message",
                column: "prompt_id");

            migrationBuilder.CreateIndex(
                name: "ix_prompt_usage_id",
                schema: "whitebox",
                table: "prompt",
                column: "usage_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_prompt_user_id",
                schema: "whitebox",
                table: "prompt",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_usage_prompt_id",
                schema: "whitebox",
                table: "usage",
                column: "prompt_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_user_chat_configuration_id",
                schema: "whitebox",
                table: "user",
                column: "chat_configuration_id",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "fk_content_message_message_id",
                schema: "whitebox",
                table: "content",
                column: "message_id",
                principalSchema: "whitebox",
                principalTable: "message",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_conversation_message_last_appended_message_id",
                schema: "whitebox",
                table: "conversation",
                column: "last_appended_message_id",
                principalSchema: "whitebox",
                principalTable: "message",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_message_prompt_prompt_id",
                schema: "whitebox",
                table: "message",
                column: "prompt_id",
                principalSchema: "whitebox",
                principalTable: "prompt",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_prompt_usage_usage_id",
                schema: "whitebox",
                table: "prompt",
                column: "usage_id",
                principalSchema: "whitebox",
                principalTable: "usage",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_conversation_message_last_appended_message_id",
                schema: "whitebox",
                table: "conversation");

            migrationBuilder.DropForeignKey(
                name: "fk_prompt_user_user_id",
                schema: "whitebox",
                table: "prompt");

            migrationBuilder.DropForeignKey(
                name: "fk_usage_prompt_prompt_id",
                schema: "whitebox",
                table: "usage");

            migrationBuilder.DropTable(
                name: "content",
                schema: "whitebox");

            migrationBuilder.DropTable(
                name: "message",
                schema: "whitebox");

            migrationBuilder.DropTable(
                name: "conversation",
                schema: "whitebox");

            migrationBuilder.DropTable(
                name: "user",
                schema: "whitebox");

            migrationBuilder.DropTable(
                name: "chat_configuration",
                schema: "whitebox");

            migrationBuilder.DropTable(
                name: "prompt",
                schema: "whitebox");

            migrationBuilder.DropTable(
                name: "usage",
                schema: "whitebox");
        }
    }
}
