namespace ABI.API.Structure.Application.DTO
{
    public class MostVisitedFilterDto
    {
        public virtual int Value { get; set; }
        public virtual string Name { get; set; }
        public virtual int FilterType { get; set; }

        public MostVisitedFilterDto()
        {

        }
    }
}
