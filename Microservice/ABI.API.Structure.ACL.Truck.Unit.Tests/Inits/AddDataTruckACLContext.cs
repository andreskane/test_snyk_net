using ABI.API.Structure.ACL.Truck.Domain.Entities;
using ABI.API.Structure.ACL.Truck.Infrastructure;
using ABI.API.Structure.Unit.Tests.Mock;
using System.Collections.Generic;
using System.IO;

namespace ABI.API.Structure.ACL.TruckTests.Inits
{
    public static class AddDataTruckACLContext
    {

        public static TruckACLContext _context;

        public static void PrepareFactoryData()
        {
            var factory = new ConnectionFactory();
            _context = factory.CreateTruckACLContextForSQLite();
            FillData();
            _context.SaveChanges();
        }

        private static void FillData()
        {
            Fill<BusinessTruckPortal>("ACL.Modelo_Estructura_Empresa_Truck.json");
            Fill<LevelTruckPortal>("ACL.Nivel_Truck_Portal.json");
            Fill<TypeVendorTruckPortal>("ACL.Tipo_Vendedor_Truck_Portal.json");
            Fill<EstructuraClienteTerritorioIO>("ACL.et_io.json");
            Fill<SyncLog>("ACL.Log_Sincronizacion.json");
            Fill<ImportProcess>("ACL.proceso_importacion.json");
            Fill<ResourceResponsable>("ACL.ResourceResponsable.json");


            Fill<VersionedStatus>("ACL.Versionado_Estado.json");
            Fill<VersionedLogStatus>("ACL.Versionado_Estado_Log.json");
            Fill<Versioned>("ACL.Versionado.json");
            Fill<VersionedNode>("ACL.Versionado_Nodo.json");
            Fill<VersionedArista>("ACL.Versionado_Aristas.json");
            Fill<VersionedLog>("ACL.Versionado_Log.json");
        }

        private static void Fill<TTarget>(string jsonFileName) where TTarget : class
        {
            var targetList = FactoryMock.GetMockJson<List<TTarget>>(Path.Combine("MockFile", jsonFileName));
            _context.Set<TTarget>().AddRange(targetList);
        }
    }
}
