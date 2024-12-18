﻿// <auto-generated />
using System;
using CurlingRinkManagement.Planner.Business.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CurlingRinkManagement.Planner.Business.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20241218194827_ActivityOwnsSheetLinks")]
    partial class ActivityOwnsSheetLinks
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("CurlingRinkManagement.Planner.Domain.DatabaseModels.Activity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("ActivityTypeId")
                        .HasColumnType("uuid");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("ActivityTypeId");

                    b.ToTable("Activities");
                });

            modelBuilder.Entity("CurlingRinkManagement.Planner.Domain.DatabaseModels.ActivityType", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Color")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("RecommendedMinutesBlockedAfter")
                        .HasColumnType("integer");

                    b.Property<int>("RecommendedMinutesBlockedBefore")
                        .HasColumnType("integer");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("ActivityTypes");
                });

            modelBuilder.Entity("CurlingRinkManagement.Planner.Domain.DatabaseModels.Sheet", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Order")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Sheets");
                });

            modelBuilder.Entity("CurlingRinkManagement.Planner.Domain.DatabaseModels.Activity", b =>
                {
                    b.HasOne("CurlingRinkManagement.Planner.Domain.DatabaseModels.ActivityType", "ActivityType")
                        .WithMany()
                        .HasForeignKey("ActivityTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsMany("CurlingRinkManagement.Planner.Domain.DatabaseModels.DateTimeRange", "PlannedDates", b1 =>
                        {
                            b1.Property<Guid>("ActivityId")
                                .HasColumnType("uuid");

                            b1.Property<Guid>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("uuid");

                            b1.Property<DateTime>("End")
                                .HasColumnType("timestamp with time zone");

                            b1.Property<int>("MinutesBlockedAfter")
                                .HasColumnType("integer");

                            b1.Property<int>("MinutesBlockedBefore")
                                .HasColumnType("integer");

                            b1.Property<DateTime>("Start")
                                .HasColumnType("timestamp with time zone");

                            b1.HasKey("ActivityId", "Id");

                            b1.ToTable("DateTimeRanges");

                            b1.WithOwner("Activity")
                                .HasForeignKey("ActivityId");

                            b1.Navigation("Activity");
                        });

                    b.OwnsMany("CurlingRinkManagement.Planner.Domain.DatabaseModels.SheetActivity", "Sheets", b1 =>
                        {
                            b1.Property<Guid>("ActivityId")
                                .HasColumnType("uuid");

                            b1.Property<Guid>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("uuid");

                            b1.Property<Guid>("SheetId")
                                .HasColumnType("uuid");

                            b1.HasKey("ActivityId", "Id");

                            b1.HasIndex("SheetId");

                            b1.ToTable("SheetActivity");

                            b1.WithOwner("Activity")
                                .HasForeignKey("ActivityId");

                            b1.HasOne("CurlingRinkManagement.Planner.Domain.DatabaseModels.Sheet", "Sheet")
                                .WithMany()
                                .HasForeignKey("SheetId")
                                .OnDelete(DeleteBehavior.Cascade)
                                .IsRequired();

                            b1.Navigation("Activity");

                            b1.Navigation("Sheet");
                        });

                    b.Navigation("ActivityType");

                    b.Navigation("PlannedDates");

                    b.Navigation("Sheets");
                });
#pragma warning restore 612, 618
        }
    }
}
