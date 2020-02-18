﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using weddingplanner.Models;

namespace weddingplanner.Migrations
{
    [DbContext(typeof(Context))]
    [Migration("20200217213012_Weddingmig")]
    partial class Weddingmig
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("weddingplanner.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedAt");

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<string>("FirstName")
                        .IsRequired();

                    b.Property<string>("LastName")
                        .IsRequired();

                    b.Property<string>("Password")
                        .IsRequired();

                    b.Property<DateTime>("UpdatedAt");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("weddingplanner.Models.UserWedding", b =>
                {
                    b.Property<int>("UserWeddingId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("UserId");

                    b.Property<int>("WeddingId");

                    b.HasKey("UserWeddingId");

                    b.HasIndex("UserId");

                    b.HasIndex("WeddingId");

                    b.ToTable("UsersWeddings");
                });

            modelBuilder.Entity("weddingplanner.Models.Wedding", b =>
                {
                    b.Property<int>("WeddingId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address")
                        .IsRequired();

                    b.Property<DateTime>("CreatedAt");

                    b.Property<int?>("CreaterUserId");

                    b.Property<DateTime>("UpdatedAt");

                    b.Property<string>("WedderOne")
                        .IsRequired();

                    b.Property<string>("WedderTwo")
                        .IsRequired();

                    b.Property<DateTime>("WeddingDate");

                    b.HasKey("WeddingId");

                    b.HasIndex("CreaterUserId");

                    b.ToTable("Weddings");
                });

            modelBuilder.Entity("weddingplanner.Models.UserWedding", b =>
                {
                    b.HasOne("weddingplanner.Models.User", "User")
                        .WithMany("Weddings")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("weddingplanner.Models.Wedding", "Wedding")
                        .WithMany("RSVP")
                        .HasForeignKey("WeddingId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("weddingplanner.Models.Wedding", b =>
                {
                    b.HasOne("weddingplanner.Models.User", "Creater")
                        .WithMany("WeddingsCreated")
                        .HasForeignKey("CreaterUserId");
                });
#pragma warning restore 612, 618
        }
    }
}
