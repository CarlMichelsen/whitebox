﻿// <auto-generated />
using System;
using Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Api.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    partial class ApplicationContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("white_box")
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Database.Entity.ChatConfigurationEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("DefaultSystemMessage")
                        .HasMaxLength(102400)
                        .HasColumnType("character varying(102400)")
                        .HasColumnName("default_system_message");

                    b.Property<int>("MaxTokens")
                        .HasColumnType("integer")
                        .HasColumnName("max_tokens");

                    b.Property<string>("SelectedModelIdentifier")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)")
                        .HasColumnName("selected_model_identifier");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint")
                        .HasColumnName("user_id");

                    b.HasKey("Id")
                        .HasName("pk_chat_configuration");

                    b.ToTable("chat_configuration", "white_box");
                });

            modelBuilder.Entity("Database.Entity.ContentEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<Guid>("MessageId")
                        .HasColumnType("uuid")
                        .HasColumnName("message_id");

                    b.Property<int>("SortOrder")
                        .HasColumnType("integer")
                        .HasColumnName("sort_order");

                    b.Property<int>("Type")
                        .HasColumnType("integer")
                        .HasColumnName("type");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasMaxLength(102400)
                        .HasColumnType("character varying(102400)")
                        .HasColumnName("value");

                    b.HasKey("Id")
                        .HasName("pk_content");

                    b.HasIndex("MessageId")
                        .HasDatabaseName("ix_content_message_id");

                    b.ToTable("content", "white_box");
                });

            modelBuilder.Entity("Database.Entity.ConversationEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateTime>("CreatedUtc")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_utc");

                    b.Property<long>("CreatorId")
                        .HasColumnType("bigint")
                        .HasColumnName("creator_id");

                    b.Property<DateTime?>("DeletedAtUtc")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("deleted_at_utc");

                    b.Property<DateTime>("LastAlteredUtc")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("last_altered_utc");

                    b.Property<Guid?>("LastAppendedMessageId")
                        .HasColumnType("uuid")
                        .HasColumnName("last_appended_message_id");

                    b.Property<string>("Summary")
                        .HasMaxLength(1024)
                        .HasColumnType("character varying(1024)")
                        .HasColumnName("summary");

                    b.Property<string>("SystemMessage")
                        .HasMaxLength(102400)
                        .HasColumnType("character varying(102400)")
                        .HasColumnName("system_message");

                    b.HasKey("Id")
                        .HasName("pk_conversation");

                    b.HasIndex("CreatorId")
                        .HasDatabaseName("ix_conversation_creator_id");

                    b.HasIndex("DeletedAtUtc")
                        .HasDatabaseName("ix_conversation_deleted_at_utc")
                        .HasFilter("\"conversation\".\"deleted_at_utc\" IS NULL");

                    b.HasIndex("LastAppendedMessageId")
                        .HasDatabaseName("ix_conversation_last_appended_message_id");

                    b.ToTable("conversation", "white_box");
                });

            modelBuilder.Entity("Database.Entity.MessageEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<Guid>("ConversationId")
                        .HasColumnType("uuid")
                        .HasColumnName("conversation_id");

                    b.Property<DateTime>("CreatedUtc")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_utc");

                    b.Property<DateTime?>("DeletedAtUtc")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("deleted_at_utc");

                    b.Property<Guid?>("PreviousMessageId")
                        .HasColumnType("uuid")
                        .HasColumnName("previous_message_id");

                    b.Property<Guid?>("PromptId")
                        .HasColumnType("uuid")
                        .HasColumnName("prompt_id");

                    b.HasKey("Id")
                        .HasName("pk_message");

                    b.HasIndex("ConversationId")
                        .HasDatabaseName("ix_message_conversation_id");

                    b.HasIndex("DeletedAtUtc")
                        .HasDatabaseName("ix_message_deleted_at_utc")
                        .HasFilter("\"message\".\"deleted_at_utc\" IS NULL");

                    b.HasIndex("PreviousMessageId")
                        .HasDatabaseName("ix_message_previous_message_id");

                    b.HasIndex("PromptId")
                        .HasDatabaseName("ix_message_prompt_id");

                    b.ToTable("message", "white_box");
                });

            modelBuilder.Entity("Database.Entity.PromptEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("PromptJson")
                        .IsRequired()
                        .HasMaxLength(204800)
                        .HasColumnType("jsonb")
                        .HasColumnName("prompt_json");

                    b.Property<DateTime>("PromptUtc")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("prompt_utc");

                    b.Property<bool>("Stream")
                        .HasColumnType("boolean")
                        .HasColumnName("stream");

                    b.Property<Guid?>("UsageId")
                        .HasColumnType("uuid")
                        .HasColumnName("usage_id");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint")
                        .HasColumnName("user_id");

                    b.HasKey("Id")
                        .HasName("pk_prompt");

                    b.HasIndex("UsageId")
                        .IsUnique()
                        .HasDatabaseName("ix_prompt_usage_id");

                    b.HasIndex("UserId")
                        .HasDatabaseName("ix_prompt_user_id");

                    b.ToTable("prompt", "white_box");
                });

            modelBuilder.Entity("Database.Entity.RedirectEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateTime>("RedirectedAtUtc")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("redirected_at_utc");

                    b.Property<Guid>("SourceId")
                        .HasColumnType("uuid")
                        .HasColumnName("source_id");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasMaxLength(80001)
                        .HasColumnType("character varying(80001)")
                        .HasColumnName("url");

                    b.Property<long?>("UserId")
                        .HasColumnType("bigint")
                        .HasColumnName("user_id");

                    b.HasKey("Id")
                        .HasName("pk_redirect");

                    b.HasIndex("UserId")
                        .HasDatabaseName("ix_redirect_user_id");

                    b.ToTable("redirect", "white_box");
                });

            modelBuilder.Entity("Database.Entity.UsageEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateTime>("CompleteUtc")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("complete_utc");

                    b.Property<string>("Completion")
                        .IsRequired()
                        .HasMaxLength(102400)
                        .HasColumnType("character varying(102400)")
                        .HasColumnName("completion");

                    b.Property<string>("InitialModelIdentifier")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)")
                        .HasColumnName("initial_model_identifier");

                    b.Property<int>("InputTokens")
                        .HasColumnType("integer")
                        .HasColumnName("input_tokens");

                    b.Property<int>("OutputTokens")
                        .HasColumnType("integer")
                        .HasColumnName("output_tokens");

                    b.Property<Guid>("PromptId")
                        .HasColumnType("uuid")
                        .HasColumnName("prompt_id");

                    b.Property<string>("Provider")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)")
                        .HasColumnName("provider");

                    b.Property<string>("SpecificModelIdentifier")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)")
                        .HasColumnName("specific_model_identifier");

                    b.HasKey("Id")
                        .HasName("pk_usage");

                    b.HasIndex("PromptId")
                        .IsUnique()
                        .HasDatabaseName("ix_usage_prompt_id");

                    b.ToTable("usage", "white_box");
                });

            modelBuilder.Entity("Database.Entity.UserEntity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("AuthenticationId")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)")
                        .HasColumnName("authentication_id");

                    b.Property<string>("AuthenticationMethod")
                        .IsRequired()
                        .HasMaxLength(512)
                        .HasColumnType("character varying(512)")
                        .HasColumnName("authentication_method");

                    b.Property<Guid>("ChatConfigurationId")
                        .HasColumnType("uuid")
                        .HasColumnName("chat_configuration_id");

                    b.Property<DateTime>("FirstLoginUtc")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("first_login_utc");

                    b.HasKey("Id")
                        .HasName("pk_user");

                    b.HasIndex("ChatConfigurationId")
                        .IsUnique()
                        .HasDatabaseName("ix_user_chat_configuration_id");

                    b.ToTable("user", "white_box");
                });

            modelBuilder.Entity("Database.Entity.ContentEntity", b =>
                {
                    b.HasOne("Database.Entity.MessageEntity", "Message")
                        .WithMany("Content")
                        .HasForeignKey("MessageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_content_message_message_id");

                    b.Navigation("Message");
                });

            modelBuilder.Entity("Database.Entity.ConversationEntity", b =>
                {
                    b.HasOne("Database.Entity.UserEntity", "Creator")
                        .WithMany("Conversations")
                        .HasForeignKey("CreatorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_conversation_user_creator_id");

                    b.HasOne("Database.Entity.MessageEntity", "LastAppendedMessage")
                        .WithMany()
                        .HasForeignKey("LastAppendedMessageId")
                        .HasConstraintName("fk_conversation_message_last_appended_message_id");

                    b.Navigation("Creator");

                    b.Navigation("LastAppendedMessage");
                });

            modelBuilder.Entity("Database.Entity.MessageEntity", b =>
                {
                    b.HasOne("Database.Entity.ConversationEntity", "Conversation")
                        .WithMany("Messages")
                        .HasForeignKey("ConversationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_message_conversation_conversation_id");

                    b.HasOne("Database.Entity.MessageEntity", "PreviousMessage")
                        .WithMany("NextMessages")
                        .HasForeignKey("PreviousMessageId")
                        .HasConstraintName("fk_message_message_previous_message_id");

                    b.HasOne("Database.Entity.PromptEntity", "Prompt")
                        .WithMany()
                        .HasForeignKey("PromptId")
                        .HasConstraintName("fk_message_prompt_prompt_id");

                    b.Navigation("Conversation");

                    b.Navigation("PreviousMessage");

                    b.Navigation("Prompt");
                });

            modelBuilder.Entity("Database.Entity.PromptEntity", b =>
                {
                    b.HasOne("Database.Entity.UsageEntity", "Usage")
                        .WithOne()
                        .HasForeignKey("Database.Entity.PromptEntity", "UsageId")
                        .HasConstraintName("fk_prompt_usage_usage_id");

                    b.HasOne("Database.Entity.UserEntity", "User")
                        .WithMany("Prompts")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_prompt_user_user_id");

                    b.Navigation("Usage");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Database.Entity.RedirectEntity", b =>
                {
                    b.HasOne("Database.Entity.UserEntity", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .HasConstraintName("fk_redirect_user_user_id");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Database.Entity.UsageEntity", b =>
                {
                    b.HasOne("Database.Entity.PromptEntity", "Prompt")
                        .WithOne()
                        .HasForeignKey("Database.Entity.UsageEntity", "PromptId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_usage_prompt_prompt_id");

                    b.Navigation("Prompt");
                });

            modelBuilder.Entity("Database.Entity.UserEntity", b =>
                {
                    b.HasOne("Database.Entity.ChatConfigurationEntity", "ChatConfiguration")
                        .WithOne("User")
                        .HasForeignKey("Database.Entity.UserEntity", "ChatConfigurationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_user_chat_configuration_chat_configuration_id");

                    b.Navigation("ChatConfiguration");
                });

            modelBuilder.Entity("Database.Entity.ChatConfigurationEntity", b =>
                {
                    b.Navigation("User");
                });

            modelBuilder.Entity("Database.Entity.ConversationEntity", b =>
                {
                    b.Navigation("Messages");
                });

            modelBuilder.Entity("Database.Entity.MessageEntity", b =>
                {
                    b.Navigation("Content");

                    b.Navigation("NextMessages");
                });

            modelBuilder.Entity("Database.Entity.UserEntity", b =>
                {
                    b.Navigation("Conversations");

                    b.Navigation("Prompts");
                });
#pragma warning restore 612, 618
        }
    }
}
