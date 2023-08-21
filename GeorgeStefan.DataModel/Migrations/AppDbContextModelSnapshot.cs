﻿// <auto-generated />
using System;
using GeorgeStefan.DataModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace GeorgeStefan.DataModel.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.17")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("GeorgeStefan.DataModel.Country", b =>
                {
                    b.Property<string>("CountryId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CountryName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DepotId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("CountryId");

                    b.HasIndex("DepotId");

                    b.ToTable("Countries");
                });

            modelBuilder.Entity("GeorgeStefan.DataModel.Depot", b =>
                {
                    b.Property<string>("DepotId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("DepotName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("DepotId");

                    b.ToTable("Depots");
                });

            modelBuilder.Entity("GeorgeStefan.DataModel.DrugType", b =>
                {
                    b.Property<string>("DrugTypeId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("DrugTypeName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("WeightInPounds")
                        .HasColumnType("real");

                    b.HasKey("DrugTypeId");

                    b.ToTable("DrugTypes");
                });

            modelBuilder.Entity("GeorgeStefan.DataModel.DrugUnit", b =>
                {
                    b.Property<int?>("DrugUnitId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("DepotId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("DrugTypeId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int?>("PickNumber")
                        .HasColumnType("int");

                    b.HasKey("DrugUnitId");

                    b.HasIndex("DepotId");

                    b.HasIndex("DrugTypeId");

                    b.ToTable("DrugUnits");
                });

            modelBuilder.Entity("GeorgeStefan.DataModel.Site", b =>
                {
                    b.Property<string>("SiteId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CountryCode")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("SiteName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("SiteId");

                    b.HasIndex("CountryCode");

                    b.ToTable("Sites");
                });

            modelBuilder.Entity("GeorgeStefan.DataModel.Country", b =>
                {
                    b.HasOne("GeorgeStefan.DataModel.Depot", "Depot")
                        .WithMany("Countries")
                        .HasForeignKey("DepotId");

                    b.Navigation("Depot");
                });

            modelBuilder.Entity("GeorgeStefan.DataModel.DrugUnit", b =>
                {
                    b.HasOne("GeorgeStefan.DataModel.Depot", "Depot")
                        .WithMany("DrugUnits")
                        .HasForeignKey("DepotId");

                    b.HasOne("GeorgeStefan.DataModel.DrugType", "DrugType")
                        .WithMany("DrugUnits")
                        .HasForeignKey("DrugTypeId");

                    b.Navigation("Depot");

                    b.Navigation("DrugType");
                });

            modelBuilder.Entity("GeorgeStefan.DataModel.Site", b =>
                {
                    b.HasOne("GeorgeStefan.DataModel.Country", "Country")
                        .WithMany()
                        .HasForeignKey("CountryCode");

                    b.Navigation("Country");
                });

            modelBuilder.Entity("GeorgeStefan.DataModel.Depot", b =>
                {
                    b.Navigation("Countries");

                    b.Navigation("DrugUnits");
                });

            modelBuilder.Entity("GeorgeStefan.DataModel.DrugType", b =>
                {
                    b.Navigation("DrugUnits");
                });
#pragma warning restore 612, 618
        }
    }
}