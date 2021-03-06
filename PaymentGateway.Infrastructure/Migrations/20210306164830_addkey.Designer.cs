﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PaymentGateway.Infrastructure;

namespace PaymentGateway.Infrastructure.Migrations
{
    [DbContext(typeof(PaymentGatewayContext))]
    [Migration("20210306164830_addkey")]
    partial class addkey
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.3")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("PaymentGateway.Domain.Card", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Brand")
                        .HasColumnType("int");

                    b.Property<int>("Cvv")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateTimeCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateTimeUpdated")
                        .HasColumnType("datetime2");

                    b.Property<byte>("ExpiryMonth")
                        .HasColumnType("tinyint");

                    b.Property<int>("ExpiryYear")
                        .HasColumnType("int");

                    b.Property<bool>("Is3DSecure")
                        .HasColumnType("bit");

                    b.Property<string>("LastFourDigits")
                        .HasMaxLength(4)
                        .HasColumnType("nvarchar(4)");

                    b.Property<Guid>("MerchantId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("MerchantId", "Id")
                        .IsUnique();

                    b.ToTable("Cards");
                });

            modelBuilder.Entity("PaymentGateway.Domain.Charge", b =>
                {
                    b.Property<Guid>("MerchantId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(19,4)");

                    b.Property<int>("Currency")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateTimeCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateTimeUpdated")
                        .HasColumnType("datetime2");

                    b.Property<int>("FailureCode")
                        .HasColumnType("int");

                    b.Property<string>("FailureMesage")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<Guid>("IdempotencyKey")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("PaymentResponseId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("MerchantId", "Id");

                    b.ToTable("Charges");
                });

            modelBuilder.Entity("PaymentGateway.Domain.Merchant", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DateTimeCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateTimeUpdated")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<string>("PostalCode")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.HasKey("Id");

                    b.ToTable("Merchant");
                });

            modelBuilder.Entity("PaymentGateway.Domain.Card", b =>
                {
                    b.HasOne("PaymentGateway.Domain.Charge", "Charge")
                        .WithOne("Card")
                        .HasForeignKey("PaymentGateway.Domain.Card", "MerchantId", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Charge");
                });

            modelBuilder.Entity("PaymentGateway.Domain.Charge", b =>
                {
                    b.HasOne("PaymentGateway.Domain.Merchant", "Merchant")
                        .WithMany("Charges")
                        .HasForeignKey("MerchantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Merchant");
                });

            modelBuilder.Entity("PaymentGateway.Domain.Charge", b =>
                {
                    b.Navigation("Card");
                });

            modelBuilder.Entity("PaymentGateway.Domain.Merchant", b =>
                {
                    b.Navigation("Charges");
                });
#pragma warning restore 612, 618
        }
    }
}
