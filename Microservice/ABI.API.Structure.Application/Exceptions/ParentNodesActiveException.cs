namespace ABI.API.Structure.Application.Exceptions
{
    public class ParentNodesActiveException : PropertyException
    {
        public ParentNodesActiveException() : base("El nivel Superior, se encuentra inactivo.")
        {
            _property = "Active";
        }
    }
}
