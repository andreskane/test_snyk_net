using ABI.Framework.MS.Helpers.Response;
using System.Threading.Tasks;

namespace ABI.API.Structure.Application.Services.Interfaces
{
    public interface INotificationStatusService
    {
        Task Notify(string ChannelId, GenericResponse response);
    }
}
