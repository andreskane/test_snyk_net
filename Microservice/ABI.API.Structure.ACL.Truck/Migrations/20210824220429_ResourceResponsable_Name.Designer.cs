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
    [Migration("20210824220429_ResourceResponsable_Name")]
    partial class ResourceResponsable_Name
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.8")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ABI.API.Structure.ACL.Truck.Domain.Entities.BusinessTruckPortal", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("modelo_estructura_empresa_truck_id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("BusinessCode")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)")
                        .HasColumnName("modelo_estructura_empresa_truck_codigo");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("modelo_estructura_empresa_truck_nombre");

                    b.Property<int>("StructureModelId")
                        .HasColumnType("int")
                        .HasColumnName("modelo_estructurta_id");

                    b.HasKey("Id");

                    b.ToTable("Modelo_Estructura_Empresa_Truck", "acl");
                });

            modelBuilder.Entity("ABI.API.Structure.ACL.Truck.Domain.Entities.EstructuraClienteTerritorioIO", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("et_io_id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CliId")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("et_io_CliId");

                    b.Property<string>("CliNom")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("et_io_CliNom");

                    b.Property<string>("CliSts")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("et_io_CliSts");

                    b.Property<string>("CliTrrId")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("et_io_CliTrrId");

                    b.Property<string>("EmpId")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("et_io_EmpId");

                    b.Property<string>("GerenciaID")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("et_io_GerenciaID");

                    b.Property<int>("ImportProcessId")
                        .HasColumnType("int")
                        .HasColumnName("et_io_proceso_id");

                    b.Property<string>("Pais_Desc")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("et_io_pais_desc");

                    b.Property<string>("Pais_ID")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("et_io_pais_id");

                    b.HasKey("Id");

                    b.ToTable("et_io", "acl");
                });

            modelBuilder.Entity("ABI.API.Structure.ACL.Truck.Domain.Entities.ImportProcess", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("proceso_importacion_id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CompanyId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(max)")
                        .HasDefaultValue("01AR")
                        .HasColumnName("proceso_importacion_sociedad_id");

                    b.Property<string>("Condition")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("proceso_importacion_condicion");

                    b.Property<Guid>("CreatedBy")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("proceso_importacion_id_usuario_alta");

                    b.Property<string>("CreatedByName")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("proceso_importacion_nombre_usuario_alta");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("proceso_importacion_fecha_alta");

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("proceso_importacion_fecha_fin_ejecucion");

                    b.Property<int?>("From")
                        .HasColumnType("int")
                        .HasColumnName("proceso_importacion_desde");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit")
                        .HasColumnName("proceso_importacion_eliminado");

                    b.Property<Guid?>("LastModifiedBy")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("proceso_importacion_id_usuario_ultima_modificacion");

                    b.Property<string>("LastModifiedByName")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("proceso_importacion_nombre_usuario_ultima_modificacion");

                    b.Property<DateTime?>("LastModifiedDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("proceso_importacion_fecha_ultima_modificacion");

                    b.Property<int>("Periodicity")
                        .HasColumnType("int")
                        .HasColumnName("proceso_importacion_periodicidad");

                    b.Property<DateTime>("ProcessDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("proceso_importacion_fecha_proceso");

                    b.Property<int>("ProcessState")
                        .HasColumnType("int")
                        .HasColumnName("proceso_importacion_estado");

                    b.Property<int?>("ProcessedRows")
                        .HasColumnType("int")
                        .HasColumnName("proceso_importacion_cantidad_registros_procesados");

                    b.Property<int>("Source")
                        .HasColumnType("int")
                        .HasColumnName("proceso_importacion_origen");

                    b.Property<DateTime?>("StartDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("proceso_importacion_fecha_inicio_ejecucion");

                    b.Property<int?>("To")
                        .HasColumnType("int")
                        .HasColumnName("proceso_importacion_hasta");

                    b.HasKey("Id");

                    b.ToTable("proceso_importacion", "acl");
                });

            modelBuilder.Entity("ABI.API.Structure.ACL.Truck.Domain.Entities.LevelTruckPortal", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("nivel_truck_portal_id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("LevelPortalId")
                        .HasColumnType("int")
                        .HasColumnName("nivel_id");

                    b.Property<string>("LevelPortalName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("nivel_nombre");

                    b.Property<int>("LevelTruck")
                        .HasColumnType("int")
                        .HasColumnName("nivel_truck_portal_nivel_truck");

                    b.Property<string>("LevelTruckName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("nivel_truck_portal_nombre_truck");

                    b.Property<int?>("RolPortalId")
                        .HasColumnType("int")
                        .HasColumnName("rol_id");

                    b.Property<string>("TypeEmployeeTruck")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)")
                        .HasColumnName("nivel_truck_portal_tipo_Empleado");

                    b.HasKey("Id");

                    b.ToTable("Nivel_Truck_Portal", "acl");
                });

            modelBuilder.Entity("ABI.API.Structure.ACL.Truck.Domain.Entities.PeriodicityDays", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int")
                        .HasColumnName("dias_periodicidad_id");

                    b.Property<int>("DaysCount")
                        .HasColumnType("int")
                        .HasColumnName("dias_periodicidad_cantidad_dias");

                    b.HasKey("Id");

                    b.ToTable("dias_periodicidad", "acl");
                });

            modelBuilder.Entity("ABI.API.Structure.ACL.Truck.Domain.Entities.ResourceResponsable", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("recurso_responsable_id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CountryId")
                        .HasColumnType("int")
                        .HasColumnName("recurso_responsable_paisId");

                    b.Property<bool>("IsVacant")
                        .HasColumnType("bit")
                        .HasColumnName("recurso_responsable_vacante");

                    b.Property<int>("ResourceId")
                        .HasColumnType("int")
                        .HasColumnName("recurso_responsable_recurso_id");

                    b.Property<string>("TruckId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("recurso_responsable_truck_id");

                    b.Property<string>("VendorCategory")
                        .IsRequired()
                        .HasMaxLength(1)
                        .HasColumnType("nvarchar(1)")
                        .HasColumnName("recurso_responsable_categoria_vendedor");

                    b.HasKey("Id");

                    b.ToTable("Recurso_responsable", "acl");
                });

            modelBuilder.Entity("ABI.API.Structure.ACL.Truck.Domain.Entities.SyncLog", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("log_sincronizacion_id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("log_sincronizacion_mensaje");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("datetime2")
                        .HasColumnName("log_sincronizacion_timestamp");

                    b.HasKey("Id");

                    b.ToTable("Log_Sincronizacion", "acl");
                });

            modelBuilder.Entity("ABI.API.Structure.ACL.Truck.Domain.Entities.TypeVendorTruck", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("tipo_vendedor_truck_id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AttentionModeRoleId")
                        .HasColumnType("int")
                        .HasColumnName("modo_atencion_rol_id");

                    b.Property<int?>("VendorTruckId")
                        .HasColumnType("int")
                        .HasColumnName("vendedor_truck_id");

                    b.HasKey("Id");

                    b.ToTable("Tipo_Vendedor_Truck", "acl");
                });

            modelBuilder.Entity("ABI.API.Structure.ACL.Truck.Domain.Entities.TypeVendorTruckPortal", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("tipo_vendedor_truck_portal_id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("AttentionModeId")
                        .HasColumnType("int")
                        .HasColumnName("modo_atencion_id");

                    b.Property<bool?>("MappingTruckReading")
                        .HasColumnType("bit")
                        .HasColumnName("mapeo_truck_lectura");

                    b.Property<bool?>("MappingTruckWriting")
                        .HasColumnType("bit")
                        .HasColumnName("mapeo_truck_escritura");

                    b.Property<int?>("RoleId")
                        .HasColumnType("int")
                        .HasColumnName("rol_id");

                    b.Property<int>("VendorTruckId")
                        .HasColumnType("int")
                        .HasColumnName("vendedor_truck_id");

                    b.Property<string>("VendorTruckName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("vendedor_truck_Name");

                    b.HasKey("Id");

                    b.ToTable("Tipo_Vendedor_Truck_Portal", "acl");
                });

            modelBuilder.Entity("ABI.API.Structure.ACL.Truck.Domain.Entities.Versioned", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("versionado_id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTimeOffset>("Date")
                        .HasColumnType("datetimeoffset")
                        .HasColumnName("versionado_fecha_hora");

                    b.Property<DateTimeOffset?>("GenerateVersionDate")
                        .HasColumnType("datetimeoffset")
                        .HasColumnName("versionado_fecha_generado_version");

                    b.Property<int>("StatusId")
                        .HasColumnType("int")
                        .HasColumnName("versionado_estado_id");

                    b.Property<int>("StructureId")
                        .HasColumnType("int")
                        .HasColumnName("estructura_id");

                    b.Property<string>("User")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("versionado_usuario");

                    b.Property<DateTimeOffset>("Validity")
                        .HasColumnType("datetimeoffset")
                        .HasColumnName("versionado_vigencia");

                    b.Property<string>("Version")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)")
                        .HasColumnName("versionado_version");

                    b.HasKey("Id");

                    b.HasIndex("StatusId");

                    b.ToTable("Versionado", "acl");
                });

            modelBuilder.Entity("ABI.API.Structure.ACL.Truck.Domain.Entities.VersionedArista", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("versionado_arista_id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AristaId")
                        .HasColumnType("int")
                        .HasColumnName("arista_Id");

                    b.Property<int>("VersionedId")
                        .HasColumnType("int")
                        .HasColumnName("versionado_id");

                    b.HasKey("Id");

                    b.HasIndex("VersionedId");

                    b.ToTable("Versionado_Aristas", "acl");
                });

            modelBuilder.Entity("ABI.API.Structure.ACL.Truck.Domain.Entities.VersionedLog", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("versionado_log_Id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTimeOffset>("Date")
                        .HasColumnType("datetimeoffset")
                        .HasColumnName("versionado_log_fecha_hora");

                    b.Property<string>("Detaill")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("versionado_log_detalle");

                    b.Property<int>("LogStatusId")
                        .HasColumnType("int")
                        .HasColumnName("versionado_estado_log_Id");

                    b.Property<int>("VersionedId")
                        .HasColumnType("int")
                        .HasColumnName("versionado_id");

                    b.HasKey("Id");

                    b.HasIndex("LogStatusId");

                    b.HasIndex("VersionedId");

                    b.ToTable("Versionado_Log", "acl");
                });

            modelBuilder.Entity("ABI.API.Structure.ACL.Truck.Domain.Entities.VersionedLogStatus", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int")
                        .HasColumnName("versionado_estado_log_Id");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)")
                        .HasColumnName("versionado_estado_log_codigo");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("versionado_estado_log_nombre");

                    b.HasKey("Id");

                    b.ToTable("Versionado_Estado_Log", "acl");
                });

            modelBuilder.Entity("ABI.API.Structure.ACL.Truck.Domain.Entities.VersionedNode", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("versionado_nodo_id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("NodeDefinitionId")
                        .HasColumnType("int")
                        .HasColumnName("nodo_definicion_id");

                    b.Property<int>("NodeId")
                        .HasColumnType("int")
                        .HasColumnName("nodo_id");

                    b.Property<int>("VersionedId")
                        .HasColumnType("int")
                        .HasColumnName("versionado_id");

                    b.HasKey("Id");

                    b.HasIndex("VersionedId");

                    b.ToTable("Versionado_Nodo", "acl");
                });

            modelBuilder.Entity("ABI.API.Structure.ACL.Truck.Domain.Entities.VersionedStatus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("versionado_estado_id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("versionado_estado_nombre");

                    b.HasKey("Id");

                    b.ToTable("Versionado_Estado", "acl");
                });

            modelBuilder.Entity("ABI.API.Structure.ACL.Truck.Domain.Entities.Versioned", b =>
                {
                    b.HasOne("ABI.API.Structure.ACL.Truck.Domain.Entities.VersionedStatus", "VersionedStatus")
                        .WithMany("Versioneds")
                        .HasForeignKey("StatusId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("VersionedStatus");
                });

            modelBuilder.Entity("ABI.API.Structure.ACL.Truck.Domain.Entities.VersionedArista", b =>
                {
                    b.HasOne("ABI.API.Structure.ACL.Truck.Domain.Entities.Versioned", "Versioned")
                        .WithMany("VersionedsArista")
                        .HasForeignKey("VersionedId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Versioned");
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

                    b.Navigation("LogStatus");

                    b.Navigation("Versioned");
                });

            modelBuilder.Entity("ABI.API.Structure.ACL.Truck.Domain.Entities.VersionedNode", b =>
                {
                    b.HasOne("ABI.API.Structure.ACL.Truck.Domain.Entities.Versioned", "Versioned")
                        .WithMany("VersionedsNode")
                        .HasForeignKey("VersionedId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Versioned");
                });

            modelBuilder.Entity("ABI.API.Structure.ACL.Truck.Domain.Entities.Versioned", b =>
                {
                    b.Navigation("VersionedsArista");

                    b.Navigation("VersionedsLog");

                    b.Navigation("VersionedsNode");
                });

            modelBuilder.Entity("ABI.API.Structure.ACL.Truck.Domain.Entities.VersionedLogStatus", b =>
                {
                    b.Navigation("VersionedLogs");
                });

            modelBuilder.Entity("ABI.API.Structure.ACL.Truck.Domain.Entities.VersionedStatus", b =>
                {
                    b.Navigation("Versioneds");
                });
#pragma warning restore 612, 618
        }
    }
}
