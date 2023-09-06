using ABI.Framework.MS.Net.RestClient;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace ABI.API.Structure.ACL.Truck.Infrastructure.Rest
{
    [ExcludeFromCodeCoverage]

    public abstract class BaseRestClient
    {
        protected string _uri_KeyName;
        private readonly string _urlService;

        private readonly int _siteId;
        private readonly int _userId;

        private readonly ABI.Framework.MS.Net.RestClient.Client _restClient;
        private Dictionary<string, object> _headers;



        public BaseRestClient(int siteId, int userId, string urlService)
        {
            Init(siteId, userId);
            _siteId = siteId;
            _userId = userId;
            _urlService = urlService;
            SetHeaders();

            _restClient = new ABI.Framework.MS.Net.RestClient.Client(_urlService, _headers);
        }
        protected virtual void SetHeaders()
        {
            _headers = new Dictionary<string, object>();
            AddHeader("id_site", GetSiteId());
            AddHeader("app", "PortalEstructuras");
            AddHeader("user_id", GetUser());
            AddHeader("token", "thisissparta");
        }

        protected void AddHeader(string key, object value)
        {
            _headers.Add(key, value);
        }

        protected int GetSiteId()
        {
            return _siteId;
        }
        protected Int32 GetUser()
        {
            return _userId;
        }
        protected abstract void Init(int siteId, int userId);

        public T Get<T>(string uri, Dictionary<string, object> parameters = null, bool urlSegmentType = true)
        {
            return _restClient.Get<T>(uri, parameters, urlSegmentType);
        }

        public async Task<T> GetAsync<T>(string uri, Dictionary<string, object> parameters = null, bool urlSegmentType = true)
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
                return default(T);
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
                return default(T);
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