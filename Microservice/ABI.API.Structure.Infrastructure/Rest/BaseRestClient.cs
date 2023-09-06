using ABI.Framework.MS.Net.RestClient;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace ABI.API.Structure.Infrastructure.Rest
{
    [ExcludeFromCodeCoverage]
    public abstract class BaseRestClient
    {
        private readonly string _url;

        private readonly Client _restClient;
        private Dictionary<string, object> _headers;


        protected BaseRestClient(string url)
        {
            _url = url;

            SetHeaders();

            _restClient = new Client(_url, _headers);
        }


        protected virtual void SetHeaders()
        {
            _headers = new Dictionary<string, object>();

            AddHeader("Content-Type", "application/json");
            AddHeader("Content-Encoding", "gzip, deflate, br");
        }

        protected void AddHeader(string key, object value)
        {
            _headers.Add(key, value);
        }


        public T Get<T>(string uri, Dictionary<string, object> parameters = null, bool urlSegmentType = false)
        {
            return _restClient.Get<T>(uri, parameters, urlSegmentType);
        }

        public async Task<T> GetAsync<T>(string uri, Dictionary<string, object> parameters = null, bool urlSegmentType = false)
        {
            return await _restClient.GetAsync<T>(uri, parameters, urlSegmentType);
        }

        public T Post<T>(string uri, Dictionary<string, object> parameters = null, object body = null, bool urlSegmentType = true)
        {
            try
            {
                return _restClient.Post<T>(uri, body, parameters, urlSegmentType);
            }
            catch (ConflictException)
            {
                return default;
            }
        }
        public async Task<T> PostAsync<T>(string uri, Dictionary<string, object> parameters = null, object body = null, bool urlSegmentType = true)
        {
            try
            {
                return await _restClient.PostAsync<T>(uri, body, parameters, urlSegmentType);
            }
            catch (ConflictException)
            {
                return default;
            }
        }

        public bool Put(string uri, Dictionary<string, object> parameters = null, object body = null, bool urlSegmentType = true)
        {
            return _restClient.Put(uri, body, parameters, urlSegmentType);
        }

        public bool Delete(string uri, Dictionary<string, object> parameters, bool urlSegmentType = true)
        {
            return _restClient.Delete(uri, parameters, urlSegmentType);
        }
    }

}