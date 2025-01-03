﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PruebaTecnica.Infrastructure;

#nullable disable

namespace PruebaTecnica.Infrastructure.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20241224000000_thirdMigration")]
    partial class thirdMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("PruebaTecnica.Domain.Entities.BankEntity", b =>
                {
                    b.Property<string>("BIC")
                        .HasMaxLength(8)
                        .HasColumnType("nvarchar(8)")
                        .HasAnnotation("Relational:JsonPropertyName", "bic");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasMaxLength(2)
                        .HasColumnType("nvarchar(2)")
                        .HasAnnotation("Relational:JsonPropertyName", "country");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasAnnotation("Relational:JsonPropertyName", "name");

                    b.HasKey("BIC");

                    b.HasIndex("BIC")
                        .IsUnique();

                    b.ToTable("Banks");
                });
#pragma warning restore 612, 618
        }
    }
}
