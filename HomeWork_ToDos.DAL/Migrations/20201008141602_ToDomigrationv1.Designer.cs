﻿// <auto-generated />
using System;
using HomeWork_ToDos.DAL.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace HomeWork_ToDos.DAL.Migrations
{
    [DbContext(typeof(ToDoDbContext))]
    [Migration("20201008141602_ToDomigrationv1")]
    partial class ToDomigrationv1
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("HomeWork_ToDos.CommonLib.Models.DbModels.LabelDbModel", b =>
                {
                    b.Property<long>("LabelId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long>("CreatedBy")
                        .HasColumnType("bigint");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("LabelId");

                    b.HasIndex("CreatedBy");

                    b.ToTable("Labels");

                    b.HasData(
                        new
                        {
                            LabelId = 1L,
                            CreatedBy = 1L,
                            Description = "Review"
                        },
                        new
                        {
                            LabelId = 2L,
                            CreatedBy = 1L,
                            Description = "Buy"
                        },
                        new
                        {
                            LabelId = 3L,
                            CreatedBy = 1L,
                            Description = "Explore"
                        },
                        new
                        {
                            LabelId = 4L,
                            CreatedBy = 1L,
                            Description = "Discover"
                        });
                });

            modelBuilder.Entity("HomeWork_ToDos.CommonLib.Models.DbModels.MapLabelsToItemDbModel", b =>
                {
                    b.Property<long>("ItemMappingId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long>("CreatedBy")
                        .HasColumnType("bigint");

                    b.Property<long>("LabelId")
                        .HasColumnType("bigint");

                    b.Property<long>("ToDoItemId")
                        .HasColumnType("bigint");

                    b.HasKey("ItemMappingId");

                    b.HasIndex("CreatedBy");

                    b.HasIndex("LabelId");

                    b.HasIndex("ToDoItemId");

                    b.ToTable("MapLabelsToItems");
                });

            modelBuilder.Entity("HomeWork_ToDos.CommonLib.Models.DbModels.MapLabelsToListDbModel", b =>
                {
                    b.Property<long>("ListMappingId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long>("CreatedBy")
                        .HasColumnType("bigint");

                    b.Property<long>("LabelId")
                        .HasColumnType("bigint");

                    b.Property<long>("ToDoListId")
                        .HasColumnType("bigint");

                    b.HasKey("ListMappingId");

                    b.HasIndex("CreatedBy");

                    b.HasIndex("LabelId");

                    b.HasIndex("ToDoListId");

                    b.ToTable("MapLabelsToLists");
                });

            modelBuilder.Entity("HomeWork_ToDos.CommonLib.Models.DbModels.ToDoItemDbModel", b =>
                {
                    b.Property<long>("ToDoItemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long>("CreatedBy")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Notes")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("ToDoListId")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("UpdationDate")
                        .HasColumnType("datetime2");

                    b.HasKey("ToDoItemId");

                    b.HasIndex("CreatedBy");

                    b.HasIndex("ToDoListId");

                    b.ToTable("ToDoItems");

                    b.HasData(
                        new
                        {
                            ToDoItemId = 1L,
                            CreatedBy = 1L,
                            CreationDate = new DateTime(2020, 10, 8, 19, 46, 1, 562, DateTimeKind.Local).AddTicks(6686),
                            Notes = "Buy IPhone 11",
                            ToDoListId = 1L
                        },
                        new
                        {
                            ToDoItemId = 2L,
                            CreatedBy = 1L,
                            CreationDate = new DateTime(2020, 10, 8, 19, 46, 1, 562, DateTimeKind.Local).AddTicks(7112),
                            Notes = "Buy Pixel 4a",
                            ToDoListId = 1L
                        },
                        new
                        {
                            ToDoItemId = 3L,
                            CreatedBy = 1L,
                            CreationDate = new DateTime(2020, 10, 8, 19, 46, 1, 562, DateTimeKind.Local).AddTicks(7127),
                            Notes = "Review Pixel 4a",
                            ToDoListId = 2L
                        },
                        new
                        {
                            ToDoItemId = 4L,
                            CreatedBy = 1L,
                            CreationDate = new DateTime(2020, 10, 8, 19, 46, 1, 562, DateTimeKind.Local).AddTicks(7129),
                            Notes = "Review IPhone 11",
                            ToDoListId = 2L
                        },
                        new
                        {
                            ToDoItemId = 5L,
                            CreatedBy = 1L,
                            CreationDate = new DateTime(2020, 10, 8, 19, 46, 1, 562, DateTimeKind.Local).AddTicks(7132),
                            Notes = "Explore animal kingdom",
                            ToDoListId = 3L
                        });
                });

            modelBuilder.Entity("HomeWork_ToDos.CommonLib.Models.DbModels.ToDoListDbModel", b =>
                {
                    b.Property<long>("ToDoListId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long>("CreatedBy")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdationDate")
                        .HasColumnType("datetime2");

                    b.HasKey("ToDoListId");

                    b.HasIndex("CreatedBy");

                    b.ToTable("ToDoLists");

                    b.HasData(
                        new
                        {
                            ToDoListId = 1L,
                            CreatedBy = 1L,
                            CreationDate = new DateTime(2020, 10, 8, 19, 46, 1, 528, DateTimeKind.Local).AddTicks(3791),
                            Description = "List of phones to buy"
                        },
                        new
                        {
                            ToDoListId = 2L,
                            CreatedBy = 1L,
                            CreationDate = new DateTime(2020, 10, 8, 19, 46, 1, 552, DateTimeKind.Local).AddTicks(5414),
                            Description = "List of phones to review"
                        },
                        new
                        {
                            ToDoListId = 3L,
                            CreatedBy = 1L,
                            CreationDate = new DateTime(2020, 10, 8, 19, 46, 1, 552, DateTimeKind.Local).AddTicks(5454),
                            Description = "List of things to explore"
                        },
                        new
                        {
                            ToDoListId = 4L,
                            CreatedBy = 1L,
                            CreationDate = new DateTime(2020, 10, 8, 19, 46, 1, 552, DateTimeKind.Local).AddTicks(5457),
                            Description = "List of places to travel"
                        },
                        new
                        {
                            ToDoListId = 5L,
                            CreatedBy = 1L,
                            CreationDate = new DateTime(2020, 10, 8, 19, 46, 1, 552, DateTimeKind.Local).AddTicks(5459),
                            Description = "List of games"
                        });
                });

            modelBuilder.Entity("HomeWork_ToDos.CommonLib.Models.DbModels.UserDbModel", b =>
                {
                    b.Property<long>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserRole")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            UserId = 1L,
                            FirstName = "Onkar",
                            LastName = "Singh",
                            Password = "MTIz",
                            UserName = "Onkar",
                            UserRole = "Admin"
                        });
                });

            modelBuilder.Entity("HomeWork_ToDos.CommonLib.Models.DbModels.LabelDbModel", b =>
                {
                    b.HasOne("HomeWork_ToDos.CommonLib.Models.DbModels.UserDbModel", "Users")
                        .WithMany()
                        .HasForeignKey("CreatedBy")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("HomeWork_ToDos.CommonLib.Models.DbModels.MapLabelsToItemDbModel", b =>
                {
                    b.HasOne("HomeWork_ToDos.CommonLib.Models.DbModels.UserDbModel", "Users")
                        .WithMany()
                        .HasForeignKey("CreatedBy")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("HomeWork_ToDos.CommonLib.Models.DbModels.LabelDbModel", "Labels")
                        .WithMany()
                        .HasForeignKey("LabelId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("HomeWork_ToDos.CommonLib.Models.DbModels.ToDoItemDbModel", "ToDoItems")
                        .WithMany("MapLabelsToItems")
                        .HasForeignKey("ToDoItemId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("HomeWork_ToDos.CommonLib.Models.DbModels.MapLabelsToListDbModel", b =>
                {
                    b.HasOne("HomeWork_ToDos.CommonLib.Models.DbModels.UserDbModel", "Users")
                        .WithMany()
                        .HasForeignKey("CreatedBy")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("HomeWork_ToDos.CommonLib.Models.DbModels.LabelDbModel", "Labels")
                        .WithMany()
                        .HasForeignKey("LabelId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("HomeWork_ToDos.CommonLib.Models.DbModels.ToDoListDbModel", "ToDoLists")
                        .WithMany("MapLabelsToLists")
                        .HasForeignKey("ToDoListId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("HomeWork_ToDos.CommonLib.Models.DbModels.ToDoItemDbModel", b =>
                {
                    b.HasOne("HomeWork_ToDos.CommonLib.Models.DbModels.UserDbModel", "Users")
                        .WithMany()
                        .HasForeignKey("CreatedBy")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HomeWork_ToDos.CommonLib.Models.DbModels.ToDoListDbModel", "ToDoLists")
                        .WithMany("ToDoItems")
                        .HasForeignKey("ToDoListId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("HomeWork_ToDos.CommonLib.Models.DbModels.ToDoListDbModel", b =>
                {
                    b.HasOne("HomeWork_ToDos.CommonLib.Models.DbModels.UserDbModel", "Users")
                        .WithMany()
                        .HasForeignKey("CreatedBy")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
