﻿// <auto-generated />
using System;
using Lab.RateMyBeer.Checkins.Data.Checkins;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Lab.RateMyBeer.Checkins.Data.Checkins.Migrations
{
    [DbContext(typeof(CheckinsContext))]
    [Migration("20210506174240_Checkins")]
    partial class Checkins
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.5")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Lab.RateMyBeer.Checkins.Data.Checkins.CheckinData", b =>
                {
                    b.Property<Guid>("CheckinId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("BeerName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("CheckinId");

                    b.ToTable("Checkins");
                });
#pragma warning restore 612, 618
        }
    }
}
