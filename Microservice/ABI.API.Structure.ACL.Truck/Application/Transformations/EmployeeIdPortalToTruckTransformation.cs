using ABI.API.Structure.ACL.Truck.Application.DTO.Resource;
using ABI.API.Structure.ACL.Truck.Application.Transformations.Base;
using ABI.API.Structure.ACL.Truck.Infrastructure.Repositories.Interfaces;
using ABI.API.Structure.ACL.Truck.Repositories.Interfaces;
using ABI.Framework.MS.Helpers.Extensions;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ABI.API.Structure.ACL.Truck.Application.Transformations
{
    public class EmployeeIdPortalToTruckTransformation : TransformationBase
    {
        private readonly IMediator _mediator;
        private readonly IDBUHResourceRepository _dBUHResourceRepository;
        private readonly IStructureNodePortalRepository _repositoryStructureNode;

        public EmployeeIdPortalToTruckTransformation(IDBUHResourceRepository dBUHResourceRepository,
                                                        IStructureNodePortalRepository repositoryStructureNode,
                                                        IMediator mediator)
        {
            _dBUHResourceRepository = dBUHResourceRepository;
            _repositoryStructureNode = repositoryStructureNode;
            _mediator = mediator;
        }

        public EmployeeIdPortalToTruckTransformation()
        {

        }

        public override  async Task<object> DoTransform(object value)
        {
            var data = (dynamic)value;

            if (data != null)
            {
                var companyId = (data.CompanyId as string).ToInt();
                var leveltruck = (data.TypeEmployeeTruck as string).Trim().Split(',');
                var node = (data.Node as DTO.NodePortalTruckDTO);

                if (node.RoleId.HasValue && node.EmployeeId.HasValue)
                {
                    foreach (var itemlevel in leveltruck)
                    {
                        var resource = (Items as List<ResourceDTO>).FirstOrDefault(r => r.Id == node.EmployeeId.Value);

                        if (resource != null)
                        {
                            var relation = resource.Relations.FirstOrDefault(x => x.Attributes.VdrTpoCat == itemlevel);

                            if (relation != null)
                                return relation.Attributes.VdrCod.ToInt();
                        }
                    }
                }
                
                if(node.RoleId.HasValue && !node.EmployeeId.HasValue)
                {
                    return await GetEmpleadoTruckAsync(leveltruck, companyId, node);
                }

                if (!node.RoleId.HasValue && !node.EmployeeId.HasValue)
                {
                    return await GetEmpleadoTruckAsync(leveltruck, companyId, node);
                }

                if (!node.RoleId.HasValue && node.EmployeeId.HasValue)
                {
                    foreach (var itemlevel in leveltruck)
                    {
                        var resource = (Items as List<ResourceDTO>).FirstOrDefault(r => r.Id == node.EmployeeId.Value);

                        if (resource != null)
                        {
                            var relation = resource.Relations.FirstOrDefault(x => x.Attributes.VdrTpoCat == itemlevel);

                            await SetVacantEmployeeAsync(node, resource);

                            if (relation != null)
                                return relation.Attributes.VdrCod.ToInt();
                        }
                    }

                    var vacant = new ResourceDTO();

                  
                }

            }

            return null;
        }


        /// <summary>
        /// Gets the empleado truck.
        /// </summary>
        /// <param name="leveltruck">The leveltruck.</param>
        /// <param name="companyId">The company identifier.</param>
        /// <returns></returns>
        public async Task<int?> GetEmpleadoTruckAsync(string[] leveltruck, int companyId, DTO.NodePortalTruckDTO node)
        {
            foreach (var itemlevel in leveltruck)
            {
                if (itemlevel != "C")
                {
                    var vacant = await _dBUHResourceRepository.AddVacantResource(companyId, itemlevel);

                    if (vacant != null)
                    {
                        var relationVac = vacant.Relations.FirstOrDefault(x => x.Attributes.VdrTpoCat == itemlevel);

                        if (relationVac != null)
                        {
                            await SetVacantEmployeeAsync(node, vacant);

                            return relationVac.Attributes.VdrCod.ToInt();
                        }
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// Sets the vacant employee.
        /// </summary>
        /// <param name="node">The node.</param>
        /// <param name="vacant">The vacant.</param>
        private async Task SetVacantEmployeeAsync(DTO.NodePortalTruckDTO node, ResourceDTO vacant)
        {
            //Asigno empledo vacande de truck a portal

           // var nodeDefinition = await _mediator.Send(new GetOneNodeDefinitionByIdQuery { NodeDefinitionId = node.NodeDefinitionId.Value });

            var nodeDefinition = await _repositoryStructureNode.GetNodeDefinitionsByIdAsync(node.NodeDefinitionId.Value);

            if (nodeDefinition != null)
            {
                nodeDefinition.EditEmployeeId(vacant.Id);
                nodeDefinition.EditVacantPerson(true);

                _repositoryStructureNode.UpdateNodoDefinition(nodeDefinition);
                await _repositoryStructureNode.SaveEntitiesAsync();
            }
        }
    }
}
