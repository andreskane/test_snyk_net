namespace ABI.API.Structure.Application.Exceptions
{
    public class NodeCodeNotNumericException : PropertyException
    {
        private const string _msg = "El código no es numérico";

        public NodeCodeNotNumericException() : base(_msg)
        {
            _property = "Code";
        }
    }
}
