﻿// <auto-generated />
using System;
using System.Collections.Generic;
using DataAccessLayer.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DataAccessLayer.Migrations
{
    [DbContext(typeof(EntityDbContext))]
    partial class EntityDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("DataAccessLayer.Models.AggregatorModel", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("Login");

                    b.ToTable("Aggregators");
                });

            modelBuilder.Entity("DataAccessLayer.Models.ConsumerModel", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("Id");

                    b.ToTable("Consumers");
                });

            modelBuilder.Entity("DataAccessLayer.Models.InviteModel", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<long>("AggregatorId")
                        .HasColumnType("bigint");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsActivated")
                        .HasColumnType("boolean");

                    b.HasKey("Id");

                    b.HasIndex("AggregatorId");

                    b.HasIndex("Code");

                    b.ToTable("Invites");
                });

            modelBuilder.Entity("DataAccessLayer.Models.OrderModel", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<long>("AggregatorId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("EndTimestamp")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("StartTimestamp")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("State")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("AggregatorId");

                    b.HasIndex("Id");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("DataAccessLayer.Models.ResponseModel", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<long>("ConsumerId")
                        .HasColumnType("bigint");

                    b.Property<long>("OrderId")
                        .HasColumnType("bigint");

                    b.Property<List<long>>("ReduceData")
                        .IsRequired()
                        .HasColumnType("bigint[]");

                    b.HasKey("Id");

                    b.HasIndex("ConsumerId");

                    b.HasIndex("Id");

                    b.HasIndex("OrderId");

                    b.ToTable("Responses");
                });

            modelBuilder.Entity("DataAccessLayer.Models.SheddingModel", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<long>("ConsumerId")
                        .HasColumnType("bigint");

                    b.Property<long>("Duration")
                        .HasColumnType("bigint");

                    b.Property<long>("OrderId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("StartTimestamp")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.Property<long>("Volume")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("ConsumerId");

                    b.HasIndex("Id");

                    b.HasIndex("OrderId");

                    b.ToTable("Sheddings");
                });

            modelBuilder.Entity("DataAccessLayer.Models.InviteModel", b =>
                {
                    b.HasOne("DataAccessLayer.Models.AggregatorModel", "Aggregator")
                        .WithMany()
                        .HasForeignKey("AggregatorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Aggregator");
                });

            modelBuilder.Entity("DataAccessLayer.Models.OrderModel", b =>
                {
                    b.HasOne("DataAccessLayer.Models.AggregatorModel", "Aggregator")
                        .WithMany()
                        .HasForeignKey("AggregatorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Aggregator");
                });

            modelBuilder.Entity("DataAccessLayer.Models.ResponseModel", b =>
                {
                    b.HasOne("DataAccessLayer.Models.ConsumerModel", "Consumer")
                        .WithMany()
                        .HasForeignKey("ConsumerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DataAccessLayer.Models.OrderModel", "Order")
                        .WithMany()
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Consumer");

                    b.Navigation("Order");
                });

            modelBuilder.Entity("DataAccessLayer.Models.SheddingModel", b =>
                {
                    b.HasOne("DataAccessLayer.Models.ConsumerModel", "Consumer")
                        .WithMany()
                        .HasForeignKey("ConsumerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DataAccessLayer.Models.OrderModel", "Order")
                        .WithMany()
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Consumer");

                    b.Navigation("Order");
                });
#pragma warning restore 612, 618
        }
    }
}
