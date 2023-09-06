using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace ABI.API.Structure.Unit.Tests.Mock
{

    public class Serializer
    {
        public static string Serialize(object obj)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(obj.GetType());
            string result;
            using (StringWriter stringWriter = new StringWriter(new StringBuilder()))
            {
                xmlSerializer.Serialize(stringWriter, obj);
                stringWriter.Flush();
                result = stringWriter.ToString();
            }

            return result;
        }
        public static T Deserialize<T>(string obj)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            T result;
            using (TextReader reader = new StringReader(obj))
            {
                result = (T)serializer.Deserialize(reader);
            }

            return result;
        }
        public static string SerializeJson(object obj)
        {

            return Newtonsoft.Json.JsonConvert.SerializeObject(obj);
        }
        public static T DeserializeJson<T>(string obj)
        {

            return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(obj);
        }
    }

}
