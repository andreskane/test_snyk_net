using ABI.Framework.MS.Domain.Exceptions;

namespace ABI.API.Structure.Application.Exceptions
{
    public class ConfirmException : DomainException
    {
        private const string _msg = "¿Desea continuar con la operación?";
        public string mensaje { get; set; }
        public ConfirmException()
        {
            mensaje = _msg;
        }
        public ConfirmException(string mensajePersonalizado)
        {
            mensaje = mensajePersonalizado + _msg;
        }
    }
}
