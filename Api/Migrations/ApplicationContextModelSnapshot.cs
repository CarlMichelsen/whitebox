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
                .HasDefaultSchema("whitebox")
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Database.Entity.ChatConfigurationEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<string>("DefaultSystemMessage")
                        .HasMaxLength(102400)
                        .HasColumnType("character varying(102400)");

                    b.Property<int>("MaxTokens")
                        .HasColumnType("integer");

                    b.Property<string>("SelectedModelIdentifier")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.ToTable("ChatConfiguration", "whitebox");
                });

            modelBuilder.Entity("Database.Entity.ContentEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<Guid>("MessageId")
                        .HasColumnType("uuid");

                    b.Property<int>("SortOrder")
                        .HasColumnType("integer");

                    b.Property<int>("Type")
                        .HasColumnType("integer");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasMaxLength(102400)
                        .HasColumnType("character varying(102400)");

                    b.HasKey("Id");

                    b.HasIndex("MessageId");

                    b.ToTable("Content", "whitebox");
                });

            modelBuilder.Entity("Database.Entity.ConversationEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedUtc")
                        .HasColumnType("timestamp with time zone");

                    b.Property<long>("CreatorId")
                        .HasColumnType("bigint");

                    b.Property<Guid?>("LastAppendedMessageId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("LastAppendedUtc")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("CreatorId");

                    b.HasIndex("LastAppendedMessageId");

                    b.ToTable("Conversation", "whitebox");
                });

            modelBuilder.Entity("Database.Entity.MessageEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<Guid>("ConversationId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedUtc")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid?>("PreviousMessageId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("PromptId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("ConversationId");

                    b.HasIndex("PreviousMessageId");

                    b.HasIndex("PromptId");

                    b.ToTable("Message", "whitebox");
                });

            modelBuilder.Entity("Database.Entity.PromptEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<string>("PromptJson")
                        .IsRequired()
                        .HasMaxLength(204800)
                        .HasColumnType("jsonb");

                    b.Property<DateTime>("PromptUtc")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Text")
                        .HasMaxLength(102400)
                        .HasColumnType("character varying(102400)");

                    b.Property<Guid?>("UsageId")
                        .HasColumnType("uuid");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("UsageId")
                        .IsUnique();

                    b.HasIndex("UserId");

                    b.ToTable("Prompt", "whitebox");
                });

            modelBuilder.Entity("Database.Entity.UsageEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CompleteUtc")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("InputTokens")
                        .HasColumnType("integer");

                    b.Property<string>("ModelIdentifier")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)");

                    b.Property<int>("OutputTokens")
                        .HasColumnType("integer");

                    b.Property<Guid>("PromptId")
                        .HasColumnType("uuid");

                    b.Property<string>("Provider")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)");

                    b.HasKey("Id");

                    b.HasIndex("PromptId")
                        .IsUnique();

                    b.ToTable("Usage", "whitebox");
                });

            modelBuilder.Entity("Database.Entity.UserEntity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("AuthenticationId")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)");

                    b.Property<string>("AuthenticationMethod")
                        .IsRequired()
                        .HasMaxLength(512)
                        .HasColumnType("character varying(512)");

                    b.Property<Guid>("ChatConfigurationId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("FirstLoginUtc")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("ChatConfigurationId")
                        .IsUnique();

                    b.ToTable("User", "whitebox");
                });

            modelBuilder.Entity("Database.Entity.ContentEntity", b =>
                {
                    b.HasOne("Database.Entity.MessageEntity", "Message")
                        .WithMany("Content")
                        .HasForeignKey("MessageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Message");
                });

            modelBuilder.Entity("Database.Entity.ConversationEntity", b =>
                {
                    b.HasOne("Database.Entity.UserEntity", "Creator")
                        .WithMany("Conversations")
                        .HasForeignKey("CreatorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Database.Entity.MessageEntity", "LastAppendedMessage")
                        .WithMany()
                        .HasForeignKey("LastAppendedMessageId");

                    b.Navigation("Creator");

                    b.Navigation("LastAppendedMessage");
                });

            modelBuilder.Entity("Database.Entity.MessageEntity", b =>
                {
                    b.HasOne("Database.Entity.ConversationEntity", "Conversation")
                        .WithMany("Messages")
                        .HasForeignKey("ConversationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Database.Entity.MessageEntity", "PreviousMessage")
                        .WithMany("NextMessages")
                        .HasForeignKey("PreviousMessageId");

                    b.HasOne("Database.Entity.PromptEntity", "Prompt")
                        .WithMany()
                        .HasForeignKey("PromptId");

                    b.Navigation("Conversation");

                    b.Navigation("PreviousMessage");

                    b.Navigation("Prompt");
                });

            modelBuilder.Entity("Database.Entity.PromptEntity", b =>
                {
                    b.HasOne("Database.Entity.UsageEntity", "Usage")
                        .WithOne()
                        .HasForeignKey("Database.Entity.PromptEntity", "UsageId");

                    b.HasOne("Database.Entity.UserEntity", "User")
                        .WithMany("Prompts")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Usage");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Database.Entity.UsageEntity", b =>
                {
                    b.HasOne("Database.Entity.PromptEntity", "Prompt")
                        .WithOne()
                        .HasForeignKey("Database.Entity.UsageEntity", "PromptId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Prompt");
                });

            modelBuilder.Entity("Database.Entity.UserEntity", b =>
                {
                    b.HasOne("Database.Entity.ChatConfigurationEntity", "ChatConfiguration")
                        .WithOne("User")
                        .HasForeignKey("Database.Entity.UserEntity", "ChatConfigurationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

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
