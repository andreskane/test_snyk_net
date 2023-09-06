using System.Text.Json;

namespace ABI.API.Structure.Infrastructure.Filters
{

    public class ErrorDetails
    {

        public ErrorDetails(string source, string message)
        {
            Source = source;
            Message = message;
        }

        public string Source { get; set; }

        public string Message { get; set; }

        public override string ToString() => JsonSerializer.Serialize(this);

    }
}
