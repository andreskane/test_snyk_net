using ABI.API.Structure.ACL.Truck.Application.DTO.ImportProcess;
using ABI.API.Structure.ACL.Truck.Application.Service.ACL.Models;
using ABI.API.Structure.ACL.Truck.Application.Service.Models;
using ABI.API.Structure.ACL.Truck.Domain.Entities;
using ABI.API.Structure.Application.DTO;
using ABI.API.Structure.Domain.AggregatesModel.StructureAggregate;
using ABI.API.Structure.Domain.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace ABI.API.Structure.Application.Infrastructure
{
    [ExcludeFromCodeCoverage]
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            // Hacia la base
            CreateMap<RoleDTO, Role>()
                .ForMember(m => m.StructureNodoDefinitions, o => o.Ignore())
                .ForMember(m => m.AttentionModeRoles, o => o.MapFrom(f => new List<AttentionModeRole>
                {
                    new AttentionModeRole
                    {
                        AttentionModeId = f.AttentionMode.Id ?? default(int?),
                        RoleId = f.Id ?? 0
                    }
                }))
                .ForMember(m => m.Tags, o => o.MapFrom(f => string.Join(",", f.Tag)));

            CreateMap<AttentionModeDTO, AttentionMode>()
                .ForMember(m => m.AttentionModeRoles, o => o.Ignore())
                .ForMember(m => m.StructureNodoDefinitions, o => o.Ignore());

            CreateMap<LevelDTO, Level>()
                .ForMember(m => m.StructureModelsDefinitions, o => o.Ignore())
                .ForMember(m => m.ParentStructureModelsDefinitions, o => o.Ignore())
                .ForMember(m => m.StructureNodos, o => o.Ignore());

            CreateMap<StructureModelDTO, StructureModel>()
             .ForMember(m => m.StructureModelsDefinitions, o => o.Ignore())
             .ForMember(m => m.Structures, o => o.Ignore())
             .ForMember(m => m.Country, o => o.Ignore());

     
            CreateMap<StructureModelDefinitionDTO, StructureModelDefinition>()
                .ForMember(m => m.StructureModel, o => o.Ignore())
                .ForMember(m => m.Name, o => o.Ignore())
                .ForMember(m => m.ParentLevel, o => o.Ignore())
                .ForMember(m => m.StructureModelId, o => o.MapFrom(f => f.StructureModelId));

            CreateMap<SaleChannelDTO, SaleChannel>()
                   .ForMember(m => m.StructureNodoDefinitions, o => o.Ignore());

            CreateMap<StructureClientDTO, StructureClientNode>()
                    .ForMember(m => m.Node, o => o.Ignore())
                    .ForMember(m => m.DomainEvents, o=> o.Ignore())
                    .ForMember(m => m.ClientState, o => o.MapFrom(f => f.State));

            // Hacia el frontend
            CreateMap<Role, RoleDTO>()
                .ForMember(m => m.Erasable, o => o.Ignore())
                .ForMember(m => m.AttentionMode, o => o.Ignore())
                .ForMember(m => m.Tag, o => o.MapFrom(f =>
                    string.IsNullOrEmpty(f.Tags)
                        ? new string[0]
                        : f.Tags.Split(',', StringSplitOptions.RemoveEmptyEntries)
                ))
                .AfterMap((s, d, ctx) =>
                {
                    var attentionRole = s.AttentionModeRoles.FirstOrDefault(f => f.RoleId == d.Id);
                    var attentionMode = attentionRole?.AttentionMode;

                    if (attentionMode != null)
                        d.AttentionMode = ctx.Mapper.Map<ItemDTO>(attentionMode);
                });

            CreateMap<AttentionMode, ItemDTO>();

            CreateMap<AttentionMode, AttentionModeDTO>()
                .ForMember(m => m.Role, o => o.Ignore())
                .ForMember(m => m.Erasable, o => o.MapFrom(f => f.AttentionModeRoles != null && !f.AttentionModeRoles.Any()));

            CreateMap<AttentionModeDTO, ItemDTO>()
                .ForMember(m => m.Description, o => o.Ignore());
            
            CreateMap<StructureDomainDTO, StructureDomainV2DTO>();
            CreateMap<StructureDTO, StructureV2DTO>();
            CreateMap<NodeDTO, NodeV2DTO>();
            CreateMap<NodeVersionDTO, NodeVersionV2DTO>();

            CreateMap<AttentionModelRolTypeVender, AttentionModeRoleConfigurationDTO>()
                .AfterMap((s, d, ctx) =>
                {
                    if (s.AttentionMode != null)
                    {
                        d.AttentionModeId = s.AttentionMode.Id;
                        d.AttentionModeName = s.AttentionMode.Name;
                    }

                    if (s.Role != null)
                    {
                        d.RoleId = s.Role.Id;
                        d.RoleName = s.Role.Name;
                    }
                });

            CreateMap<TypeVendor, ItemDTO>();

            CreateMap<Role, ItemDTO>()
                .ForMember(m => m.Description, o => o.Ignore());

            CreateMap<RoleDTO, ItemDTO>()
                .ForMember(m => m.Description, o => o.Ignore());

            CreateMap<IList<string>, RoleTagDTO>()
                .ForMember(m => m.Tags, o => o.MapFrom(f => f.ToList()));

            CreateMap<Level, LevelDTO>()
                .ForMember(m => m.Erasable, o => o.MapFrom(f =>
                    (f.StructureModelsDefinitions != null && !f.StructureModelsDefinitions.Any()) ||
                    (f.ParentStructureModelsDefinitions != null && !f.ParentStructureModelsDefinitions.Any())
                ));

            CreateMap<Level, ItemDTO>();

            CreateMap<LevelDTO, ItemDTO>()
                .ForMember(m => m.Description, o => o.Ignore());

            CreateMap<SaleChannel, SaleChannelDTO>()
                .ForMember(m => m.Erasable, o => o.Ignore());

            CreateMap<SaleChannel, ItemDTO>();

            CreateMap<SaleChannelDTO, ItemDTO>()
                .ForMember(m => m.Description, o => o.Ignore());

            CreateMap<StructureModel, StructureModelDTO>()
                .ForMember(m => m.StructureModelSourceId, o => o.Ignore())
                .ForMember(m => m.InUse, o => o.MapFrom(f => f.Structures != null && f.Structures.Any()))
                .ForMember(m => m.Erasable, o => o.MapFrom(f => f.Structures != null && !f.Structures.Any())
                );

            CreateMap<StructureModel, StructureModelV2DTO>()
                .ForMember(x => x.Erasable, y => y.Ignore())
                .ForMember(x => x.Definitions, y => y.Ignore());

            CreateMap<StructureModel, ItemDTO>();

            CreateMap<StructureModelDefinition, StructureModelDefinitionDTO>()
                .ForMember(m => m.Erasable, o => o.Ignore());

            CreateMap<StructureModelDefinition, StructureModelDefinitionV2DTO>();

            CreateMap<StructureDomain, StructureDTO>()
                .ForMember(m => m.ThereAreChangesWithoutSaving, o => o.Ignore())
                .ForMember(m => m.ThereAreScheduledChanges, o => o.Ignore())
                .ForMember(m => m.Erasable, o => o.Ignore())
                .ForMember(m => m.Processing, o => o.Ignore())
                .ForMember(m => m.ValidityFrom, o => o.MapFrom(f => f.ValidityFrom))
                .ForMember(m => m.FirstNodeName, o => o.MapFrom(f =>
                    f.Node.StructureNodoDefinitions.Any()
                        ? f.Node.StructureNodoDefinitions.First().Name
                        : null
                ))
                .ForMember(m => m.StructureModel, o => o.MapFrom(f =>
                    f.StructureModel != null
                        ? new StructureModelDTO
                        {
                            Id = f.StructureModelId,
                            Name = f.StructureModel.Name,
                            ShortName = f.StructureModel.ShortName,
                            Description = f.StructureModel.Description,
                            Active = f.StructureModel.Active.Value,
                            CanBeExportedToTruck = f.StructureModel.CanBeExportedToTruck,
                            Code = f.StructureModel.Code,
                            CountryId = f.StructureModel.CountryId
                        }
                        : null
                ))

                .ForPath(c => c.StructureModel.Country, o => o.MapFrom(co =>
                      co.StructureModel != null && co.StructureModel.Country != null
                      ? new CountryDTO
                      {
                          Id = co.StructureModel.Country.Id,
                          Name = co.StructureModel.Country.Name,
                          Code = co.StructureModel.Country.Code
                      }
                      : null
                      ))
                ;

            CreateMap < StructureBranchNodeDTO, StructureBranchTerritoryDTO> ()
                .ForMember(m => m.Clients, o => o.Ignore());
           
            CreateMap<EstructuraClienteTerritorioIO, DataIODto>();

            CreateMap<Country, CountryDTO>();

            CreateMap<StructureClientNode, StructureClientDTO>()
                .ForMember(m => m.State, f => f.MapFrom(f => f.ClientState));

            CreateMap<StructureClientNode, DataIODto>()
                .ForMember(m => m.CliId, f => f.MapFrom(f => f.ClientId))
                .ForMember(m => m.CliNom, f => f.MapFrom(f => f.Name))
                .ForMember(m => m.CliSts, f => f.MapFrom(f => f.ClientState))
                .ForMember(m => m.CliTrrId, f => f.MapFrom(f => f.Node.Code))
                .ForMember(m => m.EmpId, f => f.Ignore());

            CreateMap<MostVisitedFilter, MostVisitedFilterDto>()
                .ForMember(m => m.Name, f => f.MapFrom(f => f.Description))
                .ForMember(m => m.Value, f => f.MapFrom(f => f.IdValue))
                .ForMember(m => m.FilterType, f => f.MapFrom(f => f.FilterType));

            CreateMap<MostVisitedFilterDto, MostVisitedFilter>()
                .ForMember(m => m.Description, f => f.MapFrom(f => f.Name))
                .ForMember(m => m.IdValue, f => f.MapFrom(f => f.Value))
                .ForMember(m => m.FilterType, f => f.MapFrom(f => f.FilterType))
                .ForMember(m => m.UserId, f => f.Ignore())
                .ForMember(m => m.Id, f => f.Ignore())
                .ForMember(m => m.StructureId, f => f.Ignore())
                .ForMember(m => m.Quantity, f => f.Ignore());
        }
    }
}
