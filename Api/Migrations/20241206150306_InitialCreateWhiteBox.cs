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
                    FirstLoginUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
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

            migrationBuilder.CreateTable(
                name: "Content",
                schema: "whitebox",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    MessageId = table.Column<Guid>(type: "uuid", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    Value = table.Column<string>(type: "character varying(102400)", maxLength: 102400, nullable: false),
                    SortOrder = table.Column<int>(type: "integer", nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Content", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Conversation",
                schema: "whitebox",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastAppendedUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastAppendedMessageId = table.Column<Guid>(type: "uuid", nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Conversation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Conversation_User_CreatorId",
                        column: x => x.CreatorId,
                        principalSchema: "whitebox",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Message",
                schema: "whitebox",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ConversationId = table.Column<Guid>(type: "uuid", nullable: false),
                    PromptId = table.Column<Guid>(type: "uuid", nullable: true),
                    PreviousMessageId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Message", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Message_Conversation_ConversationId",
                        column: x => x.ConversationId,
                        principalSchema: "whitebox",
                        principalTable: "Conversation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Message_Message_PreviousMessageId",
                        column: x => x.PreviousMessageId,
                        principalSchema: "whitebox",
                        principalTable: "Message",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Prompt",
                schema: "whitebox",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UsageId = table.Column<Guid>(type: "uuid", nullable: true),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    PromptJson = table.Column<string>(type: "jsonb", maxLength: 204800, nullable: false),
                    PromptUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Stream = table.Column<bool>(type: "boolean", nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prompt", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Prompt_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "whitebox",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Usage",
                schema: "whitebox",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Provider = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    ModelIdentifier = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    PromptId = table.Column<Guid>(type: "uuid", nullable: false),
                    Completion = table.Column<string>(type: "character varying(102400)", maxLength: 102400, nullable: false),
                    InputTokens = table.Column<int>(type: "integer", nullable: false),
                    OutputTokens = table.Column<int>(type: "integer", nullable: false),
                    CompleteUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Usage_Prompt_PromptId",
                        column: x => x.PromptId,
                        principalSchema: "whitebox",
                        principalTable: "Prompt",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Content_MessageId",
                schema: "whitebox",
                table: "Content",
                column: "MessageId");

            migrationBuilder.CreateIndex(
                name: "IX_Conversation_CreatorId",
                schema: "whitebox",
                table: "Conversation",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Conversation_LastAppendedMessageId",
                schema: "whitebox",
                table: "Conversation",
                column: "LastAppendedMessageId");

            migrationBuilder.CreateIndex(
                name: "IX_Message_ConversationId",
                schema: "whitebox",
                table: "Message",
                column: "ConversationId");

            migrationBuilder.CreateIndex(
                name: "IX_Message_PreviousMessageId",
                schema: "whitebox",
                table: "Message",
                column: "PreviousMessageId");

            migrationBuilder.CreateIndex(
                name: "IX_Message_PromptId",
                schema: "whitebox",
                table: "Message",
                column: "PromptId");

            migrationBuilder.CreateIndex(
                name: "IX_Prompt_UsageId",
                schema: "whitebox",
                table: "Prompt",
                column: "UsageId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Prompt_UserId",
                schema: "whitebox",
                table: "Prompt",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Usage_PromptId",
                schema: "whitebox",
                table: "Usage",
                column: "PromptId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_ChatConfigurationId",
                schema: "whitebox",
                table: "User",
                column: "ChatConfigurationId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Content_Message_MessageId",
                schema: "whitebox",
                table: "Content",
                column: "MessageId",
                principalSchema: "whitebox",
                principalTable: "Message",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Conversation_Message_LastAppendedMessageId",
                schema: "whitebox",
                table: "Conversation",
                column: "LastAppendedMessageId",
                principalSchema: "whitebox",
                principalTable: "Message",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Message_Prompt_PromptId",
                schema: "whitebox",
                table: "Message",
                column: "PromptId",
                principalSchema: "whitebox",
                principalTable: "Prompt",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Prompt_Usage_UsageId",
                schema: "whitebox",
                table: "Prompt",
                column: "UsageId",
                principalSchema: "whitebox",
                principalTable: "Usage",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Conversation_Message_LastAppendedMessageId",
                schema: "whitebox",
                table: "Conversation");

            migrationBuilder.DropForeignKey(
                name: "FK_Prompt_User_UserId",
                schema: "whitebox",
                table: "Prompt");

            migrationBuilder.DropForeignKey(
                name: "FK_Usage_Prompt_PromptId",
                schema: "whitebox",
                table: "Usage");

            migrationBuilder.DropTable(
                name: "Content",
                schema: "whitebox");

            migrationBuilder.DropTable(
                name: "Message",
                schema: "whitebox");

            migrationBuilder.DropTable(
                name: "Conversation",
                schema: "whitebox");

            migrationBuilder.DropTable(
                name: "User",
                schema: "whitebox");

            migrationBuilder.DropTable(
                name: "ChatConfiguration",
                schema: "whitebox");

            migrationBuilder.DropTable(
                name: "Prompt",
                schema: "whitebox");

            migrationBuilder.DropTable(
                name: "Usage",
                schema: "whitebox");
        }
    }
}
