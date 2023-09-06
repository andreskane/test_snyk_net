using ABI.API.Structure.Domain.AggregatesModel.StructureAggregate;
using ABI.API.Structure.Domain.Entities;
using ABI.API.Structure.Infrastructure;
using ABI.API.Structure.Unit.Tests.Mock;
using System.Collections.Generic;
using System.IO;

namespace ABI.API.Structure.Unit.Tests.Inits
{
    public static class AddDataContext
    {

        public static StructureContext _context;

        public static void PrepareFactoryData()
        {
            var factory = new ConnectionFactory();
            _context = factory.CreateContextForSQLite();
            FillData();

            _context.SaveChanges();
        }

        private static void FillData()
        {
            Fill<AttentionMode>("DBO.AttentionMode.json");
            Fill<Level>("DBO.Level.json");
            Fill<Role>("DBO.Rol.json");
            Fill<SaleChannel>("DBO.SaleChannel.json");
            Fill<StructureModel>("DBO.StructureModel.json");
            Fill<StructureModelDefinition>("DBO.StructureModelDefinition.json");
            Fill<TypeGroup>("DBO.TypeGroup.json");
            Fill<Type>("DBO.Type.json");
            Fill<AttentionModeRole>("DBO.AttentionModeRole.json");
            Fill<ObjectType>("DBO.ObjectType.json");
            Fill<ChangeTrackingStatus>("DBO.ChangeTrackingStatus.json");
            Fill<ChangeTracking>("DBO.ChangeTracking.json");
            Fill<State>("DBO.State.json");
            Fill<StateGroup>("DBO.StateGroup.json");
            Fill<Motive>("DBO.Motive.json");
            Fill<MotiveState>("DBO.MotiveState.json");
            Fill<StructureDomain>("DBO.Structure.json");
            Fill<StructureNode>("DBO.StructureNode.json");
            Fill<StructureNodeDefinition>("DBO.StructureNodeDefinition.json");
            Fill<StructureArista>("DBO.StructureArista.json");
            Fill<StructureClientNode>("DBO.StructureClientNodes.json");
            Fill<Country>("DBO.Country.json");
            Fill<MostVisitedFilter>("DBO.MostVisitedFilter.json");
        }

        private static void Fill<TTarget>(string jsonFileName) where TTarget : class
        {
            var targetList = FactoryMock.GetMockJson<List<TTarget>>(Path.Combine("MockFile", jsonFileName));
            _context.Set<TTarget>().AddRange(targetList);
        }
    }
}
