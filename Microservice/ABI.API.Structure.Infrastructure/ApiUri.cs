using System.Diagnostics.CodeAnalysis;

namespace ABI.API.Structure.Infrastructure
{
    [ExcludeFromCodeCoverage]
    public static class ApiUri
    {
        public static class DBUHResource
        {
            public static string GetAllDBUHResource(string baseUri) => $"{baseUri}api/Resource/GetAll";
            public static string CheckVacantCategory(string baseUri) => $"{baseUri}api/Resource/CheckVacantCategory";
            public static string AddVacantResource(string baseUri) => $"{baseUri}api/Resource/AddVacantResource";
        }
    }
}
