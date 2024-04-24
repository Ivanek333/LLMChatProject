﻿// <auto-generated />
using System;
using ChatWebAPI.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ChatWebAPI.Migrations
{
    [DbContext(typeof(ChatDbContext))]
    [Migration("20240423172237_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Domain.Core.Entities.Chat", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("GenerationState")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Chats");
                });

            modelBuilder.Entity("Domain.Core.Entities.Message", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int?>("ChatId")
                        .HasColumnType("int");

                    b.Property<int>("Sender")
                        .HasColumnType("int");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("ChatId");

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("Domain.Core.Entities.Chat", b =>
                {
                    b.OwnsOne("Domain.Core.Entities.LLMGenerationErrorData", "GenerationError", b1 =>
                        {
                            b1.Property<int>("ChatId")
                                .HasColumnType("int");

                            b1.Property<string>("Message")
                                .IsRequired()
                                .HasColumnType("longtext");

                            b1.Property<string>("Title")
                                .IsRequired()
                                .HasColumnType("longtext");

                            b1.HasKey("ChatId");

                            b1.ToTable("Chats");

                            b1.WithOwner()
                                .HasForeignKey("ChatId");
                        });

                    b.OwnsOne("Domain.Core.Entities.LLMParameters", "ChatLLMParameters", b1 =>
                        {
                            b1.Property<int>("ChatId")
                                .HasColumnType("int");

                            b1.Property<int>("MaxTokens")
                                .HasColumnType("int");

                            b1.Property<string>("Model")
                                .IsRequired()
                                .HasColumnType("longtext");

                            b1.Property<double>("Temperature")
                                .HasColumnType("double");

                            b1.HasKey("ChatId");

                            b1.ToTable("Chats");

                            b1.WithOwner()
                                .HasForeignKey("ChatId");
                        });

                    b.Navigation("ChatLLMParameters")
                        .IsRequired();

                    b.Navigation("GenerationError")
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Core.Entities.Message", b =>
                {
                    b.HasOne("Domain.Core.Entities.Chat", null)
                        .WithMany("Messages")
                        .HasForeignKey("ChatId");
                });

            modelBuilder.Entity("Domain.Core.Entities.Chat", b =>
                {
                    b.Navigation("Messages");
                });
#pragma warning restore 612, 618
        }
    }
}