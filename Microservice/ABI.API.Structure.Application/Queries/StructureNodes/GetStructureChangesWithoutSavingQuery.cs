using ABI.API.Structure.Application.DTO;
using ABI.API.Structure.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace ABI.API.Structure.Application.Queries.StructureNodes
{
    public class GetStructureChangesWithoutSavingQuery : IRequest<IList<StructureNodeChangesDTO>>
    {
        public int StructureId { get; set; }

        public DateTimeOffset ValidityFrom { get; set; }
    }

    public class GetStructureChangesWithoutSavingQueryHandler : IRequestHandler<GetStructureChangesWithoutSavingQuery, IList<StructureNodeChangesDTO>>
    {
        private readonly IMediator _mediator;


        public GetStructureChangesWithoutSavingQueryHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<IList<StructureNodeChangesDTO>> Handle(GetStructureChangesWithoutSavingQuery request, CancellationToken cancellationToken)
        {
            var list = new List<StructureNodeChangesDTO>();
            
            var result = await _mediator.Send(new GetStructurePendingChangesDetailQuery { StructureId = request.StructureId, ValidityFrom = request.ValidityFrom });
            if (result.Count == 0)
                return list;

            // Nodo definicion
            foreach (var item in result.ToList())
            {
                var node1 = item.Nodes.FirstOrDefault(n =>
                    n.NodeMotiveStateId == (int)MotiveStateNode.Confirmed &&
                    n.ValidityFrom <= request.ValidityFrom &&
                    n.ValidityTo >= request.ValidityFrom
                );
                var node2 = item.Nodes.FirstOrDefault(n => n.NodeMotiveStateId == (int)MotiveStateNode.Draft);

                //Si no hay cambios en Draft no debería comparar porque o no hay cambios o es un cambio de arista
                var change = CompareNodes(node1, node2);
                if (change != null)
                    list.Add(change);
            }

            // Aristas
            foreach (var item in result.ToList())
            {
                var node1 = item.Nodes.FirstOrDefault(n =>
                    n.AristaMotiveStateId == (int)MotiveStateNode.Confirmed &&
                    n.ValidityFrom <= request.ValidityFrom &&
                    n.ValidityTo >= request.ValidityFrom
                );
                var node2 = item.Nodes.FirstOrDefault(n => n.AristaMotiveStateId == (int)MotiveStateNode.Draft);

                if (node1 == null || node2 == null)
                    continue;

                var change = CompareNodes(node1, node2);
                if (change != null)
                {
                    var exists = list.FirstOrDefault(f => f.NodeId == change.NodeId);

                    if (exists != null)
                        exists.Changes.AddRange(change.Changes);
                    else
                        list.Add(change);
                }
            }
            return list;
        }


        /// <summary>
        /// Compares the nodes.
        /// </summary>
        /// <param name="node1">The node1.</param>
        /// <param name="node2">The node2.</param>
        /// <returns></returns>
        private StructureNodeChangesDTO CompareNodes(ChangeNodeDTO node1, ChangeNodeDTO node2)
        {
            if (node2 != null)
            {
                var node = new StructureNodeChangesDTO();

                if (node1 != null)
                {
                    node.NodeId = node1.NodeId;
                    node.Description = $"{node1.Level}: {node1.Code} - {node1.Name.ToUpper()}";
                }
                else
                {
                    node.NodeId = node2.NodeId;
                    node.Description = $"{node2.Level}: {node2.Code} - {node2.Name.ToUpper()}";
                    node.NewNode = true;
                    node1 = new ChangeNodeDTO();
                }

                PropertyInfo[] Props1 = node1.GetType().GetProperties();
                PropertyInfo[] Props2 = node2.GetType().GetProperties();

                foreach (PropertyInfo pInfo1 in Props1)
                {

                    PropertyInfo pInfo2 = Props2.FirstOrDefault(f => f.Name == pInfo1.Name);

                    var value1 = pInfo1.GetValue(node1, null);
                    var value2 = pInfo2.GetValue(node2, null);

                    CompareChanges(node1, node2, node, pInfo1, value1, value2);
                }
                return node; 
            }
            return null;
        }


        /// <summary>
        /// Compares the changes.
        /// </summary>
        /// <param name="node1">The node1.</param>
        /// <param name="nodeChanges">The node.</param>
        /// <param name="pInfo1">The p info1.</param>
        /// <param name="value1">The value1.</param>
        /// <param name="value2">The value2.</param>
        private void CompareChanges(ChangeNodeDTO node1, ChangeNodeDTO node2, StructureNodeChangesDTO nodeChanges, PropertyInfo pInfo1, object value1, object value2)
        {
            var validProperties = new List<string>
            {
                "Name",
                "Code",
                "Active",
                "AttentionModeName",
                "Role",
                "EmployeeId",
                "SaleChannel",
                "NodeParentId"
            };

            if (!Equals(value1, value2) && validProperties.Any(name => pInfo1.Name.Equals(name)))
            {
                var change = new ChangesWithoutSavingDTO
                {
                    Value = string.Empty,
                    ValueNew = value2 != null ? value2.ToString().ToUpper() : string.Empty
                };

                if (node1 != null)
                {
                    change.Value = value1 != null ? value1.ToString().ToUpper() : string.Empty;
                }

                if (pInfo1.Name == "Active")
                {
                    change.Value = GetNameActive(change.Value);
                    change.ValueNew = GetNameActive(change.ValueNew);
                }
                else if (pInfo1.Name == "NodeParentId")
                {
                    if (node1 != null)
                    {
                        change.Value = $"{node1.NodeParentCode} - {node1.NodeParentName}";
                    }
                    else {
                        change.Value = "";
                            }
                    change.ValueNew = $"{node2.NodeParentCode} - {node2.NodeParentName}";
                }

                GetFieldName(pInfo1, change);
                change.FieldName = GetNameData(change.Field);
                nodeChanges.Changes.Add(change);
            }
        }

        /// <summary>
        /// Gets the name of the field.
        /// </summary>
        /// <param name="pInfo1">The p info1.</param>
        /// <param name="change">The change.</param>
        private void GetFieldName(PropertyInfo pInfo1, ChangesWithoutSavingDTO change)
        {
            switch (pInfo1.Name)
            {
                case "AttentionModeName":
                    change.Field = "AttentionModeId";
                    break;
                case "EmployeeId":
                    change.Field = "EmployeeId";
                    break;
                case "Name":
                    change.Field = "Name";
                    break;
                case "Active":
                    change.Field = "Active";
                    break;
                case "Code":
                    change.Field = "Code";
                    break;
                case "NodeParentId":
                    change.Field = "NodeParentId";
                    break;
                default:
                    change.Field = $"{pInfo1.Name}Id";
                    break;
            }
        }

        /// <summary>
        /// Gets the name data.
        /// </summary>
        /// <param name="field">The field.</param>
        /// <returns></returns>
        private string GetNameData(string field)
        {
            if (!string.IsNullOrEmpty(field))
            {
                switch (field)
                {
                    case "AttentionModeId":
                        return "Modo de atención";
                    case "RoleId":
                        return "Rol";
                    case "EmployeeId":
                        return "Persona";
                    case "SaleChannelId":
                        return "Canal de venta";
                    case "Name":
                        return "Nombre";
                    case "Active":
                        return "Estado";
                    case "Code":
                        return "Código";
                    case "NodeParentId":
                        return "Depende de";
                    default:
                        break;
                }
            }
            return string.Empty;
        }

        /// <summary>
        /// Gets the name active.
        /// </summary>
        /// <param name="field">The field.</param>
        /// <returns></returns>
        private string GetNameActive(string field)
        {
            if (!string.IsNullOrEmpty(field))
            {
                switch (field)
                {
                    case "TRUE":
                        return "ACTIVO";
                    case "FALSE":
                        return "INACTIVO";
                    default:
                        return "-";
                }
            }
            return string.Empty;
        }
    }
}