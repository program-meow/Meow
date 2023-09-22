﻿// <auto-generated />
using System;
using Meow.Sample.Data.MySql;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Meow.Sample.Data.MySql.Migrations
{
    [DbContext(typeof(SampleUnitOfWork))]
    partial class SampleUnitOfWorkModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Meow.Sample.Domain.Models.Sample", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)")
                        .HasColumnName("SampleId")
                        .HasComment("样本标识");

                    b.Property<DateTime?>("CreationTime")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("CreationTime")
                        .HasComment("创建时间");

                    b.Property<Guid?>("CreatorId")
                        .HasColumnType("char(36)")
                        .HasColumnName("CreatorId")
                        .HasComment("创建人标识");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)")
                        .HasColumnName("IsDeleted")
                        .HasComment("是否删除");

                    b.Property<DateTime?>("LastModificationTime")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("LastModificationTime")
                        .HasComment("最后修改时间");

                    b.Property<Guid?>("LastModifierId")
                        .HasColumnType("char(36)")
                        .HasColumnName("LastModifierId")
                        .HasComment("最后修改人标识");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("Name")
                        .HasComment("名称");

                    b.Property<string>("TenantId")
                        .HasColumnType("longtext")
                        .HasColumnName("TenantId")
                        .HasComment("租户标识");

                    b.Property<byte[]>("Version")
                        .IsConcurrencyToken()
                        .HasColumnType("longblob")
                        .HasColumnName("Version")
                        .HasComment("版本号");

                    b.HasKey("Id");

                    b.ToTable("Sample", null, t =>
                        {
                            t.HasComment("样本");
                        });
                });
#pragma warning restore 612, 618
        }
    }
}