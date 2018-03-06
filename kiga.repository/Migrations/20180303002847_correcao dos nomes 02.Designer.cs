﻿// <auto-generated />
using kiga.repository.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace kiga.repository.Migrations
{
    [DbContext(typeof(kigaContexto))]
    [Migration("20180303002847_correcao dos nomes 02")]
    partial class correcaodosnomes02
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("kiga.domain.Entities.UsuarioDomain", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("created_at")
                        .ValueGeneratedOnAddOrUpdate();

                    b.Property<string>("facebookId")
                        .IsRequired();

                    b.Property<string>("firstName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("lastName")
                        .IsRequired()
                        .HasMaxLength(120);

                    b.Property<string>("token");

                    b.HasKey("id");

                    b.ToTable("Usuarios");
                });
#pragma warning restore 612, 618
        }
    }
}
