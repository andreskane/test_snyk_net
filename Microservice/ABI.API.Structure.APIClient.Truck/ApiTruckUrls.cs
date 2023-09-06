using ABI.API.Structure.APIClient.Truck.Models;
using System.Diagnostics.CodeAnalysis;

namespace ABI.API.Structure.APIClient.Truck
{
    [ExcludeFromCodeCoverage]
    public class ApiTruckUrls : IApiTruckUrls
    {
        private readonly string _baseUrl;
        private readonly int? _timeoutApi;
        public ApiTruckOptions Options { get; set; }

        #region Methods

        public string EstructuraVentas => "EstructuraVentas";
        public string EstructuraRutas => "EstructuraRutas";
        public string EstructuraVersion => "EstructuraVersion";
        public string CategoriaVendedores => "CategoriaVendedores";
        public string CentroDeDespacho => "CentroDespacho";
        public string Responsable => "Responsable";
        public string TiposCategoriaVendedores => "TiposCategoriaVendedores";
        public string TipoVendedores => "TipoVendedores";
        public string Wkgresttester => "wkgresttester";

        public string Opecpini => "opecpini";
        public string Ptecdire => "ptecdire";
        public string Ptecarea => "ptecarea";
        public string Ptecgere => "ptecgere";
        public string Ptecregi => "ptecregi";
        public string Ptecterr => "ptecterr";
        public string Pteczoco => "pteczoco";
        public string Pteczona => "pteczona";


        public ApiTruckUrls(string baseUrl, int? timeoutApi)
        {
            _baseUrl = baseUrl;
            _timeoutApi = timeoutApi;
        }

        /// <summary>
        /// APIs the truck base URL.
        /// </summary>
        /// <returns></returns>
        public string ApiTruckBaseUrl()
        {
            return !string.IsNullOrEmpty(_baseUrl) ? _baseUrl : "";
        }

        public int ApiTrucktimeoutApi()
        {
            return _timeoutApi.HasValue ? _timeoutApi.Value : 240000;
        }

        #endregion

    }

}
