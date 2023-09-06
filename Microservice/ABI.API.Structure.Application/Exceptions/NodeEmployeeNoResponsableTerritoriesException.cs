namespace ABI.API.Structure.Application.Exceptions
{
    public class NodeEmployeeNoResponsableTerritoriesException : PropertyException
    {
        public NodeEmployeeNoResponsableTerritoriesException() : base("La persona asignada debe ser no responsable en todos sus territorios")
        {
            _property = "AttentionModeId";
        }
    }
}
