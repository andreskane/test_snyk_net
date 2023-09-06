namespace ABI.API.Structure.Application.Exceptions
{
    public class TerritoryClientActiveException : PropertyException
    {
        public TerritoryClientActiveException() : base("El territorio tiene al menos un cliente activo.")
        {
            _property = "Active";
        }
    }
}
