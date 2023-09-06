﻿// <auto-generated />
using System;
using ABI.API.Structure.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ABI.API.Structure.Infrastructure.Migrations
{
    [DbContext(typeof(StructureContext))]
    [Migration("20210121213011_ADD_MotiveStateId")]
    partial class ADD_MotiveStateId
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ABI.API.Structure.Domain.AggregatesModel.StructureAggregate.StructureArista", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("arista_id")
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("MotiveStateId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("motivo_estado_id")
                        .HasColumnType("int")
                        .HasDefaultValue(2);

                    b.Property<int>("NodeIdFrom")
                        .HasColumnName("nodo_id_desde")
                        .HasColumnType("int");

                    b.Property<int>("NodeIdTo")
                        .HasColumnName("nodo_id_hasta")
                        .HasColumnType("int");

                    b.Property<int>("StructureIdFrom")
                        .HasColumnName("estructura_id_desde")
                        .HasColumnType("int");

                    b.Property<int>("StructureIdTo")
                        .HasColumnName("estructura_id_hasta")
                        .HasColumnType("int");

                    b.Property<int>("TypeRelationshipId")
                        .HasColumnName("tipo_relacion_id")
                        .HasColumnType("int");

                    b.Property<DateTime?>("ValidityFrom")
                        .HasColumnName("arista_vigencia_desde")
                        .HasColumnType("Date");

                    b.Property<DateTime>("ValidityTo")
                        .HasColumnName("arista_vigencia_hasta")
                        .HasColumnType("Date");

                    b.HasKey("Id");

                    b.HasIndex("MotiveStateId");

                    b.HasIndex("NodeIdFrom");

                    b.HasIndex("NodeIdTo");

                    b.HasIndex("StructureIdFrom");

                    b.HasIndex("StructureIdTo");

                    b.HasIndex("TypeRelationshipId");

                    b.ToTable("Arista","dbo");
                });

            modelBuilder.Entity("ABI.API.Structure.Domain.AggregatesModel.StructureAggregate.StructureDomain", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("estructura_id")
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("estructura_nombre")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<int?>("RootNodeId")
                        .HasColumnName("nodo_raiz_id")
                        .HasColumnType("int");

                    b.Property<int>("StructureModelId")
                        .HasColumnName("modelo_estructura_id")
                        .HasColumnType("int");

                    b.Property<DateTime?>("ValidityFrom")
                        .HasColumnName("estructura_vigencia_desde")
                        .HasColumnType("Date");

                    b.HasKey("Id");

                    b.HasIndex("RootNodeId");

                    b.HasIndex("StructureModelId");

                    b.ToTable("Estructura","dbo");
                });

            modelBuilder.Entity("ABI.API.Structure.Domain.AggregatesModel.StructureAggregate.StructureNode", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("nodo_id")
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnName("nodo_codigo")
                        .HasColumnType("nvarchar(10)")
                        .HasMaxLength(10);

                    b.Property<int>("LevelId")
                        .HasColumnName("nivel_id")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("LevelId");

                    b.ToTable("Nodo","dbo");
                });

            modelBuilder.Entity("ABI.API.Structure.Domain.AggregatesModel.StructureAggregate.StructureNodeDefinition", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("nodo_definicion_id")
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Active")
                        .HasColumnName("nodo_activo")
                        .HasColumnType("bit");

                    b.Property<int?>("AttentionModeId")
                        .HasColumnName("modo_atencion_id")
                        .HasColumnType("int");

                    b.Property<int?>("EmployeeId")
                        .HasColumnName("empleado_id")
                        .HasColumnType("int");

                    b.Property<int>("MotiveStateId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("motivo_estado_id")
                        .HasColumnType("int")
                        .HasDefaultValue(2);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("nodo_nombre")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<int>("NodeId")
                        .HasColumnName("nodo_id")
                        .HasColumnType("int");

                    b.Property<int?>("RoleId")
                        .HasColumnName("rol_id")
                        .HasColumnType("int");

                    b.Property<int?>("SaleChannelId")
                        .HasColumnName("canal_venta_id")
                        .HasColumnType("int");

                    b.Property<DateTime?>("ValidityFrom")
                        .HasColumnName("nodo_definicion_vigencia_desde")
                        .HasColumnType("date");

                    b.Property<DateTime>("ValidityTo")
                        .HasColumnName("nodo_definicion_vigencia_hasta")
                        .HasColumnType("date");

                    b.HasKey("Id");

                    b.HasIndex("AttentionModeId");

                    b.HasIndex("MotiveStateId");

                    b.HasIndex("NodeId");

                    b.HasIndex("RoleId");

                    b.HasIndex("SaleChannelId");

                    b.ToTable("Nodo_Definicion","dbo");
                });

            modelBuilder.Entity("ABI.API.Structure.Domain.Entities.AttentionMode", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("modo_atencion_id")
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool?>("Active")
                        .IsRequired()
                        .HasColumnName("modo_atencion_activo")
                        .HasColumnType("bit");

                    b.Property<string>("Description")
                        .HasColumnName("modo_atencion_descripcion")
                        .HasColumnType("nvarchar(140)")
                        .HasMaxLength(140);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("modo_atencion_nombre")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("ShortName")
                        .IsRequired()
                        .HasColumnName("modo_atencion_nombre_corto")
                        .HasColumnType("nvarchar(10)")
                        .HasMaxLength(10);

                    b.HasKey("Id");

                    b.ToTable("Modo_Atencion","dbo");
                });

            modelBuilder.Entity("ABI.API.Structure.Domain.Entities.AttentionModeRole", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("modo_atencion_rol_id")
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("AttentionModeId")
                        .HasColumnName("modo_atencion_id")
                        .HasColumnType("int");

                    b.Property<int?>("RoleId")
                        .HasColumnName("rol_id")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AttentionModeId");

                    b.HasIndex("RoleId");

                    b.ToTable("Modo_Atencion_Rol","dbo");
                });

            modelBuilder.Entity("ABI.API.Structure.Domain.Entities.ChangeTracking", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("seguimiento_cambio_id")
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("IdObject")
                        .HasColumnName("seguimiento_cambio_id_objeto")
                        .HasColumnType("int");

                    b.Property<int>("IdObjectType")
                        .HasColumnName("seguimiento_cambio_id_tipo_objeto")
                        .HasColumnType("int");

                    b.Property<int>("IdStructure")
                        .HasColumnName("seguimiento_cambio_id_estructura")
                        .HasColumnType("int");

                    b.Property<Guid>("IdUser")
                        .HasColumnName("seguimiento_cambio_id_usuario")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("NewValue")
                        .HasColumnName("seguimiento_cambio_valor_nuevo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OldValue")
                        .HasColumnName("seguimiento_cambio_valor_anterior")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnName("seguimiento_cambio_fecha")
                        .HasColumnType("datetime2");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnName("seguimiento_cambio_nombre_usuario")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.HasIndex("IdObjectType");

                    b.ToTable("Seguimiento_Cambio","dbo");
                });

            modelBuilder.Entity("ABI.API.Structure.Domain.Entities.Level", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("nivel_id")
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool?>("Active")
                        .IsRequired()
                        .HasColumnName("nivel_activo")
                        .HasColumnType("bit");

                    b.Property<string>("Description")
                        .HasColumnName("nivel_descripcion")
                        .HasColumnType("nvarchar(140)")
                        .HasMaxLength(140);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("nivel_nombre")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("ShortName")
                        .IsRequired()
                        .HasColumnName("nivel_nombre_corto")
                        .HasColumnType("nvarchar(10)")
                        .HasMaxLength(10);

                    b.HasKey("Id");

                    b.ToTable("Nivel","dbo");
                });

            modelBuilder.Entity("ABI.API.Structure.Domain.Entities.Motive", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("motivo_id")
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("motivo_nombre")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("Motivo","dbo");
                });

            modelBuilder.Entity("ABI.API.Structure.Domain.Entities.MotiveState", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("motivo_estado_id")
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("MotiveId")
                        .HasColumnName("motivo_id")
                        .HasColumnType("int");

                    b.Property<int>("StateId")
                        .HasColumnName("estado_id")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("MotiveId");

                    b.HasIndex("StateId");

                    b.ToTable("Motivo_Estado","dbo");
                });

            modelBuilder.Entity("ABI.API.Structure.Domain.Entities.ObjectType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("objeto_tipo_id")
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("objeto_tipo_nombre")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("Objeto_Tipo","dbo");
                });

            modelBuilder.Entity("ABI.API.Structure.Domain.Entities.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("rol_id")
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Active")
                        .HasColumnName("rol_activo")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("rol_nombre")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("ShortName")
                        .IsRequired()
                        .HasColumnName("rol_nombre_corto")
                        .HasColumnType("nvarchar(10)")
                        .HasMaxLength(10);

                    b.Property<string>("Tags")
                        .HasColumnName("rol_etiquetas")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.ToTable("Rol","dbo");
                });

            modelBuilder.Entity("ABI.API.Structure.Domain.Entities.SaleChannel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("canal_venta_id")
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool?>("Active")
                        .IsRequired()
                        .HasColumnName("canal_venta_activo")
                        .HasColumnType("bit");

                    b.Property<string>("Description")
                        .HasColumnName("canal_venta_descripcion")
                        .HasColumnType("nvarchar(140)")
                        .HasMaxLength(140);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("canal_venta_nombre")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("ShortName")
                        .IsRequired()
                        .HasColumnName("canal_venta_nombre_corto")
                        .HasColumnType("nvarchar(10)")
                        .HasMaxLength(10);

                    b.HasKey("Id");

                    b.ToTable("Canal_Venta","dbo");
                });

            modelBuilder.Entity("ABI.API.Structure.Domain.Entities.State", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("estado_id")
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("estado_nombre")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<int>("StateGroupId")
                        .HasColumnName("grupo_estado_id")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("StateGroupId");

                    b.ToTable("Estado","dbo");
                });

            modelBuilder.Entity("ABI.API.Structure.Domain.Entities.StateGroup", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("grupo_estado_id")
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("grupo_estado_nombre")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("Grupo_Estado","dbo");
                });

            modelBuilder.Entity("ABI.API.Structure.Domain.Entities.StateTruck", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("estado_truck_id")
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("estado_truck_nombre")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("Estado_Truck","dbo");
                });

            modelBuilder.Entity("ABI.API.Structure.Domain.Entities.StructureModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("modelo_estructura_id")
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool?>("Active")
                        .IsRequired()
                        .HasColumnName("modelo_estructura_activo")
                        .HasColumnType("bit");

                    b.Property<bool>("CanBeExportedToTruck")
                        .HasColumnName("modelo_estructura_exportar_truck")
                        .HasColumnType("bit");

                    b.Property<string>("Description")
                        .HasColumnName("modelo_estructura_descripcion")
                        .HasColumnType("nvarchar(140)")
                        .HasMaxLength(140);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("modelo_estructura_nombre")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("ShortName")
                        .IsRequired()
                        .HasColumnName("modelo_estructura_nombre_corto")
                        .HasColumnType("nvarchar(10)")
                        .HasMaxLength(10);

                    b.HasKey("Id");

                    b.ToTable("Modelo_Estructura","dbo");
                });

            modelBuilder.Entity("ABI.API.Structure.Domain.Entities.StructureModelDefinition", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("modelo_estructura_definicion_id")
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("IsAttentionModeRequired")
                        .HasColumnName("modelo_estructura_tiene_modo_atencion")
                        .HasColumnType("bit");

                    b.Property<bool>("IsSaleChannelRequired")
                        .HasColumnName("modelo_estructura_tiene_canal_venta")
                        .HasColumnType("bit");

                    b.Property<int>("LevelId")
                        .HasColumnName("nivel_id")
                        .HasColumnType("int");

                    b.Property<int?>("ParentLevelId")
                        .HasColumnName("padre_nivel_id")
                        .HasColumnType("int");

                    b.Property<int>("StructureModelId")
                        .HasColumnName("modelo_estructura_id")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("LevelId");

                    b.HasIndex("ParentLevelId");

                    b.HasIndex("StructureModelId");

                    b.ToTable("Modelo_Estructura_Definicion","dbo");
                });

            modelBuilder.Entity("ABI.API.Structure.Domain.Entities.Type", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("tipo_id")
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("tipo_nombre")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<int>("TypeGroupId")
                        .HasColumnName("grupo_tipo_id")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TypeGroupId");

                    b.ToTable("Tipo","dbo");
                });

            modelBuilder.Entity("ABI.API.Structure.Domain.Entities.TypeGroup", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("grupo_tipo_id")
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("grupo_tipo_nombre")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("Grupo_Tipo","dbo");
                });

            modelBuilder.Entity("ABI.API.Structure.Domain.AggregatesModel.StructureAggregate.StructureArista", b =>
                {
                    b.HasOne("ABI.API.Structure.Domain.Entities.MotiveState", "MotiveState")
                        .WithMany("StructureAristas")
                        .HasForeignKey("MotiveStateId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("ABI.API.Structure.Domain.AggregatesModel.StructureAggregate.StructureNode", "NodeFrom")
                        .WithMany("AristasFrom")
                        .HasForeignKey("NodeIdFrom")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("ABI.API.Structure.Domain.AggregatesModel.StructureAggregate.StructureNode", "NodeTo")
                        .WithMany("AristasTo")
                        .HasForeignKey("NodeIdTo")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("ABI.API.Structure.Domain.AggregatesModel.StructureAggregate.StructureDomain", "StructureFrom")
                        .WithMany("AristaFrom")
                        .HasForeignKey("StructureIdFrom")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("ABI.API.Structure.Domain.AggregatesModel.StructureAggregate.StructureDomain", "StructureTo")
                        .WithMany("AristaTo")
                        .HasForeignKey("StructureIdTo")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("ABI.API.Structure.Domain.Entities.Type", "TypeRelationship")
                        .WithMany("StructureArista")
                        .HasForeignKey("TypeRelationshipId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("ABI.API.Structure.Domain.AggregatesModel.StructureAggregate.StructureDomain", b =>
                {
                    b.HasOne("ABI.API.Structure.Domain.AggregatesModel.StructureAggregate.StructureNode", "Node")
                        .WithMany("Structures")
                        .HasForeignKey("RootNodeId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("ABI.API.Structure.Domain.Entities.StructureModel", "StructureModel")
                        .WithMany("Structures")
                        .HasForeignKey("StructureModelId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("ABI.API.Structure.Domain.AggregatesModel.StructureAggregate.StructureNode", b =>
                {
                    b.HasOne("ABI.API.Structure.Domain.Entities.Level", "Level")
                        .WithMany("StructureNodos")
                        .HasForeignKey("LevelId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("ABI.API.Structure.Domain.AggregatesModel.StructureAggregate.StructureNodeDefinition", b =>
                {
                    b.HasOne("ABI.API.Structure.Domain.Entities.AttentionMode", "AttentionMode")
                        .WithMany("StructureNodoDefinitions")
                        .HasForeignKey("AttentionModeId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("ABI.API.Structure.Domain.Entities.MotiveState", "MotiveState")
                        .WithMany("StructureNodoDefinitions")
                        .HasForeignKey("MotiveStateId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("ABI.API.Structure.Domain.AggregatesModel.StructureAggregate.StructureNode", "Node")
                        .WithMany("StructureNodoDefinitions")
                        .HasForeignKey("NodeId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("ABI.API.Structure.Domain.Entities.Role", "Role")
                        .WithMany("StructureNodoDefinitions")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("ABI.API.Structure.Domain.Entities.SaleChannel", "SaleChannel")
                        .WithMany("StructureNodoDefinitions")
                        .HasForeignKey("SaleChannelId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("ABI.API.Structure.Domain.Entities.AttentionModeRole", b =>
                {
                    b.HasOne("ABI.API.Structure.Domain.Entities.AttentionMode", "AttentionMode")
                        .WithMany("AttentionModeRoles")
                        .HasForeignKey("AttentionModeId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("ABI.API.Structure.Domain.Entities.Role", "Role")
                        .WithMany("AttentionModeRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("ABI.API.Structure.Domain.Entities.ChangeTracking", b =>
                {
                    b.HasOne("ABI.API.Structure.Domain.Entities.ObjectType", "ObjectType")
                        .WithMany("ChangeTracking")
                        .HasForeignKey("IdObjectType")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("ABI.API.Structure.Domain.Entities.MotiveState", b =>
                {
                    b.HasOne("ABI.API.Structure.Domain.Entities.Motive", "Motive")
                        .WithMany("MotiveStates")
                        .HasForeignKey("MotiveId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("ABI.API.Structure.Domain.Entities.State", "State")
                        .WithMany("MotiveStates")
                        .HasForeignKey("StateId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("ABI.API.Structure.Domain.Entities.State", b =>
                {
                    b.HasOne("ABI.API.Structure.Domain.Entities.StateGroup", "StateGroup")
                        .WithMany("States")
                        .HasForeignKey("StateGroupId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("ABI.API.Structure.Domain.Entities.StructureModelDefinition", b =>
                {
                    b.HasOne("ABI.API.Structure.Domain.Entities.Level", "Level")
                        .WithMany("StructureModelsDefinitions")
                        .HasForeignKey("LevelId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("ABI.API.Structure.Domain.Entities.Level", "ParentLevel")
                        .WithMany("ParentStructureModelsDefinitions")
                        .HasForeignKey("ParentLevelId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("ABI.API.Structure.Domain.Entities.StructureModel", "StructureModel")
                        .WithMany("StructureModelsDefinitions")
                        .HasForeignKey("StructureModelId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("ABI.API.Structure.Domain.Entities.Type", b =>
                {
                    b.HasOne("ABI.API.Structure.Domain.Entities.TypeGroup", "TypeGroup")
                        .WithMany("Types")
                        .HasForeignKey("TypeGroupId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
