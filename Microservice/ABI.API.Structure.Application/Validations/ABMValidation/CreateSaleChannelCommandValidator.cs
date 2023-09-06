using ABI.API.Structure.Application.Commands.SaleChannel;
using ABI.API.Structure.Domain.Entities;
using ABI.API.Structure.Infrastructure;

namespace ABI.API.Structure.Application.Validations.ABMValidation
{
    public class CreateSaleChannelCommandValidator : NameCommandValidator<AddCommand, SaleChannel>
    {
        public CreateSaleChannelCommandValidator(StructureContext context) : base(context)
        {
        }
    }
}
