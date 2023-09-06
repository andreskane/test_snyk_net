namespace ABI.API.Structure.APIClient.Truck.Models
{
    public class Business
    {
        public string EmpId { get; private set; }


        public Business(string empId)
        {
            EmpId = empId;
        }
    }
}
