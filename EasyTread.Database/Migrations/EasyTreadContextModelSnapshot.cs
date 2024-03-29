﻿// <auto-generated />
using System;
using EasyTread.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EasyTread.Database.Migrations
{
    [DbContext(typeof(EasyTreadContext))]
    partial class EasyTreadContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("EasyTread.Database.Models.Crossing", b =>
                {
                    b.Property<int>("CrossingId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CrossingId"));

                    b.Property<DateTime>("CreatedTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("CrossingDirection")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DealerNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("LicensePlateId")
                        .HasColumnType("int");

                    b.Property<string>("ReportLink")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Valid")
                        .HasColumnType("bit");

                    b.Property<string>("VehicleRating")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CrossingId");

                    b.HasIndex("LicensePlateId");

                    b.ToTable("Crossing");
                });

            modelBuilder.Entity("EasyTread.Database.Models.CustomerDetails", b =>
                {
                    b.Property<int>("CustomerDetailsId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CustomerDetailsId"));

                    b.Property<string>("CustomerName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EmailAdress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LicensePlate")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CustomerDetailsId");

                    b.ToTable("CustomerDetails");
                });

            modelBuilder.Entity("EasyTread.Database.Models.LicensePlate", b =>
                {
                    b.Property<int>("LicensePlateId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("LicensePlateId"));

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CountryConfidence")
                        .HasColumnType("int");

                    b.Property<string>("Plate")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PlateConfidence")
                        .HasColumnType("int");

                    b.HasKey("LicensePlateId");

                    b.ToTable("LicensePlate");
                });

            modelBuilder.Entity("EasyTread.Database.Models.PropertySet", b =>
                {
                    b.Property<int>("PropertySetId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PropertySetId"));

                    b.Property<bool>("DeepGrove")
                        .HasColumnType("bit");

                    b.Property<int>("ShoulderWearId")
                        .HasColumnType("int");

                    b.Property<int>("WearPatternId")
                        .HasColumnType("int");

                    b.HasKey("PropertySetId");

                    b.HasIndex("ShoulderWearId");

                    b.HasIndex("WearPatternId");

                    b.ToTable("PropertySet");
                });

            modelBuilder.Entity("EasyTread.Database.Models.Region", b =>
                {
                    b.Property<int>("RegionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RegionId"));

                    b.Property<int>("RegionPosition")
                        .HasColumnType("int");

                    b.Property<int?>("TireId")
                        .HasColumnType("int");

                    b.Property<decimal>("Value")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("VehicleRating")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("RegionId");

                    b.HasIndex("TireId");

                    b.ToTable("Region");
                });

            modelBuilder.Entity("EasyTread.Database.Models.ShoulderWear", b =>
                {
                    b.Property<int>("ShoulderWearId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ShoulderWearId"));

                    b.Property<string>("Info")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Value")
                        .HasColumnType("int");

                    b.HasKey("ShoulderWearId");

                    b.ToTable("ShoulderWear");
                });

            modelBuilder.Entity("EasyTread.Database.Models.Tire", b =>
                {
                    b.Property<int>("TireId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TireId"));

                    b.Property<int?>("CrossingId")
                        .HasColumnType("int");

                    b.Property<int>("PropertySetId")
                        .HasColumnType("int");

                    b.Property<int>("TirePosition")
                        .HasColumnType("int");

                    b.Property<bool>("Valid")
                        .HasColumnType("bit");

                    b.HasKey("TireId");

                    b.HasIndex("CrossingId");

                    b.HasIndex("PropertySetId");

                    b.ToTable("Tire");
                });

            modelBuilder.Entity("EasyTread.Database.Models.WearPattern", b =>
                {
                    b.Property<int>("WearPatternId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("WearPatternId"));

                    b.Property<string>("Info")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Value")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("WearPatternId");

                    b.ToTable("WearPattern");
                });

            modelBuilder.Entity("EasyTread.Database.Models.Crossing", b =>
                {
                    b.HasOne("EasyTread.Database.Models.LicensePlate", "LicensePlate")
                        .WithMany()
                        .HasForeignKey("LicensePlateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("LicensePlate");
                });

            modelBuilder.Entity("EasyTread.Database.Models.PropertySet", b =>
                {
                    b.HasOne("EasyTread.Database.Models.ShoulderWear", "ShoulderWear")
                        .WithMany()
                        .HasForeignKey("ShoulderWearId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EasyTread.Database.Models.WearPattern", "WearPattern")
                        .WithMany()
                        .HasForeignKey("WearPatternId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ShoulderWear");

                    b.Navigation("WearPattern");
                });

            modelBuilder.Entity("EasyTread.Database.Models.Region", b =>
                {
                    b.HasOne("EasyTread.Database.Models.Tire", null)
                        .WithMany("Regions")
                        .HasForeignKey("TireId");
                });

            modelBuilder.Entity("EasyTread.Database.Models.Tire", b =>
                {
                    b.HasOne("EasyTread.Database.Models.Crossing", null)
                        .WithMany("Tires")
                        .HasForeignKey("CrossingId");

                    b.HasOne("EasyTread.Database.Models.PropertySet", "PropertySet")
                        .WithMany()
                        .HasForeignKey("PropertySetId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PropertySet");
                });

            modelBuilder.Entity("EasyTread.Database.Models.Crossing", b =>
                {
                    b.Navigation("Tires");
                });

            modelBuilder.Entity("EasyTread.Database.Models.Tire", b =>
                {
                    b.Navigation("Regions");
                });
#pragma warning restore 612, 618
        }
    }
}
