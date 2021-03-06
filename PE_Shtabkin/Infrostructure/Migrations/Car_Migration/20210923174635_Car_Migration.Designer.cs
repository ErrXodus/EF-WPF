// <auto-generated />
using System;
using Infrostructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infrostructure.Migrations.Car_Migration
{
    [DbContext(typeof(Car_Context))]
    [Migration("20210923174635_Car_Migration")]
    partial class Car_Migration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 64)
                .HasAnnotation("ProductVersion", "5.0.10");

            modelBuilder.Entity("Infrostructure.Car", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Country")
                        .HasColumnType("longtext");

                    b.Property<bool>("Is_Busy")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Model")
                        .HasColumnType("longtext");

                    b.Property<string>("Place")
                        .HasColumnType("longtext");

                    b.Property<decimal>("Price_PerDay")
                        .HasColumnType("decimal(65,30)");

                    b.Property<DateTime>("Rent_Ends")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("Rent_Starts")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Town")
                        .HasColumnType("longtext");

                    b.HasKey("ID");

                    b.ToTable("Cars");
                });
#pragma warning restore 612, 618
        }
    }
}
