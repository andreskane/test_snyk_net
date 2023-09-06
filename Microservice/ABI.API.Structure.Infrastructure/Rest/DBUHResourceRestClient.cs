using System;
using System.Diagnostics.CodeAnalysis;

namespace ABI.API.Structure.Infrastructure.Rest
{
    [ExcludeFromCodeCoverage]
    public class DBUHResourceRestClient : BaseRestClient
    {
        public readonly string _baseUrl;
        public DBUHResourceRestClient(string baseUrl) : base(baseUrl)
        {
            _baseUrl = baseUrl ?? throw new ArgumentNullException(nameof(baseUrl));
        }

    }
}