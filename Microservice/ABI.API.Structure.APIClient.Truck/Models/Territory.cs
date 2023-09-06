namespace ABI.API.Structure.APIClient.Truck.Models
{
    public class Territory
    {
        public string EmpId { get; private set; }
        public string GerenciaID { get; private set; }
        public string TrrId { get; private set; }


        public Territory(string empId, string gerenciaID, string trrId)
        {
            EmpId = empId;
            GerenciaID = gerenciaID;
            TrrId = trrId;
        }
    }
}
