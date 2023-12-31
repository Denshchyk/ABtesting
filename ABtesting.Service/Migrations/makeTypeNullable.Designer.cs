﻿// <auto-generated />
using System;
using ABtesting.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ABtesting.Service.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    [Migration("20231007112119_makeTypeNullable")]
    partial class makeTypeNullable
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("ABtesting.Service.Device", b =>
                {
                    b.Property<Guid>("DeviceToken")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Type")
                        .HasColumnType("text");

                    b.HasKey("DeviceToken");

                    b.ToTable("Devices");
                });

            modelBuilder.Entity("ABtesting.Service.DevicesExperiment", b =>
                {
                    b.Property<Guid>("ExperimentId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("DeviceToken")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("DeviceToken1")
                        .HasColumnType("uuid");

                    b.HasKey("ExperimentId", "DeviceToken");

                    b.HasIndex("DeviceToken1");

                    b.ToTable("DevicesExperiments");
                });

            modelBuilder.Entity("ABtesting.Service.Experiment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("ChanceInPercents")
                        .HasColumnType("integer");

                    b.Property<string>("Key")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Experiments");
                });

            modelBuilder.Entity("ABtesting.Service.DevicesExperiment", b =>
                {
                    b.HasOne("ABtesting.Service.Device", null)
                        .WithMany("DevicesExperiments")
                        .HasForeignKey("DeviceToken1");

                    b.HasOne("ABtesting.Service.Experiment", null)
                        .WithMany("DevicesExperiments")
                        .HasForeignKey("ExperimentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ABtesting.Service.Device", b =>
                {
                    b.Navigation("DevicesExperiments");
                });

            modelBuilder.Entity("ABtesting.Service.Experiment", b =>
                {
                    b.Navigation("DevicesExperiments");
                });
#pragma warning restore 612, 618
        }
    }
}
