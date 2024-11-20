﻿// <auto-generated />
using System;
using System.Collections.Generic;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Database.Migrations
{
    [DbContext(typeof(CineSlateContext))]
    [Migration("20241120072642_UpdateMovies")]
    partial class UpdateMovies
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Domain.Movies.MovieAggregate", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("integer");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("character varying(1000)");

                    b.Property<string>("PosterPath")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.Property<DateOnly>("ReleaseDate")
                        .HasColumnType("date");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.Property<DateTimeOffset>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("UpdatedBy")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.HasKey("Id");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.ToTable("Movies");
                });

            modelBuilder.Entity("Domain.Users.User", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)");

                    b.Property<DateTimeOffset>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("UpdatedBy")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.ComplexProperty<Dictionary<string, object>>("Name", "Domain.Users.User.Name#Name", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("First")
                                .IsRequired()
                                .HasMaxLength(50)
                                .HasColumnType("character varying(50)");

                            b1.Property<string>("Last")
                                .IsRequired()
                                .HasMaxLength(50)
                                .HasColumnType("character varying(50)");
                        });

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Domain.Users.ValueObjects.UserId", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.ToTable("UserId");
                });

            modelBuilder.Entity("Domain.Movies.MovieAggregate", b =>
                {
                    b.OwnsMany("Domain.Movies.ValueObjects.Genre", "Genres", b1 =>
                        {
                            b1.Property<int>("MovieAggregateId")
                                .HasColumnType("integer");

                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("integer");

                            NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b1.Property<int>("Id"));

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasColumnType("text");

                            b1.HasKey("MovieAggregateId", "Id");

                            b1.ToTable("Genre");

                            b1.WithOwner()
                                .HasForeignKey("MovieAggregateId");
                        });

                    b.OwnsMany("Domain.Movies.ValueObjects.Rating", "Ratings", b1 =>
                        {
                            b1.Property<int>("MovieAggregateId")
                                .HasColumnType("integer");

                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("integer");

                            NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b1.Property<int>("Id"));

                            b1.Property<Guid>("RatedById")
                                .HasColumnType("uuid");

                            b1.Property<int>("Value")
                                .HasColumnType("integer");

                            b1.HasKey("MovieAggregateId", "Id");

                            b1.HasIndex("RatedById");

                            b1.ToTable("Rating");

                            b1.WithOwner()
                                .HasForeignKey("MovieAggregateId");

                            b1.HasOne("Domain.Users.ValueObjects.UserId", "RatedBy")
                                .WithMany()
                                .HasForeignKey("RatedById")
                                .OnDelete(DeleteBehavior.Cascade)
                                .IsRequired();

                            b1.Navigation("RatedBy");
                        });

                    b.Navigation("Genres");

                    b.Navigation("Ratings");
                });
#pragma warning restore 612, 618
        }
    }
}
