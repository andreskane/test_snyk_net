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
using System.Threading.Tasks;

namespace ABI.API.Structure.APIClient.Truck
{
    public interface IApiTruck
    {

        Task<TruckStructure> GetStructureTruck(string empID);
        Task<TruckTerritory> GetTerritoryTruck(string empID, string trrId);
        Task<TruckTerritory> GetTerritoryTruckByManagement(string empID, string management);
        Task<TruckSellerCategory> GetAllSellerCategory();
        Task<TruckOfficeCenter> GetAllOfficeCenter();
        Task<TruckResponsible> GetAllResponsible();
        Task<TruckTypeCategorySeller> GetAllTypeCategorySeller();
        Task<TruckTypeSeller> GetAllTypeSeller();
        Task<StatusApi> GetStatusApi();


        Task<OpecpiniOut> SetOpecpini(OpecpiniInput opeIni);

        Task Ptecdire(PtecdireInput ptecdireInput);
        Task Ptecarea(PtecareaInput PtecareaInput);
        Task Ptecgere(PtecgereInput PtecgereInput);
        Task Ptecregi(PtecregiInput PtecregiInput);
        Task Pteczoco(PteczocoInput PteczocoInput);
        Task Pteczona(PteczonaInput PteczonaInput);
        Task Ptecterr(PtecterrInput PtecterrInput);
        Task<EstructuraVersionOutput> GetStructureVersionStatusTruck(EstructuraVersionInput ini);
    }
}