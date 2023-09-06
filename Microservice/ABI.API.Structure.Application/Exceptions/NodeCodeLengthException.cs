namespace ABI.API.Structure.Application.Exceptions
{
    public class NodeCodeLengthException : PropertyException
    {
        private const string _msg = "Código supera la longitud máxima ({0})";

        public int MaxLength { get; }

        public NodeCodeLengthException(int maxLength) : base(string.Format(_msg, maxLength))
        {
            _property = "Code";
            MaxLength = maxLength;
        }
    }
}
