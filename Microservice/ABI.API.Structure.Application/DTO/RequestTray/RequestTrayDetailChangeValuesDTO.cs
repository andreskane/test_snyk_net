namespace ABI.API.Structure.Application.DTO
{
    public class RequestTrayDetailChangeValuesDTO
    {
        public int Id { get; set; }
        public string Old { get; set; }
        public string New { get; set; }
        public string Field { get; set; }
        public string FieldName { get; set; }
    }
}
