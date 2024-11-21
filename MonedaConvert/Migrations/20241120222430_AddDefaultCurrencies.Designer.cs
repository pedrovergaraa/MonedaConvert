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
    [Migration("20241120222430_AddDefaultCurrencies")]
    partial class AddDefaultCurrencies
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

                    b.Property<bool>("IsDefault")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Legend")
                        .HasColumnType("TEXT");

                    b.Property<string>("Symbol")
                        .HasColumnType("TEXT");

                    b.Property<int?>("UserId")
                        .HasColumnType("INTEGER");

                    b.HasKey("CurrencyId");

                    b.HasIndex("UserId");

                    b.ToTable("Currencies");

                    b.HasData(
                        new
                        {
                            CurrencyId = 1,
                            IC = 0.005f,
                            IsDefault = false,
                            Legend = "ARS",
                            Symbol = "$"
                        },
                        new
                        {
                            CurrencyId = 2,
                            IC = 1f,
                            IsDefault = false,
                            Legend = "USD",
                            Symbol = "$"
                        },
                        new
                        {
                            CurrencyId = 3,
                            IC = 1.1f,
                            IsDefault = false,
                            Legend = "EUR",
                            Symbol = "€"
                        },
                        new
                        {
                            CurrencyId = 4,
                            IC = 1.3f,
                            IsDefault = false,
                            Legend = "GBP",
                            Symbol = "£"
                        },
                        new
                        {
                            CurrencyId = 5,
                            IC = 0.007f,
                            IsDefault = false,
                            Legend = "JPY",
                            Symbol = "¥"
                        },
                        new
                        {
                            CurrencyId = 6,
                            IC = 0.75f,
                            IsDefault = false,
                            Legend = "CAD",
                            Symbol = "$"
                        },
                        new
                        {
                            CurrencyId = 7,
                            IC = 0.72f,
                            IsDefault = false,
                            Legend = "AUD",
                            Symbol = "$"
                        },
                        new
                        {
                            CurrencyId = 8,
                            IC = 1.05f,
                            IsDefault = false,
                            Legend = "CHF",
                            Symbol = "$"
                        });
                });

            modelBuilder.Entity("CurrencyConvert.Entities.Subscription", b =>
                {
                    b.Property<int>("SubId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("AllowedAttempts")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<decimal>("Price")
                        .HasColumnType("TEXT");

                    b.HasKey("SubId");

                    b.ToTable("Subscriptions");

                    b.HasData(
                        new
                        {
                            SubId = 1,
                            AllowedAttempts = 10,
                            Name = "Free",
                            Price = 0m
                        },
                        new
                        {
                            SubId = 2,
                            AllowedAttempts = 100,
                            Name = "Trial",
                            Price = 10m
                        },
                        new
                        {
                            SubId = 3,
                            AllowedAttempts = 2147483647,
                            Name = "Premium",
                            Price = 15m
                        });
                });

            modelBuilder.Entity("CurrencyConvert.Entities.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("Attempts")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int?>("SubscriptionId")
                        .HasColumnType("INTEGER");

                    b.HasKey("UserId");

                    b.HasIndex("SubscriptionId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("FavoriteCurrency", b =>
                {
                    b.Property<int>("FavoriteCurrencyId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("CurrencyId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("UserId")
                        .HasColumnType("INTEGER");

                    b.HasKey("FavoriteCurrencyId");

                    b.HasIndex("CurrencyId");

                    b.HasIndex("UserId");

                    b.ToTable("FavoriteCurrencies");
                });

            modelBuilder.Entity("CurrencyConvert.Entities.Currency", b =>
                {
                    b.HasOne("CurrencyConvert.Entities.User", "User")
                        .WithMany("Currencies")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("User");
                });

            modelBuilder.Entity("CurrencyConvert.Entities.User", b =>
                {
                    b.HasOne("CurrencyConvert.Entities.Subscription", "Subscription")
                        .WithMany("Users")
                        .HasForeignKey("SubscriptionId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("Subscription");
                });

            modelBuilder.Entity("FavoriteCurrency", b =>
                {
                    b.HasOne("CurrencyConvert.Entities.Currency", "Currency")
                        .WithMany()
                        .HasForeignKey("CurrencyId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("CurrencyConvert.Entities.User", "User")
                        .WithMany("FavoriteCurrencies")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("Currency");

                    b.Navigation("User");
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
