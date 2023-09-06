namespace ABI.API.Structure.ACL.Truck.Application.DTO.Compare
{

    public enum TypeActionNode
    {
        New = 0,
        Draft = 1
    }

    public class NodePortalCompareDTO
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public int LevelId { get; set; }
        public string ParentNodeCode { get; set; }
        public int? ParentNodeLevelId { get; set; }
        public int? EmployeeId { get; set; }
        public int? RoleId { get; set; }
        public int? AttentionModeId { get; set; }
        public bool IsRootNode { get; set; }
        public bool Active { get; set; }
        public TypeActionNode TypeActionNode { get; set; }
        public bool VacantPerson { get; set; }
        public NodePortalCompareDTO()
        {

        }
    }
}
