﻿// <auto-generated />
using System;
using LGDShop.DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace LGDShop.DataAccess.Migrations
{
    [DbContext(typeof(ShopDbContext))]
    [Migration("20190629145356_shop_init")]
    partial class shop_init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("LGDShop.Domain.Entities.Employee", b =>
                {
                    b.Property<int>("EmployeeId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Age");

                    b.Property<DateTime>("CreatedAt");

                    b.Property<DateTime>("DeletedAt");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("Name");

                    b.Property<int>("PositionId");

                    b.HasKey("EmployeeId");

                    b.HasIndex("PositionId");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("LGDShop.Domain.Entities.Position", b =>
                {
                    b.Property<int>("PositionId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.HasKey("PositionId");

                    b.ToTable("Positions");
                });

            modelBuilder.Entity("LGDShop.Domain.Entities.Employee", b =>
                {
                    b.HasOne("LGDShop.Domain.Entities.Position", "Position")
                        .WithMany("Employees")
                        .HasForeignKey("PositionId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}