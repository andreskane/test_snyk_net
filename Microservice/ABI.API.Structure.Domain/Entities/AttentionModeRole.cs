using ABI.Framework.MS.Entity;

namespace ABI.API.Structure.Domain.Entities
{
    public class AttentionModeRole : BaseEntity<int>
    {
        public int? AttentionModeId { get; set; }
        public int? RoleId { get; set; }
        public bool EsResponsable { get; set; }
        public virtual AttentionMode AttentionMode { get; set; }
        public virtual Role Role { get; set; }



        public AttentionModeRole()
        {

        }

    }
}
