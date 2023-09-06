using ABI.API.Structure.APIClient.Truck.Entities.CategoriaVendedor;
using ABI.API.Structure.APIClient.Truck.Entities.CentroDeDespacho;
using ABI.API.Structure.APIClient.Truck.Entities.EstadoApi;
using ABI.API.Structure.APIClient.Truck.Entities.EstructuraRutas;
using ABI.API.Structure.APIClient.Truck.Entities.EstructuraVentas;
using ABI.API.Structure.APIClient.Truck.Entities.EstructuraVersiones;
using ABI.API.Structure.APIClient.Truck.Entities.Responsables;
using ABI.API.Structure.APIClient.Truck.Entities.TipoCategoriaVendedor;
using ABI.API.Structure.APIClient.Truck.Entities.TipoVendedores;
using ABI.API.Structure.APIClient.Truck.Entities.TruckImpact;
using ABI.API.Structure.APIClient.Truck.Exceptions;
using ABI.API.Structure.APIClient.Truck.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ABI.API.Structure.APIClient.Truck
{
    public class ApiTruck : IApiTruck
    {

        private readonly Dictionary<string, object> _apiHeaders;

        private readonly IApiTruckUrls _apiUrls;
        private int _timeoutApi { get; set; }

        public ApiTruck(IApiTruckUrls apiUrls)
        {
            _apiUrls = apiUrls;

            _timeoutApi = apiUrls.ApiTrucktimeoutApi();

            _apiHeaders = new Dictionary<string, object>
                {
                    { "Content-Type", "application/json" },
                    { "Content-Encoding", "gzip, deflate, br" }
                };

        }

        #region Methods of obtaining truck data


        /// <summary>
        /// Gets the structure truck.
        /// </summary>
        /// <param name="empID">The emp identifier.</param>
        /// <returns></returns>
        /// <exception cref="GenericException">Error al Obtener Datos</exception>
        public async Task<TruckStructure> GetStructureTruck(string empID)
        {
            try
            {
                var url = _apiUrls.ApiTruckBaseUrl();
                var headers = new Dictionary<string, object>
                {
                    { "Content-Type", "application/json" },
                    { "Content-Encoding", "gzip, deflate, br" }
                };

                var clientTruck = new ClientTruck(url, headers, _timeoutApi);

                var empresa = new Business(empID);

                var dataTruck = await clientTruck.PostAsync<TruckStructure>(_apiUrls.EstructuraVentas, empresa, null);

                dataTruck.ConsultationDate = DateTimeOffset.UtcNow.ToOffset(TimeSpan.FromHours(-3)); //HACER: Ojo multipais

                return dataTruck;
            }
            catch (System.Exception ex)
            {
                throw new GenericException("Error al Obtener Datos de la Api Truck", ex);
            }

        }

        /// <summary>
        /// Gets the territory truck.
        /// </summary>
        /// <param name="empID">The emp identifier.</param>
        /// <param name="trrId">The TRR identifier.</param>
        /// <returns></returns>
        /// <exception cref="GenericException">Error al Obtener Datos</exception>
        public async Task<TruckTerritory> GetTerritoryTruck(string empID, string trrId)
        {

            try
            {
                var url = _apiUrls.ApiTruckBaseUrl();
                var headers = new Dictionary<string, object>
                {
                    { "Content-Type", "application/json" },
                    { "Content-Encoding", "gzip, deflate, br" }
                };

                var clientTruck = new ClientTruck(url, headers, _timeoutApi);

                var territory = new Models.Territory(empID, "", trrId);

                var dataClient = await clientTruck.PostAsync<TruckTerritory>(_apiUrls.EstructuraRutas, territory, null, false);

                dataClient.ConsultationDate = DateTimeOffset.UtcNow;

                return dataClient;

            }
            catch (Exception ex)
            {
                throw new GenericException("Error al Obtener Datos", ex);
            }

        }

        /// <summary>
        /// Gets the territory truck by management.
        /// </summary>
        /// <param name="empID">The emp identifier.</param>
        /// <param name="management">The management.</param>
        /// <returns></returns>
        /// <exception cref="GenericException">Error al Obtener Datos</exception>
        public async Task<TruckTerritory> GetTerritoryTruckByManagement(string empID, string management)
        {

            try
            {
                var url = _apiUrls.ApiTruckBaseUrl();
                var headers = new Dictionary<string, object>
                {
                    { "Content-Type", "application/json" },
                    { "Content-Encoding", "gzip, deflate, br" }
                };

                var clientTruck = new ClientTruck(url, headers, _timeoutApi);

                var territory = new Models.Territory(empID, management, "00000");

                var dataClient = await clientTruck.PostAsync<TruckTerritory>(_apiUrls.EstructuraRutas, territory, null, false);

                dataClient.ConsultationDate = DateTimeOffset.UtcNow;

                return dataClient;

            }
            catch (Exception ex)
            {
                throw new GenericException("Error al Obtener Datos", ex);
            }

        }

        /// <summary>
        /// Gets the seller category.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="GenericException">Error al Obtener Datos</exception>
        public async Task<TruckSellerCategory> GetAllSellerCategory()
        {
            try
            {
                var url = _apiUrls.ApiTruckBaseUrl();
                var headers = new Dictionary<string, object>
                {
                    { "Content-Type", "application/json" },
                    { "Content-Encoding", "gzip, deflate, br" }
                };

                var clientTruck = new ClientTruck(url, headers, _timeoutApi);

                var data = await clientTruck.PostAsync<TruckSellerCategory>(_apiUrls.CategoriaVendedores, null, null, false);

                return data;
            }
            catch (Exception ex)
            {
                throw new GenericException("Error al Obtener Datos", ex);
            }

        }

        /// <summary>
        /// Gets all office center.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="GenericException">Error al Obtener Datos</exception>
        public async Task<TruckOfficeCenter> GetAllOfficeCenter()
        {
            try
            {
                var url = _apiUrls.ApiTruckBaseUrl();
                var headers = new Dictionary<string, object>
                {
                    { "Content-Type", "application/json" },
                    { "Content-Encoding", "gzip, deflate, br" }
                };

                var clientTruck = new ClientTruck(url, headers, _timeoutApi);

                var data = await clientTruck.PostAsync<TruckOfficeCenter>(_apiUrls.CentroDeDespacho, null, null, false);

                return data;
            }
            catch (Exception ex)
            {
                throw new GenericException("Error al Obtener Datos", ex);
            }

        }

        /// <summary>
        /// Gets all truck responsible.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="GenericException">Error al Obtener Datos</exception>
        public async Task<TruckResponsible> GetAllResponsible()
        {
            try
            {
                var url = _apiUrls.ApiTruckBaseUrl();
                var headers = new Dictionary<string, object>
                {
                    { "Content-Type", "application/json" },
                    { "Content-Encoding", "gzip, deflate, br" }
                };

                var clientTruck = new ClientTruck(url, headers, _timeoutApi);

                var data = await clientTruck.PostAsync<TruckResponsible>(_apiUrls.Responsable, null, null, false);

                return data;
            }
            catch (Exception ex)
            {
                throw new GenericException("Error al Obtener Datos", ex);
            }

        }

        /// <summary>
        /// Gets all type category seller.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="GenericException">Error al Obtener Datos</exception>
        public async Task<TruckTypeCategorySeller> GetAllTypeCategorySeller()
        {
            try
            {
                var url = _apiUrls.ApiTruckBaseUrl();
                var headers = new Dictionary<string, object>
                {
                    { "Content-Type", "application/json" },
                    { "Content-Encoding", "gzip, deflate, br" }
                };

                var clientTruck = new ClientTruck(url, headers, _timeoutApi);

                var data = await clientTruck.PostAsync<TruckTypeCategorySeller>(_apiUrls.TiposCategoriaVendedores, null, null, false);

                return data;
            }
            catch (Exception ex)
            {
                throw new GenericException("Error al Obtener Datos", ex);
            }

        }

        /// <summary>
        /// Gets all type seller.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="GenericException">Error al Obtener Datos</exception>
        public async Task<TruckTypeSeller> GetAllTypeSeller()
        {
            try
            {
                var url = _apiUrls.ApiTruckBaseUrl();
                var headers = new Dictionary<string, object>
                {
                    { "Content-Type", "application/json" },
                    { "Content-Encoding", "gzip, deflate, br" }
                };

                var clientTruck = new ClientTruck(url, headers, _timeoutApi);

                var data = await clientTruck.PostAsync<TruckTypeSeller>(_apiUrls.TipoVendedores, null, null, false);


                return data;
            }
            catch (Exception ex)
            {
                throw new GenericException("Error al Obtener Datos", ex);
            }

        }

        /// <summary>
        /// Statuses the API.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="GenericException">Error al Obtener Datos</exception>
        public async Task<StatusApi> GetStatusApi()
        {

            try
            {
                var url = _apiUrls.ApiTruckBaseUrl();
                var headers = new Dictionary<string, object>
                {
                    { "Content-Type", "application/json" },
                    { "Content-Encoding", "gzip, deflate, br" }
                };

                var clientTruck = new ClientTruck(url, headers, _timeoutApi);

                var test = new WkgrestTester();

                var data = new PedsInRst
                {
                    DatoIn = "000",
                    DatoExtra = "OK"
                };

                test.DatoIn = data;

                var dataClient = await clientTruck.PostAsync<TruckStatus>(_apiUrls.Wkgresttester, test, null, false);
                var status = new StatusApi();

                if (dataClient != null)
                {
                    status.GetStatus(dataClient.DatoOut);
                }

                return status;

            }
            catch (Exception ex)
            {
                throw new GenericException("Error al Obtener Datos", ex);
            }

        }

        #endregion

        #region Methods sent to truck

        /// <summary>
        /// Sets the opecpini.
        /// </summary>
        /// <param name="opeIni">The ope ini.</param>
        /// <returns></returns>
        /// <exception cref="GenericException">Error al Obtener Datos de la Api Truck</exception>
        public async Task<OpecpiniOut> SetOpecpini(OpecpiniInput opeIni)
        {
            try
            {
                var url = _apiUrls.ApiTruckBaseUrl();

                var clientTruck = new ClientTruck(url, _apiHeaders, _timeoutApi);

                var dataTruck = await clientTruck.PostAsync<OpecpiniOut>(_apiUrls.Opecpini, opeIni, null);


                return dataTruck;
            }
            catch (System.Exception ex)
            {
                throw new GenericException("Error al Obtener Datos de la Api Truck", ex);
            }

        }

        /// <summary>
        /// Gets the structure version truck.
        /// </summary>
        /// <param name="ini">The ini.</param>
        /// <returns></returns>
        /// <exception cref="GenericException">Error al Obtener Datos de la Api Truck</exception>
        public async Task<EstructuraVersionOutput> GetStructureVersionStatusTruck(EstructuraVersionInput ini)
        {
            try
            {
                var url = _apiUrls.ApiTruckBaseUrl();

                var clientTruck = new ClientTruck(url, _apiHeaders, _timeoutApi);

                var dataTruck = await clientTruck.PostAsync<EstructuraVersionOutput>(_apiUrls.EstructuraVersion, ini, null);


                return dataTruck;
            }
            catch (System.Exception ex)
            {
                throw new GenericException("Error al Obtener Datos de la Api Truck", ex);
            }
        }
        /// <summary>
        /// Ptecdires the specified ptecdire input.
        /// </summary>
        /// <param name="ptecdireInput">The ptecdire input.</param>
        /// <exception cref="GenericException">Error al enviar los Datos de la Api Truck - Ptecdire</exception>
        public async Task Ptecdire(PtecdireInput ptecdireInput)
        {
            try
            {
                var url = _apiUrls.ApiTruckBaseUrl();

                var clientTruck = new ClientTruck(url, _apiHeaders, _timeoutApi);

                var dataTruck = await clientTruck.PostAsync<object>(_apiUrls.Ptecdire, ptecdireInput, null);

            }
            catch (System.Exception ex)
            {
                throw new GenericException("Error al enviar los Datos de la Api Truck - Ptecdire", ex);
            }
        }

        /// <summary>
        /// Ptecareas the specified ptecarea input.
        /// </summary>
        /// <param name="PtecareaInput">The ptecarea input.</param>
        /// <exception cref="GenericException">Error al enviar los Datos de la Api Truck - Ptecarea</exception>
        public async Task Ptecarea(PtecareaInput PtecareaInput)
        {
            try
            {
                var url = _apiUrls.ApiTruckBaseUrl();

                var clientTruck = new ClientTruck(url, _apiHeaders, _timeoutApi);

                var dataTruck = await clientTruck.PostAsync<object>(_apiUrls.Ptecarea, PtecareaInput, null);

            }
            catch (System.Exception ex)
            {
                throw new GenericException("Error al enviar los Datos de la Api Truck - Ptecarea", ex);
            }
        }

        /// <summary>
        /// Ptecgeres the specified ptecgere input.
        /// </summary>
        /// <param name="PtecgereInput">The ptecgere input.</param>
        /// <exception cref="GenericException">Error al enviar los Datos de la Api Truck - Ptecgere</exception>
        public async Task Ptecgere(PtecgereInput PtecgereInput)
        {
            try
            {
                var url = _apiUrls.ApiTruckBaseUrl();

                var clientTruck = new ClientTruck(url, _apiHeaders, _timeoutApi);

                var dataTruck = await clientTruck.PostAsync<object>(_apiUrls.Ptecgere, PtecgereInput, null);

            }
            catch (System.Exception ex)
            {
                throw new GenericException("Error al enviar los Datos de la Api Truck - Ptecgere", ex);
            }
        }

        public async Task Ptecregi(PtecregiInput PtecregiInput)
        {
            try
            {
                var url = _apiUrls.ApiTruckBaseUrl();

                var clientTruck = new ClientTruck(url, _apiHeaders, _timeoutApi);

                var dataTruck = await clientTruck.PostAsync<object>(_apiUrls.Ptecregi, PtecregiInput, null);

            }
            catch (System.Exception ex)
            {
                throw new GenericException("Error al enviar los Datos de la Api Truck - Ptecregi", ex);
            }
        }

        /// <summary>
        /// Pteczocoes the specified pteczoco input.
        /// </summary>
        /// <param name="PteczocoInput">The pteczoco input.</param>
        /// <exception cref="GenericException">Error al enviar los Datos de la Api Truck - Pteczoco</exception>
        public async Task Pteczoco(PteczocoInput PteczocoInput)
        {
            try
            {
                var url = _apiUrls.ApiTruckBaseUrl();

                var clientTruck = new ClientTruck(url, _apiHeaders, _timeoutApi);

                var dataTruck = await clientTruck.PostAsync<object>(_apiUrls.Pteczoco, PteczocoInput, null);

            }
            catch (System.Exception ex)
            {
                throw new GenericException("Error al enviar los Datos de la Api Truck - Pteczoco", ex);
            }
        }

        /// <summary>
        /// Pteczonas the specified pteczona input.
        /// </summary>
        /// <param name="PteczonaInput">The pteczona input.</param>
        /// <exception cref="GenericException">Error al enviar los Datos de la Api Truck - Pteczona</exception>
        public async Task Pteczona(PteczonaInput PteczonaInput)
        {
            try
            {
                var url = _apiUrls.ApiTruckBaseUrl();

                var clientTruck = new ClientTruck(url, _apiHeaders, _timeoutApi);

                var dataTruck = await clientTruck.PostAsync<object>(_apiUrls.Pteczona, PteczonaInput, null);

            }
            catch (System.Exception ex)
            {
                throw new GenericException("Error al enviar los Datos de la Api Truck - Pteczona", ex);
            }
        }

        /// <summary>
        /// Ptecterrs the specified ptecterr input.
        /// </summary>
        /// <param name="PtecterrInput">The ptecterr input.</param>
        /// <exception cref="GenericException">Error al enviar los Datos de la Api Truck - Ptecterr</exception>
        public async Task Ptecterr(PtecterrInput PtecterrInput)
        {
            try
            {
                var url = _apiUrls.ApiTruckBaseUrl();

                var clientTruck = new ClientTruck(url, _apiHeaders, _timeoutApi);

                var dataTruck = await clientTruck.PostAsync<object>(_apiUrls.Ptecterr, PtecterrInput, null);

            }
            catch (System.Exception ex)
            {
                throw new GenericException("Error al enviar los Datos de la Api Truck - Ptecterr", ex);
            }
        }


        #endregion
    }
}
