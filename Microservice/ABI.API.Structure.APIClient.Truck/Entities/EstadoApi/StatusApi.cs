using System.Runtime.Serialization;

namespace ABI.API.Structure.APIClient.Truck.Entities.EstadoApi
{
    [DataContract]
    public class StatusApi
    {

        public string Static { get; set; }
        public string Date { get; set; }

        public void GetStatus(string datoOut)
        {
            var output = datoOut.Split('[', ']');


            if (output.Length == 3)
            {
                Static = output[1].Replace("Dato de Extra:", "").Replace("'", "").Trim();
                var time = output[2].Replace("Tiempo:", "").Replace("--", "").Trim();

                if (!string.IsNullOrEmpty(time))
                {
                    var datetime = time.Split(" ");

                    if (datetime.Length == 3)
                    {
                        var date = datetime[0].Split("/");
                        var Time = datetime[1].Split(":");
                        var T = datetime[2];

                        var newDate = string.Format("{0}/{1}/{2} {3}:{4}:{5} {6}", date[1], date[0], date[2], Time[0], Time[1], Time[2], T);

                        Date = newDate;

                    }
                }
            }


        }

    }
}
