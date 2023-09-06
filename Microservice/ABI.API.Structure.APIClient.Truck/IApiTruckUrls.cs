namespace ABI.API.Structure.APIClient.Truck
{
    public interface IApiTruckUrls
    {
        string CategoriaVendedores { get; }
        string CentroDeDespacho { get; }
        string EstructuraRutas { get; }
        string EstructuraVentas { get; }
        string EstructuraVersion { get; }
        string Responsable { get; }
        string TiposCategoriaVendedores { get; }
        string TipoVendedores { get; }
        string Wkgresttester { get; }

        string Opecpini { get; }
        string Ptecdire { get; }
        string Ptecarea { get; }
        string Ptecgere { get; }
        string Ptecregi { get; }
        string Ptecterr { get; }
        string Pteczoco { get; }
        string Pteczona { get; }

        string ApiTruckBaseUrl();
        int ApiTrucktimeoutApi();
    }
}