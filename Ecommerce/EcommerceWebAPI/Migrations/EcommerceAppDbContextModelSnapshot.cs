﻿// <auto-generated />
using System;
using Ecommerce.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EcommerceWebAPI.Migrations
{
    [DbContext(typeof(EcommerceAppDbContext))]
    partial class EcommerceAppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.9");

            modelBuilder.Entity("Ecommerce.Persistence.State.AggregateState", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("TEXT")
                        .HasColumnName("id");

                    b.Property<Guid>("AggregateId")
                        .HasColumnType("TEXT")
                        .HasColumnName("aggregate_id");

                    b.Property<string>("AggregationName")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("aggregation_name");

                    b.Property<string>("EventData")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("event_data");

                    b.Property<string>("EventType")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("event_type");

                    b.HasKey("Id");

                    b.ToTable("aggregate_state", (string)null);
                });

            modelBuilder.Entity("Ecommerce.Persistence.State.ProductState", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("TEXT")
                        .HasColumnName("id");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("description");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("INTEGER")
                        .HasColumnName("is_deleted");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("name");

                    b.Property<byte[]>("RowVersion")
                        .IsRequired()
                        .HasColumnType("BLOB")
                        .HasColumnName("row_version");

                    b.Property<double>("Weight")
                        .HasColumnType("REAL")
                        .HasColumnName("weight");

                    b.HasKey("Id");

                    b.ToTable("products", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}
