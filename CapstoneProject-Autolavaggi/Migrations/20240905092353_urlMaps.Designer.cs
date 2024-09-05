﻿// <auto-generated />
using System;
using CapstoneProject_Autolavaggi.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CapstoneProject_Autolavaggi.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20240905092353_urlMaps")]
    partial class urlMaps
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("AutolavaggioServizio", b =>
                {
                    b.Property<int>("AutolavaggiId")
                        .HasColumnType("int");

                    b.Property<int>("ServiziId")
                        .HasColumnType("int");

                    b.HasKey("AutolavaggiId", "ServiziId");

                    b.HasIndex("ServiziId");

                    b.ToTable("AutolavaggioServizio");
                });

            modelBuilder.Entity("CapstoneProject_Autolavaggi.Models.Auth.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id")
                        .HasName("PK_Roles");

                    b.ToTable("Role");
                });

            modelBuilder.Entity("CapstoneProject_Autolavaggi.Models.Auth.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("NumeroTelefono")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("Id")
                        .HasName("PK_Users");

                    b.ToTable("User");
                });

            modelBuilder.Entity("CapstoneProject_Autolavaggi.Models.Auth.UserRole", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id")
                        .HasName("PK_UserRoles");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserId");

                    b.ToTable("UserRole");
                });

            modelBuilder.Entity("CapstoneProject_Autolavaggi.Models.Autolavaggio", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CAP")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Città")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Descrizione")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("GoogleMapsUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Immagine")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OrariDescrizione")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("OwnerId")
                        .HasColumnType("int");

                    b.Property<string>("Telefono")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("TipoId")
                        .HasColumnType("int");

                    b.Property<string>("TipoNome")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Via")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id")
                        .HasName("PK_Autolavaggi");

                    b.HasIndex("OwnerId");

                    b.HasIndex("TipoId");

                    b.ToTable("Autolavaggi");
                });

            modelBuilder.Entity("CapstoneProject_Autolavaggi.Models.Prenotazione", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AutolavaggioId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Data")
                        .HasColumnType("datetime2");

                    b.Property<int>("ServizioId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id")
                        .HasName("PK_Prenotazioni");

                    b.HasIndex("AutolavaggioId");

                    b.HasIndex("ServizioId");

                    b.HasIndex("UserId");

                    b.ToTable("Prenotazioni");
                });

            modelBuilder.Entity("CapstoneProject_Autolavaggi.Models.Recensione", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AutolavaggioId")
                        .HasColumnType("int");

                    b.Property<string>("Commento")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Data")
                        .HasColumnType("datetime2");

                    b.Property<string>("Titolo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("Voto")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AutolavaggioId");

                    b.HasIndex("UserId");

                    b.ToTable("Recensioni");
                });

            modelBuilder.Entity("CapstoneProject_Autolavaggi.Models.Servizio", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Costo")
                        .HasColumnType("int");

                    b.Property<string>("Descrizione")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Durata")
                        .HasColumnType("int");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Servizi");
                });

            modelBuilder.Entity("CapstoneProject_Autolavaggi.Models.Tipo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Descrizione")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Tipi");
                });

            modelBuilder.Entity("AutolavaggioServizio", b =>
                {
                    b.HasOne("CapstoneProject_Autolavaggi.Models.Autolavaggio", null)
                        .WithMany()
                        .HasForeignKey("AutolavaggiId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CapstoneProject_Autolavaggi.Models.Servizio", null)
                        .WithMany()
                        .HasForeignKey("ServiziId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CapstoneProject_Autolavaggi.Models.Auth.UserRole", b =>
                {
                    b.HasOne("CapstoneProject_Autolavaggi.Models.Auth.Role", "Role")
                        .WithMany("UserRoles")
                        .HasForeignKey("RoleId")
                        .IsRequired()
                        .HasConstraintName("FK_UserRoles_Roles");

                    b.HasOne("CapstoneProject_Autolavaggi.Models.Auth.User", "User")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserId")
                        .IsRequired()
                        .HasConstraintName("FK_UserRoles_Users");

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("CapstoneProject_Autolavaggi.Models.Autolavaggio", b =>
                {
                    b.HasOne("CapstoneProject_Autolavaggi.Models.Auth.User", "Owner")
                        .WithMany()
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("CapstoneProject_Autolavaggi.Models.Tipo", "Tipo")
                        .WithMany("Autolavaggi")
                        .HasForeignKey("TipoId");

                    b.Navigation("Owner");

                    b.Navigation("Tipo");
                });

            modelBuilder.Entity("CapstoneProject_Autolavaggi.Models.Prenotazione", b =>
                {
                    b.HasOne("CapstoneProject_Autolavaggi.Models.Autolavaggio", "Autolavaggio")
                        .WithMany("Prenotazioni")
                        .HasForeignKey("AutolavaggioId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("CapstoneProject_Autolavaggi.Models.Servizio", "Servizio")
                        .WithMany("Prenotazioni")
                        .HasForeignKey("ServizioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CapstoneProject_Autolavaggi.Models.Auth.User", "User")
                        .WithMany("Prenotazioni")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Autolavaggio");

                    b.Navigation("Servizio");

                    b.Navigation("User");
                });

            modelBuilder.Entity("CapstoneProject_Autolavaggi.Models.Recensione", b =>
                {
                    b.HasOne("CapstoneProject_Autolavaggi.Models.Autolavaggio", "Autolavaggio")
                        .WithMany("Recensioni")
                        .HasForeignKey("AutolavaggioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CapstoneProject_Autolavaggi.Models.Auth.User", "User")
                        .WithMany("Recensioni")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Autolavaggio");

                    b.Navigation("User");
                });

            modelBuilder.Entity("CapstoneProject_Autolavaggi.Models.Auth.Role", b =>
                {
                    b.Navigation("UserRoles");
                });

            modelBuilder.Entity("CapstoneProject_Autolavaggi.Models.Auth.User", b =>
                {
                    b.Navigation("Prenotazioni");

                    b.Navigation("Recensioni");

                    b.Navigation("UserRoles");
                });

            modelBuilder.Entity("CapstoneProject_Autolavaggi.Models.Autolavaggio", b =>
                {
                    b.Navigation("Prenotazioni");

                    b.Navigation("Recensioni");
                });

            modelBuilder.Entity("CapstoneProject_Autolavaggi.Models.Servizio", b =>
                {
                    b.Navigation("Prenotazioni");
                });

            modelBuilder.Entity("CapstoneProject_Autolavaggi.Models.Tipo", b =>
                {
                    b.Navigation("Autolavaggi");
                });
#pragma warning restore 612, 618
        }
    }
}
