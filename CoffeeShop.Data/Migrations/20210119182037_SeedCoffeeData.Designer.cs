// <auto-generated />
using System;
using CoffeeShop.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CoffeeShop.Data.Migrations
{
    [DbContext(typeof(ShopDbContext))]
    [Migration("20210119182037_SeedCoffeeData")]
    partial class SeedCoffeeData
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.2");

            modelBuilder.Entity("CoffeeShop.Data.Entities.Coffee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("ImageFileName")
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("Name")
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<string>("Volume")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.HasKey("Id");

                    b.ToTable("Coffees");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            ImageFileName = "espresso.jpg",
                            Name = "Espresso",
                            Price = 8.0,
                            Volume = "133 ml"
                        },
                        new
                        {
                            Id = 2,
                            ImageFileName = "americano.jpg",
                            Name = "Americano",
                            Price = 12.0,
                            Volume = "250 ml"
                        },
                        new
                        {
                            Id = 3,
                            ImageFileName = "americano.jpg",
                            Name = "Americano",
                            Price = 15.0,
                            Volume = "380 ml"
                        },
                        new
                        {
                            Id = 4,
                            ImageFileName = "americano_with_milk.jpg",
                            Name = "Americano with milk",
                            Price = 14.0,
                            Volume = "250 ml"
                        },
                        new
                        {
                            Id = 5,
                            ImageFileName = "americano_with_milk.jpg",
                            Name = "Americano with milk",
                            Price = 17.0,
                            Volume = "380 ml"
                        },
                        new
                        {
                            Id = 6,
                            ImageFileName = "cappuccino.jpg",
                            Name = "Cappuccino",
                            Price = 15.0,
                            Volume = "250 ml"
                        },
                        new
                        {
                            Id = 7,
                            ImageFileName = "cappuccino.jpg",
                            Name = "Cappuccino",
                            Price = 18.0,
                            Volume = "380 ml"
                        },
                        new
                        {
                            Id = 8,
                            ImageFileName = "latte.jpg",
                            Name = "Latte",
                            Price = 15.0,
                            Volume = "250 ml"
                        },
                        new
                        {
                            Id = 9,
                            ImageFileName = "latte.jpg",
                            Name = "Latte",
                            Price = 18.0,
                            Volume = "380 ml"
                        });
                });

            modelBuilder.Entity("CoffeeShop.Data.Entities.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("CustomerName")
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("Id");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("CoffeeShop.Data.Entities.OrderItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("CoffeeId")
                        .HasColumnType("int");

                    b.Property<bool>("CupCap")
                        .HasColumnType("bit");

                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<int>("Sugar")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CoffeeId");

                    b.HasIndex("OrderId");

                    b.ToTable("OrderItem");
                });

            modelBuilder.Entity("CoffeeShop.Data.Entities.OrderItem", b =>
                {
                    b.HasOne("CoffeeShop.Data.Entities.Coffee", "Coffee")
                        .WithMany("OrderItems")
                        .HasForeignKey("CoffeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CoffeeShop.Data.Entities.Order", "Order")
                        .WithMany("OrderItems")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Coffee");

                    b.Navigation("Order");
                });

            modelBuilder.Entity("CoffeeShop.Data.Entities.Coffee", b =>
                {
                    b.Navigation("OrderItems");
                });

            modelBuilder.Entity("CoffeeShop.Data.Entities.Order", b =>
                {
                    b.Navigation("OrderItems");
                });
#pragma warning restore 612, 618
        }
    }
}
