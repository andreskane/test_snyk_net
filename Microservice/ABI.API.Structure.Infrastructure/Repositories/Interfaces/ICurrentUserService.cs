using System;

namespace ABI.API.Structure.Infrastructure.Repositories.Interfaces
{

    //todo:esto no va aca, pero perimero hay que cambiar la relacion entre capas
    public interface ICurrentUserService
    {
        string UserName { get; }
        Guid UserId { get; }
    }
}
