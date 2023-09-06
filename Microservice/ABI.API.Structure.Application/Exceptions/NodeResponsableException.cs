namespace ABI.API.Structure.Application.Exceptions
{
    public class NodeResponsableException : PropertyException
    {
        public NodeResponsableException() : base("Modo de atención debe ser de tipo responsable")
        {
            _property = "AttentionModeId";
        }
    }
}
