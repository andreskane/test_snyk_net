using ABI.API.Structure.ACL.Truck.Application.DTO.Resource;
using ABI.API.Structure.ACL.Truck.Infrastructure.Repositories.Interfaces;
using ABI.API.Structure.ACL.Truck.Infrastructure.Rest;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ABI.API.Structure.ACL.Truck.Infrastructure.Repositories
{
    public class DBUHResourceRepository : IDBUHResourceRepository
    {
        private readonly string _baseUrl;

        public DBUHResourceRepository(string baseUrl)
        {
            _baseUrl = baseUrl ?? throw new ArgumentNullException(nameof(baseUrl));
        }

        public async Task<IList<ResourceDTO>> GetAllResource()
        {
            var result = new List<ResourceDTO>();
            var uri = ApiUri.DBUHResource.GetAllDBUHResource("");
            var response = await new DBUHResourceRestClient(_baseUrl).GetAsync<JObject>(uri);

            if (response != null)
            {
                var oToken = response.SelectToken("result");
                if (oToken.HasValues)
                    result = oToken.ToObject<List<ResourceDTO>>();
            }


            return result;
        }

        /// <summary>
        /// Adds the vacant resource.
        /// </summary>
        /// <param name="companyId">The company identifier.</param>
        /// <param name="categoryId">The category identifier.</param>
        /// <returns></returns>
        public async Task<ResourceDTO> AddVacantResource(int companyId, string categoryId)
        {
            var result = new ResourceDTO();
            var uri = ApiUri.DBUHResource.AddVacantResource("");

            dynamic body = new { CompanyId = companyId, CategoryId = categoryId };

            var response = await new DBUHResourceRestClient(_baseUrl).PostAsync<JObject>(uri, null, body);

            if (response != null)
            {
                var oToken = response.SelectToken("result");

                result = oToken.ToObject<ResourceDTO>();
            }

            return result;
        }
    }
}
