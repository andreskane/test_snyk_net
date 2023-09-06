using System;

namespace ABI.API.Structure.APIClient.Truck.Exceptions
{
    public class ApiTruckException : Exception
    {
        public ApiTruckException()
        { }

        public ApiTruckException(string message)
            : base(message)
        { }

        public ApiTruckException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}