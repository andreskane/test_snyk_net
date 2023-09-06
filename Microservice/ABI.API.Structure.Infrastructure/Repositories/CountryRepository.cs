using ABI.API.Structure.Domain.Entities;
using ABI.API.Structure.Infrastructure.Repositories.Interfaces;
using ABI.Framework.MS.Helpers.Extensions;
using ABI.Framework.MS.Repository.Generics;
using System.Linq;
using System.Threading.Tasks;

namespace ABI.API.Structure.Infrastructure.Repositories
{
    public class CountryRepository : GenericNormalizedRepository<int, Country, StructureContext>, ICountryRepository
    {
        public CountryRepository(StructureContext context) : base(context) { }

        public async Task<Country> GetByName(string name)
        {
            var text = name.ToUpper().NormalizeTextLatam();

            var entitys = await GetAll();

            return (from p in entitys
                    where p.Name.NormalizeTextLatam().ToUpper() == text
                    select p).FirstOrDefault();
        }
    }
}
