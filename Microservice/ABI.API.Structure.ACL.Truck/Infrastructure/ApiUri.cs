using System.Diagnostics.CodeAnalysis;

namespace ABI.API.Structure.ACL.Truck
{
    [ExcludeFromCodeCoverage]
    public static class ApiUri
    {
        [ExcludeFromCodeCoverage]
        public static class DBUHResource
        {
            public static string GetAllDBUHResource(string baseUri) => $"{baseUri}api/Resource/GetAll";
            public static string AddVacantResource(string baseUri) => $"{baseUri}api/Resource/AddVacantResource";
        }
    }
}
