using System.ComponentModel;

namespace ABI.API.Structure.ACL.Truck.Application.Transformations
{
    public enum TypeTrasformation
    {
        [Description("LevelPortalToTruckTransformation")]
        LevelPortalToTruckTransformation = 0,
        [Description("LevelTruckToPortalTransformation")]
        LevelTruckToPortalTransformation,

        [Description("ActiveNodePortalToTruckTransformation")]
        ActiveNodePortalToTruckTransformation,
        [Description("ActiveNodeTruckToPortalTransformation")]
        ActiveNodeTruckToPortalTransformation,

        [Description("EmployeeIdPortalToTruckTransformation")]
        EmployeeIdPortalToTruckTransformation,
        [Description("EmployeeIdTruckToPortalTransformation")]
        EmployeeIdTruckToPortalTransformation,

        [Description("AttentionModeTruckToPortalTransformation")]
        AttentionModeTruckToPortalTransformation,

        [Description("RoleTruckToPortalTransformation")]
        RoleTruckToPortalTransformation,

        [Description("RoleTerritoryTruckToPortalTransformation")]
        RoleTerritoryTruckToPortalTransformation,

        [Description("ShortNameTransformation")]
        ShortNameTransformation,

        [Description("StructureModelTruckToPortalTransformation")]
        StructureModelTruckToPortalTransformation,

        [Description("VendorTruckPortalToTruckTransformation")]
        VendorTruckPortalToTruckTransformation,

        [Description("VacantPersonTruckToPortalTransformation")]
        VacantPersonTruckToPortalTransformation

    }
}
