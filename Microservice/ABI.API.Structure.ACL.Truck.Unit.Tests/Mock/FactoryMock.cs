using JsonNet.ContractResolvers;
using Newtonsoft.Json;
using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace ABI.API.Structure.Unit.Tests.Mock
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

        public static T GetMock<T>(string file)
        {

            string strFile = File.ReadAllText(file);
            return Serializer.Deserialize<T>(strFile);
        }

        public static T GetMockJson<T>(string file)
        {
            string strFile = File.ReadAllText(file);
            return JsonConvert.DeserializeObject<T>(
                strFile,
                new JsonSerializerSettings
                {
                    ContractResolver = new PrivateSetterContractResolver()
                }
            );
        }
        public static T GetMockJson<T>(string file, bool autoHandleClass)
        {
            string strFile = File.ReadAllText(file);
            return JsonConvert.DeserializeObject<T>(
                strFile,
                new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.Auto,
                    ContractResolver = new PrivateSetterContractResolver()
                }
            );
        }

        public static string GetMockPath(string jsonFileName)
        {
            return Path.Combine("MockFile", jsonFileName);
        }
    }
}
