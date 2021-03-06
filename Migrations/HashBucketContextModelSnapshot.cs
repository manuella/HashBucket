﻿// <auto-generated />
using HashBucket.Models.DB;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace HashBucket.Migrations
{
    [DbContext(typeof(HashBucketContext))]
    partial class HashBucketContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.2.3-servicing-35854")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("HashBucket.Models.EncryptedKeyValue", b =>
                {
                    b.Property<string>("HashKey")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("EncryptedValue");

                    b.HasKey("HashKey");

                    b.ToTable("hashValues");
                });
#pragma warning restore 612, 618
        }
    }
}
