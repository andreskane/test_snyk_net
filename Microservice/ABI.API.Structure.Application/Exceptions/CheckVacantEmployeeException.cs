namespace ABI.API.Structure.Application.Exceptions
{
    public class CheckVacantEmployeeException : PropertyException
    {
        public CheckVacantEmployeeException() : base("El empleado no puede ser vacante.")
        {
            _property = "EmployeeId";
        }
    }
}
