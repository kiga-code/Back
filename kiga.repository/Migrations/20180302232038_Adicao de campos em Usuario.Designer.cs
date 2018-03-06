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
    [Migration("20180302232038_Adicao de campos em Usuario")]
    partial class AdicaodecamposemUsuario
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("kiga.domain.Entities.UsuarioDomain", b =>
                {
                    b.Property<int>("IdUser")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("FacebookId")
                        .IsRequired();

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(120);

                    b.Property<string>("UserToken")
                        .IsRequired();

                    b.Property<DateTime>("created_at")
                        .ValueGeneratedOnAddOrUpdate();

                    b.HasKey("IdUser");

                    b.ToTable("Usuarios");
                });
#pragma warning restore 612, 618
        }
    }
}
