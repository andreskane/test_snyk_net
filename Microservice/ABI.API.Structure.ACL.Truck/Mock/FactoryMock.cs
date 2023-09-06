using Newtonsoft.Json;
using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace ABI.API.Structure.ACL.Truck.Mock
{
    [ExcludeFromCodeCoverage]
    public static class FactoryMock
    {
        public static string GetMock(string file)
        {
            if (file != "")
            {
                return File.ReadAllText(file);
            }
            else { return ""; }
        }

        public static T GetMockJson<T>(string file)
        {
            string strFile = File.ReadAllText(file);
            return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(strFile);
        }
        public static T GetMockJson<T>(string file, bool autoHandleClass)
        {
            string strFile = File.ReadAllText(file);
            return JsonConvert.DeserializeObject<T>(strFile,
                new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto });
        }
    }
}
