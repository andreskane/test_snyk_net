using System;
using System.Diagnostics.CodeAnalysis;

namespace ABI.API.Structure.ACL.Truck.Infrastructure.Rest
{
    [ExcludeFromCodeCoverage]
    class DBUHResourceRestClient : BaseRestClient
    {
        public readonly string _baseUrl;
        public DBUHResourceRestClient(string baseUrl) : base(0, 0, baseUrl)
        {
            _baseUrl = baseUrl ?? throw new ArgumentNullException(nameof(baseUrl));
        }
        protected override void Init(int siteId, int UserId)
        {
            _uri_KeyName = "Uri_DBUHResource";
        }

        protected override void SetHeaders()
        {
            base.SetHeaders();
            AddHeader("site_id", GetSiteId());
        }
    }
}