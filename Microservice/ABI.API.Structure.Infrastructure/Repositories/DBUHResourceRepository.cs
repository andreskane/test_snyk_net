using ABI.API.Structure.Domain.Entities;
using ABI.API.Structure.Infrastructure.Repositories.Interfaces;
using ABI.API.Structure.Infrastructure.Rest;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ABI.API.Structure.Infrastructure.Repositories
{
    public class DBUHResourceRepository : IDBUHResourceRepository
    {
        public readonly string _baseUrl;

        public DBUHResourceRepository(string baseUrl)
        {
            _baseUrl = baseUrl ?? throw new ArgumentNullException(nameof(baseUrl));
        }

        public async Task<IList<Resource>> GetAllResource()
        {
            var result = new List<Resource>();
            var uri = ApiUri.DBUHResource.GetAllDBUHResource("");
            // var response = await new DBUHResourceRestClient(_baseUrl).GetAsync<JObject>(uri);
            var param = new Dictionary<string, object> { };
            
            var response = await new DBUHResourceRestClient(_baseUrl).GetAsync<JObject>(uri, param, false);

            if (response != null)
            {
                var oToken = response.SelectToken("result"); //TODO: Filtrar las relaciones para mantener solo las que son de tipo = 1 (TRUCK)
                if (oToken.HasValues)
                    result = oToken.ToObject<List<Resource>>();
            }


            return result;
        }

        /// <summary>
        /// Checks the vacant category.
        /// </summary>
        /// <param name="companyId">The company identifier.</param>
        /// <param name="categoryId">The category identifier.</param>
        /// <returns></returns>
        public async Task<bool> CheckVacantCategory(string companyId, string categoryId)
        {
            var result = false;
            var uri = ApiUri.DBUHResource.CheckVacantCategory("");

            foreach (var item in categoryId.Split(","))
            {
                if (item != "C")
                {
                    var param = new Dictionary<string, object>
                     {
                         { "companyId", companyId },
                         { "categoryId", item }
                     };

                    var response = await new DBUHResourceRestClient(_baseUrl).GetAsync<JObject>(uri, param, false);

                    if (response != null)
                    {
                        var oToken = response.SelectToken("result");

                        result = oToken.ToObject<bool>();
                    }

                    return result;

                }
            }

            return result;

        }
    }
}
