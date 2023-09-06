using ABI.API.Structure.ACL.Truck.Application.Service.Interfaces;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ABI.API.Structure.ACL.Truck.Application.Queries.CategoryVendor
{
    public class GetByVendorTruckIdQuery : IRequest<Service.Models.CategoryVendor>
    {
        public int VendorTruckId { get; set; }

    }
    public class GetByVendorTruckIdHandler : IRequestHandler<GetByVendorTruckIdQuery, Service.Models.CategoryVendor>
    {
        private readonly ITypeVendorTruckService _service;
        private readonly ICategoryVendorTruckService _categoryVendorService;

        public GetByVendorTruckIdHandler(ITypeVendorTruckService typeVendorService, ICategoryVendorTruckService categoryVendorService)
        {
            _service = typeVendorService ?? throw new ArgumentNullException(nameof(typeVendorService));
            _categoryVendorService = categoryVendorService ?? throw new ArgumentNullException(nameof(categoryVendorService));
        }

        public async Task<Service.Models.CategoryVendor> Handle(GetByVendorTruckIdQuery request, CancellationToken cancellationToken)
        {
            var items = await _service.GetTypeVendorTruckById(request.VendorTruckId);

            if (items.Count > 0)
            {
                var itemsCategory = await _categoryVendorService.GetCategoryVendorTruckById(items.FirstOrDefault().Code);
                if (itemsCategory.Count>0)
                    return await Task.Run(() => itemsCategory.FirstOrDefault());
            }
            return await Task.Run(() => new Service.Models.CategoryVendor());
        }
    }
}
