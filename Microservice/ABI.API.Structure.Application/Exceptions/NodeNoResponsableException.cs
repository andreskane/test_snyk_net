namespace ABI.API.Structure.Application.Exceptions
{
    public class NodeNoResponsableException : PropertyException
    {
        public NodeNoResponsableException() : base("Modo de atención no debe ser de tipo responsable")
        {
            _property = "AttentionModeId";
        }
    }
}
