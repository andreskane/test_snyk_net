using ABI.Framework.MS.Domain.Exceptions;

namespace ABI.API.Structure.Application.Exceptions
{
    public class NodeEditSameDateException : DomainException
    {
        private const string _msg = "No se puede editar el nodo ya que seleccionó la misma fecha de vigencia programada";

        public NodeEditSameDateException() : base(_msg)
        {

        }
    }
}
