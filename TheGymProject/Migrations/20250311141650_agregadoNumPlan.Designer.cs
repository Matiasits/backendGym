﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace TheGymProject.Migrations
{
    [DbContext(typeof(GimnasioDbContext))]
    [Migration("20250311141650_agregadoNumPlan")]
    partial class agregadoNumPlan
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.21")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Alumno", b =>
                {
                    b.Property<int>("AlumnoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Apellido")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("DNI")
                        .HasColumnType("int");

                    b.Property<int>("DiasAdicionales")
                        .HasColumnType("int");

                    b.Property<string>("Domicilio")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("FechaRegistro")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("NumeroPlan")
                        .HasColumnType("int");

                    b.Property<int>("PlanId")
                        .HasColumnType("int");

                    b.Property<string>("Telefono")
                        .HasColumnType("longtext");

                    b.Property<string>("TelefonoEmergencia")
                        .HasColumnType("longtext");

                    b.HasKey("AlumnoId");

                    b.HasIndex("DNI")
                        .IsUnique();

                    b.HasIndex("PlanId");

                    b.ToTable("Alumno");
                });

            modelBuilder.Entity("AlumnoPlan", b =>
                {
                    b.Property<int>("AlumnoPlanId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("AlumnoId")
                        .HasColumnType("int");

                    b.Property<DateTime>("FHInicio")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("FHVencimiento")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("PlanId")
                        .HasColumnType("int");

                    b.HasKey("AlumnoPlanId");

                    b.HasIndex("AlumnoId");

                    b.HasIndex("PlanId");

                    b.ToTable("AlumnoPlan");
                });

            modelBuilder.Entity("Asistencia", b =>
                {
                    b.Property<int>("AsistenciaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("AlumnoId")
                        .HasColumnType("int");

                    b.Property<DateTime>("FHRegistro")
                        .HasColumnType("datetime(6)");

                    b.HasKey("AsistenciaId");

                    b.HasIndex("AlumnoId");

                    b.HasIndex("FHRegistro");

                    b.ToTable("Asistencia");
                });

            modelBuilder.Entity("Plan", b =>
                {
                    b.Property<int>("PlanId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("FHInicio")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("FHVencimiento")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<decimal>("Precio")
                        .HasColumnType("decimal(10,2)");

                    b.HasKey("PlanId");

                    b.ToTable("Planes");
                });

            modelBuilder.Entity("Profesor", b =>
                {
                    b.Property<int>("ProfesorId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Apellido")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("ProfesorId");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("Profesor");
                });

            modelBuilder.Entity("Alumno", b =>
                {
                    b.HasOne("Plan", "Plan")
                        .WithMany()
                        .HasForeignKey("PlanId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Plan");
                });

            modelBuilder.Entity("AlumnoPlan", b =>
                {
                    b.HasOne("Alumno", "Alumno")
                        .WithMany("AlumnoPlanes")
                        .HasForeignKey("AlumnoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Plan", "Plan")
                        .WithMany("AlumnoPlanes")
                        .HasForeignKey("PlanId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Alumno");

                    b.Navigation("Plan");
                });

            modelBuilder.Entity("Asistencia", b =>
                {
                    b.HasOne("Alumno", "Alumno")
                        .WithMany("Asistencias")
                        .HasForeignKey("AlumnoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Alumno");
                });

            modelBuilder.Entity("Alumno", b =>
                {
                    b.Navigation("AlumnoPlanes");

                    b.Navigation("Asistencias");
                });

            modelBuilder.Entity("Plan", b =>
                {
                    b.Navigation("AlumnoPlanes");
                });
#pragma warning restore 612, 618
        }
    }
}
