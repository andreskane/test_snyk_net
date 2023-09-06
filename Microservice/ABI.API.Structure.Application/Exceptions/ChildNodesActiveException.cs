namespace ABI.API.Structure.Application.Exceptions
{
    public class ChildNodesActiveException : PropertyException
    {
        public ChildNodesActiveException() : base("Uno o varios niveles que contiene, se encuentra activo.")
        {
            _property = "Active";
        }
    }
}
