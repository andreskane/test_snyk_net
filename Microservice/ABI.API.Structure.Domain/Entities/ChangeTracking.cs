using ABI.API.Structure.Domain.AggregatesModel.ChangeTrackingAggregate;
using ABI.API.Structure.Domain.AggregatesModel.StructureAggregate;
using ABI.API.Structure.Domain.Enums;
using ABI.Framework.MS.Entity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace ABI.API.Structure.Domain.Entities
{
    public class ChangeTracking : IEntity<int>
    {
        public int Id { get; set; }
        public int IdStructure { get; set; }
        public DateTimeOffset ValidityFrom { get; set; }
        public string UserJson { get; set; }
        public DateTimeOffset CreateDate { get; set; }
        public int IdObjectType { get; set; }
        public int IdChangeType { get; set; }
        public string ChangedValueJson { get; set; }
        public string NodePathJson { get; set; }
        public int IdOrigen { get; set; }
        public int IdDestino { get; set; }

        [NotMapped]
        public GenericKeyValue ExternalStatus { get; set; }

        public virtual StructureDomain Structure { get; private set; }

        [NotMapped]
        public virtual ChangeTrackingStatus ChangeStatus => _ChangeTrackingStatusListItem.Select(x => new ChangeTrackingStatus { Status=x.Status}).OrderByDescending(o=>o.Id).FirstOrDefault();

        private readonly List<ChangeTrackingStatus> _ChangeTrackingStatusListItem;
        public IReadOnlyCollection<ChangeTrackingStatus> ChangeTrackingStatusListItems => _ChangeTrackingStatusListItem;

        public ChangeTracking()
        {
            _ChangeTrackingStatusListItem = new List<ChangeTrackingStatus>();
            ExternalStatus = new GenericKeyValue { Id="4", Name ="No Exportable" };
        }



        [NotMapped]
        public User User
        {
            get
            {
                return JsonConvert.DeserializeObject<User>(string.IsNullOrEmpty(UserJson) ? "{}" : UserJson);
            }
            set
            {
                UserJson = JsonConvert.SerializeObject(value);
            }
        }

        [NotMapped]
        public ChangeTrackingNode ChangedValueNode
        {
            get
            {
                if (IdObjectType == (int)ChangeTrackingObjectType.Node)
                    return JsonConvert.DeserializeObject<ChangeTrackingNode>(string.IsNullOrEmpty(ChangedValueJson) ? "{}" : ChangedValueJson);
                return null;
            }
            set
            {
                ChangedValueJson = JsonConvert.SerializeObject(value);
            }
        }

        [NotMapped]
        public ChangeTrackingArista ChangedValueArista
        {
            get
            {
                if (IdObjectType == (int)ChangeTrackingObjectType.Arista)
                    return JsonConvert.DeserializeObject<ChangeTrackingArista>(string.IsNullOrEmpty(ChangedValueJson) ? "{}" : ChangedValueJson);
                return null;
            }
            set
            {
                ChangedValueJson = JsonConvert.SerializeObject(value);
            }
        }

        [NotMapped]
        public NodesPath NodePath
        {
            get
            {
                return JsonConvert.DeserializeObject<NodesPath>(string.IsNullOrEmpty(NodePathJson) ? "{}" : NodePathJson);
            }
            set
            {
                NodePathJson = JsonConvert.SerializeObject(value);
            }
        }

        public string KindOfChange()
        {
            switch (IdChangeType)
            {
                case 4:
                    return "Estructura";
                case 5:
                    return "Rol";
                case 6:
                    return "Persona";
                default:
                    throw new NotImplementedException(IdChangeType.ToString());
            }
        }
      
        public int GetPortalStatus(bool cancelled)
        {
            if (cancelled)
            {
                return 3;
            }
            else
                if (ValidityFrom.ToUniversalTime().Date > DateTimeOffset.UtcNow.Date)
            {
                return 1;
            }
            else
                return 2;
        }

        public Type ObjectType { get; set; }
    }
}
