using System.Collections.Generic;

namespace ABI.API.Structure.Application.Services.Interfaces
{
    public interface INotificationMessage<out T> where T : class
    {
        public string ChannelId { get; }
        public int StatusCode { get; }
        public string StatusMessage { get; }
        public string Type { get; }
        public string Username { get; }
        public T Payload { get; }
        public IList<string> Messages { get; }
    }
}
