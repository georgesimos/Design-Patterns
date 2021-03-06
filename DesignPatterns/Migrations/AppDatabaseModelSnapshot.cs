﻿// <auto-generated />
using System;
using DesignPatterns.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DesignPatterns.Migrations
{
    [DbContext(typeof(AppDatabase))]
    partial class AppDatabaseModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.0.1");

            modelBuilder.Entity("DesignPatterns.Models.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Foods")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("TableId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("DesignPatterns.Models.Table", b =>
                {
                    b.Property<Guid>("TableId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("TableId");

                    b.ToTable("Tables");
                });
#pragma warning restore 612, 618
        }
    }
}
