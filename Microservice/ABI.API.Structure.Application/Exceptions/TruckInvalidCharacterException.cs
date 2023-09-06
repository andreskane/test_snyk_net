namespace ABI.API.Structure.Application.Exceptions
{
    public class TruckInvalidCharacterException : PropertyException
    {
        private const string _msg = "Caracter ({0}) no válido para Truck";

        public string Character { get; }


        public TruckInvalidCharacterException(string propertyName, char character) : base(string.Format(_msg, character))
        {
            _property = propertyName;
            Character = character.ToString();
        }
    }
}
