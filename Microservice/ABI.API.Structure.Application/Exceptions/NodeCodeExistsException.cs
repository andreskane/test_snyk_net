namespace ABI.API.Structure.Application.Exceptions
{
    public class NodeCodeExistsException : PropertyException
    {
        private const string _msg = "El código ya existe en el nivel";

        public NodeCodeExistsException() : base(_msg)
        {
            _property = "Code";
        }
    }
}
