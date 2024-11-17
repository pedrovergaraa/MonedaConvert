﻿// <auto-generated />
using System;
using CurrencyConvert.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace MonedaConvert.Migrations
{
    [DbContext(typeof(CurrencyContext))]
    [Migration("20241117145739_MakeUserIdNullable")]
    partial class MakeUserIdNullable
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.16");

            modelBuilder.Entity("CurrencyConvert.Entities.Currency", b =>
                {
                    b.Property<int>("CurrencyId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<float>("IC")
                        .HasColumnType("REAL");

                    b.Property<string>("Legend")
                        .HasColumnType("TEXT");

                    b.Property<string>("Symbol")
                        .HasColumnType("TEXT");

                    b.Property<int?>("UserId")
                        .IsRequired()
                        .HasColumnType("INTEGER");

                    b.HasKey("CurrencyId");

                    b.HasIndex("UserId");

                    b.ToTable("Currencies");
                });

            modelBuilder.Entity("CurrencyConvert.Entities.FavoriteCurrency", b =>
                {
                    b.Property<int>("FavoriteCurrencyId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<float>("IC")
                        .HasColumnType("REAL");

                    b.Property<string>("Legend")
                        .HasColumnType("TEXT");

                    b.Property<string>("Symbol")
                        .HasColumnType("TEXT");

                    b.Property<int>("UserId")
                        .HasColumnType("INTEGER");

                    b.HasKey("FavoriteCurrencyId");

                    b.HasIndex("UserId");

                    b.ToTable("FavoriteCurrencies");
                });

            modelBuilder.Entity("CurrencyConvert.Entities.Subscription", b =>
                {
                    b.Property<int>("SubId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<long>("Conversions")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Price")
                        .HasColumnType("INTEGER");

                    b.HasKey("SubId");

                    b.ToTable("Subscriptions");

                    b.HasData(
                        new
                        {
                            SubId = 1,
                            Conversions = 10L,
                            Name = "Free",
                            Price = 0
                        },
                        new
                        {
                            SubId = 2,
                            Conversions = 100L,
                            Name = "Trial",
                            Price = 10
                        },
                        new
                        {
                            SubId = 3,
                            Conversions = 9223372036854775807L,
                            Name = "Premium",
                            Price = 15
                        });
                });

            modelBuilder.Entity("CurrencyConvert.Entities.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int?>("SubscriptionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasDefaultValue(1);

                    b.Property<int?>("TotalConversions")
                        .HasColumnType("INTEGER");

                    b.HasKey("UserId");

                    b.HasIndex("SubscriptionId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("CurrencyConvert.Entities.Currency", b =>
                {
                    b.HasOne("CurrencyConvert.Entities.User", "User")
                        .WithMany("Currencies")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("CurrencyConvert.Entities.FavoriteCurrency", b =>
                {
                    b.HasOne("CurrencyConvert.Entities.User", "User")
                        .WithMany("FavoriteCurrencies")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("CurrencyConvert.Entities.User", b =>
                {
                    b.HasOne("CurrencyConvert.Entities.Subscription", "Subscription")
                        .WithMany("Users")
                        .HasForeignKey("SubscriptionId");

                    b.Navigation("Subscription");
                });

            modelBuilder.Entity("CurrencyConvert.Entities.Subscription", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("CurrencyConvert.Entities.User", b =>
                {
                    b.Navigation("Currencies");

                    b.Navigation("FavoriteCurrencies");
                });
#pragma warning restore 612, 618
        }
    }
}
