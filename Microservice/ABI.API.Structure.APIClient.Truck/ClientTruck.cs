using ABI.API.Structure.APIClient.Truck.Exceptions;
using ABI.Framework.MS.Net.RestClient;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Serialization.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Net;
using System.Threading.Tasks;

namespace ABI.API.Structure.APIClient.Truck
{
    [ExcludeFromCodeCoverage]
    public class ClientTruck
    {
        private readonly string _URLService;
        private readonly Dictionary<string, object> _Headers;
        private int TimeoutApi { get; set; }


        public ClientTruck(string URLService, Dictionary<string, object> Headers, int timeoutApi )
        {
            this._URLService = URLService;
            this._Headers = Headers;
            this.TimeoutApi = timeoutApi;
        }
            public T Get<T>(string method, Dictionary<string, object> parameters = null, bool urlSegmentType = true)
        {
            RestClient restClient = new RestClient(this._URLService);
            restClient.Timeout = TimeoutApi;
            RestRequest request = new RestRequest(method, Method.GET);
            if (parameters != null)
                this.ADDRestParameters(ref request, parameters, urlSegmentType);
            this.ADDRestHeaders(ref request);
            RestRequest restRequest = request;
            IRestResponse restResponse = restClient.Execute(restRequest);
            T obj;
            switch (restResponse.StatusCode)
            {
                case HttpStatusCode.OK:
                    obj = JsonConvert.DeserializeObject<T>(restResponse.Content);
                    break;
                case HttpStatusCode.BadRequest:
                    throw new BadRequestException(restResponse.ErrorMessage, restResponse.ErrorException);
                case HttpStatusCode.Unauthorized:
                    throw new UnauthorizedException();
                case HttpStatusCode.Forbidden:
                    throw new ForbiddenException();
                case HttpStatusCode.NotFound:
                    obj = default(T);
                    break;
                case HttpStatusCode.InternalServerError:
                    if (restResponse.ErrorMessage != null)
                    {
                        throw new GenericException("InternalServerError: - " + restResponse.ErrorMessage);
                    }
                    else
                    {
                        throw new GenericException("InternalServerError: - Not detected");
                    }

                default:
                    throw new GenericException(restResponse.StatusDescription);
            }
            return obj;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Minor Code Smell", "S4136:Method overloads should be grouped together", Justification = "<Pending>")]
        public T Post<T>(string method, object body, Dictionary<string, object> parameters = null, bool urlSegmentType = true)
        {
            RestSharp.RestClient restClient = new RestSharp.RestClient(this._URLService);
            restClient.Timeout = TimeoutApi;
            RestRequest request = new RestRequest(method, Method.POST);
            if (parameters != null)
                this.ADDRestParameters(ref request, parameters, urlSegmentType);
            if (body != null)
                this.ADDBody(ref request, body);
            this.ADDRestHeaders(ref request);
            RestRequest restRequest = request;
            IRestResponse restResponse = restClient.Execute(restRequest);
            switch (restResponse.StatusCode)
            {
                case HttpStatusCode.OK:
                case HttpStatusCode.Created:
                    return JsonConvert.DeserializeObject<T>(restResponse.Content);
                case HttpStatusCode.NoContent:
                    throw new NotContentException();
                case HttpStatusCode.BadRequest:
                    throw new BadRequestException(restResponse.ErrorMessage);
                case HttpStatusCode.Unauthorized:
                    throw new UnauthorizedException();
                case HttpStatusCode.Forbidden:
                    throw new ForbiddenException();
                case HttpStatusCode.Conflict:
                    throw new ConflictException();
                case HttpStatusCode.InternalServerError:
                    throw new GenericException("Ha ocurrido un error interno al ejecutar la consulta - " + restResponse.ErrorMessage);
                default:
                    throw new GenericException(restResponse.StatusDescription);
            }
        }
        public T PostXML<T>(string method, object body, Dictionary<string, object> parameters = null, bool urlSegmentType = true)
        {
            RestSharp.RestClient restClient = new RestSharp.RestClient(this._URLService);
            restClient.Timeout = TimeoutApi;
            RestRequest request = new RestRequest(method, Method.POST);
            if (parameters != null)
            { this.ADDRestParameters(ref request, parameters, urlSegmentType); }
            if (body != null)
            {
                request.AddParameter("text/xml", body, ParameterType.RequestBody);
            }
            this.ADDRestHeaders(ref request);
            RestRequest restRequest = request;
            IRestResponse restResponse = restClient.Execute(restRequest);
            switch (restResponse.StatusCode)
            {
                case HttpStatusCode.Created:
                    return JsonConvert.DeserializeObject<T>(restResponse.Content);
                case HttpStatusCode.NoContent:
                    throw new NotContentException();
                case HttpStatusCode.BadRequest:
                    throw new BadRequestException(restResponse.ErrorMessage);
                case HttpStatusCode.Unauthorized:
                    throw new UnauthorizedException();
                case HttpStatusCode.Forbidden:
                    throw new ForbiddenException();
                case HttpStatusCode.Conflict:
                    throw new ConflictException();
                case HttpStatusCode.InternalServerError:
                    throw new GenericException("Ha ocurrido un error interno al ejecutar la consulta - " + restResponse.ErrorMessage);
                default:
                    throw new GenericException(restResponse.StatusDescription);
            }
        }

        public bool Post(string method, object body, ref int? id, Dictionary<string, object> parameters = null, bool urlSegmentType = true)
        {
            RestSharp.RestClient restClient = new RestSharp.RestClient(this._URLService);
            restClient.Timeout = TimeoutApi;
            RestRequest request = new RestRequest(method, Method.POST);

            request.RequestFormat = DataFormat.Json;
            if (parameters != null) { this.ADDRestParameters(ref request, parameters, urlSegmentType); }

            if (body != null) { this.ADDBody(ref request, body); }

            this.ADDRestHeaders(ref request);
            RestRequest restRequest = request;
            IRestResponse response = restClient.Execute(restRequest);
            switch (response.StatusCode)
            {
                case HttpStatusCode.Created:
                    bool flag = true;
                    id = new int?(new JsonDeserializer().Deserialize<int>(response));
                    return flag;
                case HttpStatusCode.NoContent:
                    throw new NotContentException();
                case HttpStatusCode.BadRequest:
                    throw new BadRequestException(response.ErrorMessage);
                case HttpStatusCode.Unauthorized:
                    throw new UnauthorizedException();
                case HttpStatusCode.Forbidden:
                    throw new ForbiddenException();
                case HttpStatusCode.Conflict:
                    throw new ConflictException();
                case HttpStatusCode.InternalServerError:
                    throw new GenericException("Ha ocurrido un error interno al ejecutar la consulta - " + response.ErrorMessage);
                default:
                    throw new GenericException(response.StatusDescription);
            }
        }

        public bool Put(string method, object body, Dictionary<string, object> parameters, bool urlSegmentType = true)
        {
            RestSharp.RestClient restClient = new RestSharp.RestClient(this._URLService);
            restClient.Timeout = TimeoutApi;
            RestRequest request = new RestRequest(method, Method.PUT);
            request.RequestFormat = DataFormat.Json;
            if (parameters != null)
                this.ADDRestParameters(ref request, parameters, urlSegmentType);
            if (body != null)
                this.ADDBody(ref request, body);
            this.ADDRestHeaders(ref request);
            RestRequest restRequest = request;
            IRestResponse restResponse = restClient.Execute(restRequest);
            switch (restResponse.StatusCode)
            {
                case HttpStatusCode.OK:
                    return true;
                case HttpStatusCode.NoContent:
                    throw new NotContentException();
                case HttpStatusCode.BadRequest:
                    throw new BadRequestException(restResponse.ErrorMessage);
                case HttpStatusCode.Unauthorized:
                    throw new UnauthorizedException();
                case HttpStatusCode.Forbidden:
                    throw new ForbiddenException();
                case HttpStatusCode.NotFound:
                    return false;
                case HttpStatusCode.InternalServerError:
                    throw new GenericException("Ha ocurrido un error interno al ejecutar la consulta - " + restResponse.ErrorMessage);
                default:
                    throw new GenericException(restResponse.StatusDescription);
            }
        }

        public bool Delete(string method, Dictionary<string, object> parameters, bool urlSegmentType = true)
        {
            RestSharp.RestClient client = new RestSharp.RestClient(this._URLService);
            client.Timeout = TimeoutApi;
            RestRequest request = new RestRequest(method, Method.DELETE);
            if (parameters != null)
            {
                this.ADDRestParameters(ref request, parameters, urlSegmentType);
            }

            this.ADDRestHeaders(ref request);
            RestRequest restRequest = request;
            IRestResponse restResponse = client.Delete(restRequest);
            switch (restResponse.StatusCode)
            {
                case HttpStatusCode.OK:
                    return true;
                case HttpStatusCode.BadRequest:
                    throw new BadRequestException(restResponse.ErrorMessage);
                case HttpStatusCode.Unauthorized:
                    throw new UnauthorizedException();
                case HttpStatusCode.Forbidden:
                    throw new ForbiddenException();
                case HttpStatusCode.InternalServerError:
                    throw new GenericException("Ha ocurrido un error interno al ejecutar la consulta - " + restResponse.ErrorMessage);
                default:
                    throw new GenericException(restResponse.StatusDescription);
            }
        }

        private void ADDRestParameters(ref RestRequest request, Dictionary<string, object> parameters, bool urlSegmentType = true)
        {
            foreach (KeyValuePair<string, object> parameter in parameters)
            {
                if (parameter.Value is string)
                {
                    request.AddParameter(parameter.Key, parameter.Value.ToString(), urlSegmentType ? ParameterType.UrlSegment : ParameterType.QueryString);
                }
                else if (parameter.Value is DateTime)
                {
                    request.AddParameter(parameter.Key, ((DateTime)parameter.Value).ToString("s", CultureInfo.InvariantCulture), urlSegmentType ? ParameterType.UrlSegment : ParameterType.QueryString);
                }
                else
                {
                    request.AddParameter(parameter.Key, JsonConvert.SerializeObject(parameter.Value), urlSegmentType ? ParameterType.UrlSegment : ParameterType.QueryString);
                }
            }
        }

        private void ADDBody(ref RestRequest request, object body)
        {
            if (body == null)
            { return; }
            request.AddParameter("Application/Json", JsonConvert.SerializeObject(body), ParameterType.RequestBody);
        }

        private void ADDRestHeaders(ref RestRequest request)
        {
            foreach (KeyValuePair<string, object> header in this._Headers)
            {
                if (header.Value != null)
                {
                    request.AddHeader(header.Key, header.Value.ToString());
                }
            }
        }



        #region Async
        public async Task<T> GetAsync<T>(string method, Dictionary<string, object> parameters = null, bool urlSegmentType = true)
        {
            RestSharp.RestClient restClient = new RestSharp.RestClient(this._URLService);
            restClient.Timeout = TimeoutApi;
            RestRequest request = new RestRequest(method, Method.GET);
            if (parameters != null)
                this.ADDRestParameters(ref request, parameters, urlSegmentType);
            this.ADDRestHeaders(ref request);
            RestRequest restRequest = request;
            IRestResponse restResponse = await restClient.ExecuteAsync(restRequest);
            T obj;
            switch (restResponse.StatusCode)
            {
                case HttpStatusCode.OK:
                    obj = JsonConvert.DeserializeObject<T>(restResponse.Content);
                    break;
                case HttpStatusCode.BadRequest:
                    throw new BadRequestException(restResponse.ErrorMessage, restResponse.ErrorException);
                case HttpStatusCode.Unauthorized:
                    throw new UnauthorizedException();
                case HttpStatusCode.Forbidden:
                    throw new ForbiddenException();
                case HttpStatusCode.NotFound:
                    obj = default(T);
                    break;
                case HttpStatusCode.InternalServerError:
                    if (restResponse.ErrorMessage != null)
                    {
                        throw new GenericException("InternalServerError: - " + restResponse.ErrorMessage);
                    }
                    else
                    {
                        throw new GenericException("InternalServerError: - Not detected");
                    }

                default:
                    throw new GenericException(restResponse.StatusDescription);
            }
            return obj;
        }



        public async Task<T> PostAsync<T>(string method, object body, Dictionary<string, object> parameters = null, bool urlSegmentType = true)
        {
            RestSharp.RestClient restClient = new RestSharp.RestClient(this._URLService);
            restClient.Timeout = TimeoutApi;
            RestRequest request = new RestRequest(method, Method.POST);
            if (parameters != null)
                this.ADDRestParameters(ref request, parameters, urlSegmentType);
            if (body != null)
                this.ADDBody(ref request, body);
            this.ADDRestHeaders(ref request);
            RestRequest restRequest = request;
            IRestResponse restResponse = await restClient.ExecuteAsync(restRequest);
            switch (restResponse.StatusCode)
            {
                case HttpStatusCode.OK:
                    return JsonConvert.DeserializeObject<T>(restResponse.Content);
                case HttpStatusCode.Created:
                    return JsonConvert.DeserializeObject<T>(restResponse.Content);
                case HttpStatusCode.NoContent:
                    throw new NotContentException();
                case HttpStatusCode.BadRequest:
                    throw new BadRequestException(restResponse.ErrorMessage);
                case HttpStatusCode.Unauthorized:
                    throw new UnauthorizedException();
                case HttpStatusCode.Forbidden:
                    throw new ForbiddenException();
                case HttpStatusCode.Conflict:
                    throw new ConflictException();
                case HttpStatusCode.InternalServerError:
                    throw new GenericException("Ha ocurrido un error interno al ejecutar la consulta - " + restResponse.ErrorMessage);
                default:
                    throw new GenericException(restResponse.StatusDescription);
            }
        }
        public async Task<T> PostXMLAsync<T>(string method, object body, Dictionary<string, object> parameters = null, bool urlSegmentType = true)
        {
            RestSharp.RestClient restClient = new RestSharp.RestClient(this._URLService);
            restClient.Timeout = TimeoutApi;
            RestRequest request = new RestRequest(method, Method.POST);
            if (parameters != null)
            { this.ADDRestParameters(ref request, parameters, urlSegmentType); }
            if (body != null)
            {
                request.AddParameter("text/xml", body, ParameterType.RequestBody);
            }
            this.ADDRestHeaders(ref request);
            RestRequest restRequest = request;
            IRestResponse restResponse = await restClient.ExecuteAsync(restRequest);
            switch (restResponse.StatusCode)
            {
                case HttpStatusCode.Created:
                    return JsonConvert.DeserializeObject<T>(restResponse.Content);
                case HttpStatusCode.NoContent:
                    throw new NotContentException();
                case HttpStatusCode.BadRequest:
                    throw new BadRequestException(restResponse.ErrorMessage);
                case HttpStatusCode.Unauthorized:
                    throw new UnauthorizedException();
                case HttpStatusCode.Forbidden:
                    throw new ForbiddenException();
                case HttpStatusCode.Conflict:
                    throw new ConflictException();
                case HttpStatusCode.InternalServerError:
                    throw new GenericException("Ha ocurrido un error interno al ejecutar la consulta - " + restResponse.ErrorMessage);
                default:
                    throw new GenericException(restResponse.StatusDescription);
            }
        }



        #endregion

    }
}
