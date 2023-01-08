﻿// <auto-generated />
using System;
using HorasRaras.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace HorasRaras.Data.Migrations
{
    [DbContext(typeof(HorasRarasContext))]
    [Migration("20221119141734_alteracaoDadosUsuarioMigration")]
    partial class alteracaoDadosUsuarioMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("HorasRaras.Domain.Entities.ClienteEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("Ativo")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("DataAlteracao")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DataInclusao")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("UsuarioAlteracao")
                        .HasColumnType("int");

                    b.Property<int?>("UsuarioInclusao")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("Clientes");
                });

            modelBuilder.Entity("HorasRaras.Domain.Entities.PerfilEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("Ativo")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("DataAlteracao")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DataInclusao")
                        .HasColumnType("datetime2");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("UsuarioAlteracao")
                        .HasColumnType("int");

                    b.Property<int?>("UsuarioInclusao")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Perfis");
                });

            modelBuilder.Entity("HorasRaras.Domain.Entities.ProjetoEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("Ativo")
                        .HasColumnType("bit");

                    b.Property<int>("ClienteId")
                        .HasColumnType("int");

                    b.Property<float?>("CustoPorHora")
                        .HasColumnType("real");

                    b.Property<DateTime?>("DataAlteracao")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DataFinal")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DataInclusao")
                        .HasColumnType("datetime2");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("UsuarioAlteracao")
                        .HasColumnType("int");

                    b.Property<int?>("UsuarioInclusao")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ClienteId");

                    b.ToTable("Projetos");
                });

            modelBuilder.Entity("HorasRaras.Domain.Entities.TarefaEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("Ativo")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("DataAlteracao")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DataInclusao")
                        .HasColumnType("datetime2");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("HoraFinal")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("HoraInicio")
                        .HasColumnType("datetime2");

                    b.Property<int>("ProjetoId")
                        .HasColumnType("int");

                    b.Property<TimeSpan?>("TotalHoras")
                        .HasColumnType("time");

                    b.Property<int?>("UsuarioAlteracao")
                        .HasColumnType("int");

                    b.Property<int>("UsuarioId")
                        .HasColumnType("int");

                    b.Property<int?>("UsuarioInclusao")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProjetoId");

                    b.HasIndex("UsuarioId");

                    b.ToTable("Tarefas");
                });

            modelBuilder.Entity("HorasRaras.Domain.Entities.UsuarioEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("Ativo")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("DataAlteracao")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DataInclusao")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("EmailConfirmado")
                        .HasColumnType("bit");

                    b.Property<Guid>("HashEmailConfimacao")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("HashSenha")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("HashSenhaExpiracao")
                        .HasColumnType("datetime2");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PerfilId")
                        .HasColumnType("int");

                    b.Property<string>("Senha")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("UsuarioAlteracao")
                        .HasColumnType("int");

                    b.Property<int?>("UsuarioInclusao")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("PerfilId");

                    b.ToTable("Usuarios");
                });

            modelBuilder.Entity("HorasRaras.Domain.Entities.UsuarioProjetoEntity", b =>
                {
                    b.Property<int>("UsuarioId")
                        .HasColumnType("int");

                    b.Property<int>("ProjetoId")
                        .HasColumnType("int");

                    b.Property<bool>("Ativo")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("DataAlteracao")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DataInclusao")
                        .HasColumnType("datetime2");

                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<int?>("UsuarioAlteracao")
                        .HasColumnType("int");

                    b.Property<int?>("UsuarioInclusao")
                        .HasColumnType("int");

                    b.HasKey("UsuarioId", "ProjetoId");

                    b.HasIndex("ProjetoId");

                    b.ToTable("UsuarioProjetos");
                });

            modelBuilder.Entity("HorasRaras.Domain.Entities.ProjetoEntity", b =>
                {
                    b.HasOne("HorasRaras.Domain.Entities.ClienteEntity", "Cliente")
                        .WithMany("Projetos")
                        .HasForeignKey("ClienteId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Cliente");
                });

            modelBuilder.Entity("HorasRaras.Domain.Entities.TarefaEntity", b =>
                {
                    b.HasOne("HorasRaras.Domain.Entities.ProjetoEntity", "Projeto")
                        .WithMany("Tarefas")
                        .HasForeignKey("ProjetoId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("HorasRaras.Domain.Entities.UsuarioEntity", "Usuario")
                        .WithMany("Tarefas")
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Projeto");

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("HorasRaras.Domain.Entities.UsuarioEntity", b =>
                {
                    b.HasOne("HorasRaras.Domain.Entities.PerfilEntity", "Perfil")
                        .WithMany("Usuarios")
                        .HasForeignKey("PerfilId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Perfil");
                });

            modelBuilder.Entity("HorasRaras.Domain.Entities.UsuarioProjetoEntity", b =>
                {
                    b.HasOne("HorasRaras.Domain.Entities.ProjetoEntity", "Projeto")
                        .WithMany("UsuariosProjeto")
                        .HasForeignKey("ProjetoId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("HorasRaras.Domain.Entities.UsuarioEntity", "Usuario")
                        .WithMany("UsuariosProjeto")
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Projeto");

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("HorasRaras.Domain.Entities.ClienteEntity", b =>
                {
                    b.Navigation("Projetos");
                });

            modelBuilder.Entity("HorasRaras.Domain.Entities.PerfilEntity", b =>
                {
                    b.Navigation("Usuarios");
                });

            modelBuilder.Entity("HorasRaras.Domain.Entities.ProjetoEntity", b =>
                {
                    b.Navigation("Tarefas");

                    b.Navigation("UsuariosProjeto");
                });

            modelBuilder.Entity("HorasRaras.Domain.Entities.UsuarioEntity", b =>
                {
                    b.Navigation("Tarefas");

                    b.Navigation("UsuariosProjeto");
                });
#pragma warning restore 612, 618
        }
    }
}
