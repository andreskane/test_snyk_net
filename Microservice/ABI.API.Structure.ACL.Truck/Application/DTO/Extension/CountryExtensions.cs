using ABI.API.Structure.Domain.Entities;

namespace ABI.API.Structure.ACL.Truck.Application.DTO.Extension
{
    public static class CountryExtensions
    {
        public static CountryDTO ToCountryDTO(this Country country)
        {
            var modelDTO = new CountryDTO
            {
                Id = country.Id,
                Name = country.Name,
                Code = country.Code
            };


            return modelDTO;

        }
    }
}
