﻿// <auto-generated />
using FantasyNameGen.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace FantasyNameGen.Migrations
{
    [DbContext(typeof(NamesContext))]
    [Migration("20180730091408_NameCommon")]
    partial class NameCommon
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.1.1-rtm-30846")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("FantasyNameGen.Models.Name", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Common");

                    b.Property<string>("CyrilName");

                    b.Property<string>("Description");

                    b.Property<char>("Gender");

                    b.Property<string>("RomanName");

                    b.Property<string>("Variants");

                    b.HasKey("Id");

                    b.ToTable("Names");
                });
#pragma warning restore 612, 618
        }
    }
}