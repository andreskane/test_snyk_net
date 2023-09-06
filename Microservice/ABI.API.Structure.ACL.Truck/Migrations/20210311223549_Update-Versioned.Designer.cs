﻿// <auto-generated />
using System;
using ABI.API.Structure.ACL.Truck.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ABI.API.Structure.ACL.Truck.Migrations
{
    [DbContext(typeof(TruckACLContext))]
    [Migration("20210311223549_Update-Versioned")]
    partial class UpdateVersioned
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ABI.API.Structure.ACL.Truck.Domain.Entities.BusinessTruckPortal", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("modelo_estructura_empresa_truck_id")
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("BusinessCode")
                        .IsRequired()
                        .HasColumnName("modelo_estructura_empresa_truck_codigo")
                        .HasColumnType("nvarchar(10)")
                        .HasMaxLength(10);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("modelo_estructura_empresa_truck_nombre")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<int>("StructureModelId")
                        .HasColumnName("modelo_estructurta_id")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Modelo_Estructura_Empresa_Truck","acl");
                });

            modelBuilder.Entity("ABI.API.Structure.ACL.Truck.Domain.Entities.ImpactStatus", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnName("impacto_estado_Id")
                        .HasColumnType("int");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnName("impacto_estado_codigo")
                        .HasColumnType("nvarchar(10)")
                        .HasMaxLength(10);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("impacto_estado_nombre")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.ToTable("Impacto_Estado","acl");
                });

            modelBuilder.Entity("ABI.API.Structure.ACL.Truck.Domain.Entities.LevelTruckPortal", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("nivel_truck_portal_id")
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("LevelPortalId")
                        .HasColumnName("nivel_id")
                        .HasColumnType("int");

                    b.Property<string>("LevelPortalName")
                        .IsRequired()
                        .HasColumnName("nivel_nombre")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<int>("LevelTruck")
                        .HasColumnName("nivel_truck_portal_nivel_truck")
                        .HasColumnType("int");

                    b.Property<string>("LevelTruckName")
                        .IsRequired()
                        .HasColumnName("nivel_truck_portal_nombre_truck")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<int?>("RolPortalId")
                        .HasColumnName("rol_id")
                        .HasColumnType("int");

                    b.Property<string>("TypeEmployeeTruck")
                        .IsRequired()
                        .HasColumnName("nivel_truck_portal_tipo_Empleado")
                        .HasColumnType("nvarchar(10)")
                        .HasMaxLength(10);

                    b.HasKey("Id");

                    b.ToTable("Nivel_Truck_Portal","acl");
                });

            modelBuilder.Entity("ABI.API.Structure.ACL.Truck.Domain.Entities.LogImpactTruck", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("log_impacto_truck_id")
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Date")
                        .HasColumnName("log_impacto_truck_fecha_hora")
                        .HasColumnType("datetime2");

                    b.Property<int?>("StructureId")
                        .HasColumnName("estructura_id")
                        .HasColumnType("int");

                    b.Property<string>("TruckVersion")
                        .IsRequired()
                        .HasColumnName("log_impacto_truck_version_truck")
                        .HasColumnType("nvarchar(10)")
                        .HasMaxLength(10);

                    b.HasKey("Id");

                    b.ToTable("Log_Impacto_Truck","acl");
                });

            modelBuilder.Entity("ABI.API.Structure.ACL.Truck.Domain.Entities.LogImpactTruckArista", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("log_impacto_truck_arista_id")
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AristaId")
                        .HasColumnName("arista_Id")
                        .HasColumnType("int");

                    b.Property<int>("LogImpactTruckId")
                        .HasColumnName("log_impacto_truck_id")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("LogImpactTruckId");

                    b.ToTable("Log_Impacto_Truck_Aristas","acl");
                });

            modelBuilder.Entity("ABI.API.Structure.ACL.Truck.Domain.Entities.LogImpactTruckNode", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("log_impacto_truck_nodo_id")
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("LogImpactTruckId")
                        .HasColumnName("log_impacto_truck_id")
                        .HasColumnType("int");

                    b.Property<int>("NodeDefinitionId")
                        .HasColumnName("nodo_definicion_id")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("LogImpactTruckId");

                    b.ToTable("Log_Impacto_Truck_Nodos","acl");
                });

            modelBuilder.Entity("ABI.API.Structure.ACL.Truck.Domain.Entities.LogImpactTruckStatus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("log_Impacto_truck_Estado_Id")
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Date")
                        .HasColumnName("log_impacto_truck_fecha_hora")
                        .HasColumnType("datetime2");

                    b.Property<int>("LogImpactTruckId")
                        .HasColumnName("log_impacto_truck_id")
                        .HasColumnType("int");

                    b.Property<string>("LogTruck")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("StatusId")
                        .HasColumnName("log_impacto_truck_estado")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("LogImpactTruckId");

                    b.HasIndex("StatusId");

                    b.ToTable("Log_Impacto_Truck_Estado","acl");
                });

            modelBuilder.Entity("ABI.API.Structure.ACL.Truck.Domain.Entities.LogImpactTruckTrays", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("log_impacto_truck_bandeja_id")
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("LogImpactTruckId")
                        .HasColumnName("log_impacto_truck_id")
                        .HasColumnType("int");

                    b.Property<string>("TruckTrays")
                        .HasColumnName("log_impacto_truck_bandejas")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("LogImpactTruckId");

                    b.ToTable("Log_Impacto_Truck_Bandejas","acl");
                });

            modelBuilder.Entity("ABI.API.Structure.ACL.Truck.Domain.Entities.TypeVendorTruck", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("tipo_vendedor_truck_id")
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AttentionModeRoleId")
                        .HasColumnName("modo_atencion_rol_id")
                        .HasColumnType("int");

                    b.Property<int?>("VendorTruckId")
                        .HasColumnName("vendedor_truck_id")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Tipo_Vendedor_Truck","acl");
                });

            modelBuilder.Entity("ABI.API.Structure.ACL.Truck.Domain.Entities.TypeVendorTruckPortal", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("tipo_vendedor_truck_portal_id")
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("AttentionModeId")
                        .HasColumnName("modo_atencion_id")
                        .HasColumnType("int");

                    b.Property<bool?>("MappingTruckReading")
                        .HasColumnName("mapeo_truck_lectura")
                        .HasColumnType("bit");

                    b.Property<bool?>("MappingTruckWriting")
                        .HasColumnName("mapeo_truck_escritura")
                        .HasColumnType("bit");

                    b.Property<int?>("RoleId")
                        .HasColumnName("rol_id")
                        .HasColumnType("int");

                    b.Property<int>("VendorTruckId")
                        .HasColumnName("vendedor_truck_id")
                        .HasColumnType("int");

                    b.Property<string>("VendorTruckName")
                        .IsRequired()
                        .HasColumnName("vendedor_truck_Name")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("Tipo_Vendedor_Truck_Portal","acl");
                });

            modelBuilder.Entity("ABI.API.Structure.ACL.Truck.Domain.Entities.Versioned", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("versionado_id")
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Date")
                        .HasColumnName("versionado_fecha_hora")
                        .HasColumnType("datetime");

                    b.Property<DateTime?>("GenerateVersionDate")
                        .HasColumnName("versionado_fecha_generado_version")
                        .HasColumnType("datetime");

                    b.Property<int>("StatusId")
                        .HasColumnName("versionado_estado_id")
                        .HasColumnType("int");

                    b.Property<int>("StructureId")
                        .HasColumnName("estructura_id")
                        .HasColumnType("int");

                    b.Property<string>("User")
                        .IsRequired()
                        .HasColumnName("versionado_usuario")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<DateTime>("Validity")
                        .HasColumnName("versionado_vigencia")
                        .HasColumnType("date");

                    b.Property<string>("Version")
                        .HasColumnName("versionado_version")
                        .HasColumnType("nvarchar(10)")
                        .HasMaxLength(10);

                    b.HasKey("Id");

                    b.HasIndex("StatusId");

                    b.ToTable("Versionado","acl");
                });

            modelBuilder.Entity("ABI.API.Structure.ACL.Truck.Domain.Entities.VersionedArista", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("versionado_arista_id")
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AristaId")
                        .HasColumnName("arista_Id")
                        .HasColumnType("int");

                    b.Property<int>("VersionedId")
                        .HasColumnName("versionado_id")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("VersionedId");

                    b.ToTable("Versionado_Aristas","acl");
                });

            modelBuilder.Entity("ABI.API.Structure.ACL.Truck.Domain.Entities.VersionedLog", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("versionado_log_Id")
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Date")
                        .HasColumnName("versionado_log_fecha_hora")
                        .HasColumnType("datetime2");

                    b.Property<string>("Detaill")
                        .IsRequired()
                        .HasColumnName("versionado_log_detalle")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("LogStatusId")
                        .HasColumnName("versionado_estado_log_Id")
                        .HasColumnType("int");

                    b.Property<int>("VersionedId")
                        .HasColumnName("versionado_id")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("LogStatusId");

                    b.HasIndex("VersionedId");

                    b.ToTable("Versionado_Log","acl");
                });

            modelBuilder.Entity("ABI.API.Structure.ACL.Truck.Domain.Entities.VersionedLogStatus", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnName("versionado_estado_log_Id")
                        .HasColumnType("int");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnName("versionado_estado_log_codigo")
                        .HasColumnType("nvarchar(10)")
                        .HasMaxLength(10);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("versionado_estado_log_nombre")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.ToTable("Versionado_Estado_Log","acl");
                });

            modelBuilder.Entity("ABI.API.Structure.ACL.Truck.Domain.Entities.VersionedNode", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("versionado_nodo_id")
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("NodeDefinitionId")
                        .HasColumnName("nodo_definicion_id")
                        .HasColumnType("int");

                    b.Property<int>("NodeId")
                        .HasColumnName("nodo_id")
                        .HasColumnType("int");

                    b.Property<int>("VersionedId")
                        .HasColumnName("versionado_id")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("VersionedId");

                    b.ToTable("Versionado_Nodo","acl");
                });

            modelBuilder.Entity("ABI.API.Structure.ACL.Truck.Domain.Entities.VersionedStatus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("versionado_estado_id")
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("versionado_estado_nombre")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("Versionado_Estado","acl");
                });

            modelBuilder.Entity("ABI.API.Structure.ACL.Truck.Domain.Entities.LogImpactTruckArista", b =>
                {
                    b.HasOne("ABI.API.Structure.ACL.Truck.Domain.Entities.LogImpactTruck", "LogImpactTruck")
                        .WithMany("LogsImpactTruckArista")
                        .HasForeignKey("LogImpactTruckId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("ABI.API.Structure.ACL.Truck.Domain.Entities.LogImpactTruckNode", b =>
                {
                    b.HasOne("ABI.API.Structure.ACL.Truck.Domain.Entities.LogImpactTruck", "LogImpactTruck")
                        .WithMany("LogsImpactTruckNode")
                        .HasForeignKey("LogImpactTruckId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("ABI.API.Structure.ACL.Truck.Domain.Entities.LogImpactTruckStatus", b =>
                {
                    b.HasOne("ABI.API.Structure.ACL.Truck.Domain.Entities.LogImpactTruck", "LogImpactTruck")
                        .WithMany("LogsImpactTruckStatus")
                        .HasForeignKey("LogImpactTruckId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("ABI.API.Structure.ACL.Truck.Domain.Entities.ImpactStatus", "Status")
                        .WithMany("LogsImpactTruckStatus")
                        .HasForeignKey("StatusId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("ABI.API.Structure.ACL.Truck.Domain.Entities.LogImpactTruckTrays", b =>
                {
                    b.HasOne("ABI.API.Structure.ACL.Truck.Domain.Entities.LogImpactTruck", "LogImpactTruck")
                        .WithMany("LogsImpactTruckTrays")
                        .HasForeignKey("LogImpactTruckId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("ABI.API.Structure.ACL.Truck.Domain.Entities.Versioned", b =>
                {
                    b.HasOne("ABI.API.Structure.ACL.Truck.Domain.Entities.VersionedStatus", "VersionedStatus")
                        .WithMany("Versioneds")
                        .HasForeignKey("StatusId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("ABI.API.Structure.ACL.Truck.Domain.Entities.VersionedArista", b =>
                {
                    b.HasOne("ABI.API.Structure.ACL.Truck.Domain.Entities.Versioned", "Versioned")
                        .WithMany("VersionedsArista")
                        .HasForeignKey("VersionedId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("ABI.API.Structure.ACL.Truck.Domain.Entities.VersionedLog", b =>
                {
                    b.HasOne("ABI.API.Structure.ACL.Truck.Domain.Entities.VersionedLogStatus", "LogStatus")
                        .WithMany("VersionedLogs")
                        .HasForeignKey("LogStatusId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("ABI.API.Structure.ACL.Truck.Domain.Entities.Versioned", "Versioned")
                        .WithMany("VersionedsLog")
                        .HasForeignKey("VersionedId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("ABI.API.Structure.ACL.Truck.Domain.Entities.VersionedNode", b =>
                {
                    b.HasOne("ABI.API.Structure.ACL.Truck.Domain.Entities.Versioned", "Versioned")
                        .WithMany("VersionedsNode")
                        .HasForeignKey("VersionedId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
