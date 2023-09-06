namespace ABI.API.Structure.Application.Exceptions
{
    public class NodeEmployeeResponsableTerritoriesException : PropertyException
    {
        public NodeEmployeeResponsableTerritoriesException() : base("La persona asignada debe ser responsable en todos sus territorios")
        {
            _property = "AttentionModeId";
        }
    }
}
