using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ABI.API.Structure.Domain.Entities.Tests
{
    [TestClass()]
    public class ChangeTrackingTests
    {
        [TestMethod()]
        public void ChangeTrackingNodeTest()
        {
            var result = new ChangeTracking
            {
                ChangedValueJson = "{\"Field\": \"Name\",\"FieldName\": \"Nombre\",\"Node\": {\"Id\": 1,\"Name\": \"TEST A\",\"Code\": \"AAA\"},\"OldValue\": \"TEST A\",\"NewValue\": \"TEST B\"}",
                CreateDate = Convert.ToDateTime("2021-04-01"),
                Id=1,
                IdChangeType =1,
                IdObjectType=2,
                IdStructure=1,
                IdDestino =1,
                IdOrigen =1,
                NodePathJson = "{\"Ids\":[100013,1]}",
                UserJson = "{\"Id\":\"3E8FA66E-3619-48F0-9F0B-60500005D7EF\",\"Name\":\"SSO, ALLMOBILE\"}",
                ValidityFrom = Convert.ToDateTime("2021-04-01"),
                ObjectType = new Type()
            };

            result.Should().NotBeNull();
            result.ChangedValueNode.Should().NotBeNull();
            result.ChangedValueArista.Should().BeNull();
          //  result.ChangeTrackingStatusListItems.Should().BeNull();
            result.ChangeStatus.Should().BeNull();
            //result.ChangeTrackingStatus.Should().BeNull();
            result.CreateDate.Should().Be(Convert.ToDateTime("2021-04-01"));
            result.Id.Should().Be(1);
            result.IdChangeType.Should().Be(1);
            result.IdObjectType.Should().Be(2);
            result.IdStructure.Should().Be(1);
            result.IdOrigen.Should().Be(1);
            result.IdDestino.Should().Be(1);
            result.NodePath.Should().NotBeNull();
            result.NodePathJson.Should().Be("{\"Ids\":[100013,1]}");
            result.ObjectType.Should().NotBeNull();
            result.User.Should().NotBeNull();
            result.ValidityFrom.Should().Be(Convert.ToDateTime("2021-04-01"));
        }

        [TestMethod()]
        public void ChangeTrackingAristaTest()
        {
            var result = new ChangeTracking
            {
                ChangedValueJson = "{\"AristaActual\":{\"AristaId\":100013,\"OldValue\":{\"StructureIdFrom\":7,\"NodeIdFrom\":100013,\"AristaValidityTo\":\"9999-12-31T00:00:00\"},\"NewValue\":{\"StructureIdFrom\":7,\"NodeIdFrom\":100013,\"AristaValidityTo\":\"2021-03-28T00:00:00Z\"}},\"AristaNueva\":{\"AristaId\":100022,\"StructureIdFrom\":7,\"NodeIdFrom\":100013,\"AristaValidityFrom\":\"2021-03-29T00:00:00Z\",\"AristaValidityTo\":\"9999-12-31T00:00:00\"}}",
                CreateDate = Convert.ToDateTime("2021-04-01"),
                Id = 1,
                IdChangeType = 1,
                IdObjectType = 3,
                IdStructure = 1,
                NodePathJson = "{\"Ids\":[100013,1]}",
                UserJson = "{\"Id\":\"3E8FA66E-3619-48F0-9F0B-60500005D7EF\",\"Name\":\"SSO, ALLMOBILE\"}",
                ValidityFrom = Convert.ToDateTime("2021-04-01")
            };

            result.Should().NotBeNull();
            result.ChangedValueNode.Should().BeNull();
            result.ChangedValueArista.Should().NotBeNull();
          //  result.ChangeTrackingStatusListItems.Should().BeNull();
            result.ChangeStatus.Should().BeNull();
            //result.ChangeTrackingStatus.Should().BeNull();
            result.CreateDate.Should().Be(Convert.ToDateTime("2021-04-01"));
            result.Id.Should().Be(1);
            result.IdChangeType.Should().Be(1);
            result.IdObjectType.Should().Be(3);
            result.IdStructure.Should().Be(1);
            result.NodePath.Should().NotBeNull();
            result.NodePathJson.Should().Be("{\"Ids\":[100013,1]}");
            result.ObjectType.Should().BeNull();
            result.User.Should().NotBeNull();
            result.ValidityFrom.Should().Be(Convert.ToDateTime("2021-04-01"));
        }

        [TestMethod()]
        public void ChangeTrackingAristaByObjectTest()
        {
            var result = new ChangeTracking
            {
                ChangedValueArista = new AggregatesModel.ChangeTrackingAggregate.ChangeTrackingArista
                {
                    AristaActual = new AggregatesModel.ChangeTrackingAggregate.ItemAristaActual(),
                    AristaNueva = new AggregatesModel.ChangeTrackingAggregate.ItemArista(),
                },
                CreateDate = Convert.ToDateTime("2021-04-01"),
                Id = 1,
                IdChangeType = 1,
                IdObjectType = 3,
                IdStructure = 1,
                NodePathJson = "{\"Ids\":[100013,1]}",
                UserJson = "{\"Id\":\"3E8FA66E-3619-48F0-9F0B-60500005D7EF\",\"Name\":\"SSO, ALLMOBILE\"}",
                ValidityFrom = Convert.ToDateTime("2021-04-01")
            };

            result.Should().NotBeNull();
            result.ChangedValueNode.Should().BeNull();
            result.ChangedValueArista.Should().NotBeNull();
//            result.ChangeTrackingStatusListItems.Should().BeNull();
            result.ChangeStatus.Should().BeNull();
            //result.ChangeTrackingStatus.Should().BeNull();
            result.CreateDate.Should().Be(Convert.ToDateTime("2021-04-01"));
            result.Id.Should().Be(1);
            result.IdChangeType.Should().Be(1);
            result.IdObjectType.Should().Be(3);
            result.IdStructure.Should().Be(1);
            result.NodePath.Should().NotBeNull();
            result.NodePathJson.Should().Be("{\"Ids\":[100013,1]}");
            result.ObjectType.Should().BeNull();
            result.User.Should().NotBeNull();
            result.ValidityFrom.Should().Be(Convert.ToDateTime("2021-04-01"));
        }

        [TestMethod()]
        public void ChangeTrackingConverToJSONAristaTest()
        {
            var result = new ChangeTracking
            {
                ChangedValueJson = string.Empty,
                NodePathJson = string.Empty,
                UserJson = string.Empty,
            };

            result.Should().NotBeNull();
            result.ChangedValueJson.Should().BeEmpty();
            result.NodePathJson.Should().BeEmpty();
            result.UserJson.Should().BeEmpty();
        }

        [TestMethod()]
        public void ChangeTrackingKindOfChangeTest()
        {
            var change1 = new ChangeTracking
            {
                IdChangeType = 4
            };
            var result1 = change1.KindOfChange();
            result1.Should().Be("Estructura");

            var change2 = new ChangeTracking
            {
                IdChangeType = 5
            };
            var result2 = change2.KindOfChange();
            result2.Should().Be("Rol");

            var change3 = new ChangeTracking
            {
                IdChangeType = 6
            };
            var result3 = change3.KindOfChange();
            result3.Should().Be("Persona");
        }

        [TestMethod()]
        public void ChangeTrackingKindOfChangeExceptionTest()
        {
            var change1 = new ChangeTracking
            {
                IdChangeType = 7
            };
            change1
                .Invoking((i) => i.KindOfChange())
                .Should().Throw<NotImplementedException>();

        }

        [TestMethod()]
        public void ChangeTrackingGetPortalStatusTest()
        {
            var change1 = new ChangeTracking();
            var result = change1.GetPortalStatus(true);
            result.Should().Be(3);
        }
    }
}
