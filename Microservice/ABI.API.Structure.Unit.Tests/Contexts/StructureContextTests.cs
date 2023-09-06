using ABI.API.Structure.Infrastructure;
using ABI.API.Structure.Unit.Tests.Inits;
using FluentAssertions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ABI.API.Structure.Unit.Tests.Contexts
{
    [TestClass()]
    public class StructureContextTests
    {
        private static DbContextOptionsBuilder<StructureContext> _builder;
        private static StructureContext _context;


        [ClassInitialize()]
        public static void Setup(TestContext context)
        {
            _builder = new DbContextOptionsBuilder<StructureContext>();
            _builder.UseInMemoryDatabase("TestDB");

            _context = new StructureContext(_builder.Options);
        }


        [TestMethod]
        public void Should_Initialize_Context()
        {
            var context = new StructureContext(_builder.Options);
            context.Should().NotBeNull();
        }

        [TestMethod]
        public void Should_Initialize_With_Mediator_Context()
        {
            var mediatr = new Moq.Mock<IMediator>();
            var context = new StructureContext(_builder.Options, mediatr.Object, InitCurrentUserService.GetDefaultUserService());
            context.Should().NotBeNull();
        }

        [TestMethod]
        public void Should_Initialize_With_NullMediator_Context()
        {
            var throws = Assert.ThrowsException<ArgumentNullException>(() => new StructureContext(_builder.Options, null, InitCurrentUserService.GetDefaultUserService()));
            throws.Should().BeOfType(typeof(ArgumentNullException));

        }

        [TestMethod]
        public void DBSet_AttentionModeRole_NotBeNull()
        {
            var dbset = _context.AttentionModeRole;
            dbset.Should().BeEmpty();
        }

        [TestMethod]
        public void DBSet_AttentionMode_NotBeNull()
        {
            var dbset = _context.AttentionMode;
            dbset.Should().BeEmpty();
        }

        [TestMethod]
        public void DBSet_Levels_NotBeNull()
        {
            var dbset = _context.Levels;
            dbset.Should().BeEmpty();
        }

        [TestMethod]
        public void DBSet_Roles_NotBeNull()
        {
            var dbset = _context.Roles;
            dbset.Should().BeEmpty();
        }

        [TestMethod]
        public void DBSet_StructureModels_NotBeNull()
        {
            var dbset = _context.StructureModels;
            dbset.Should().BeEmpty();
        }

        [TestMethod]
        public void DBSet_ModelStructuresDefinitions_NotBeNull()
        {
            var dbset = _context.ModelStructuresDefinitions;
            dbset.Should().BeEmpty();
        }

        [TestMethod]
        public void DBSet_SalesChannels_NotBeNull()
        {
            var dbset = _context.SalesChannels;
            dbset.Should().BeEmpty();
        }

        [TestMethod]
        public void DBSet_TypeGroups_NotBeNull()
        {
            var dbset = _context.TypeGroups;
            dbset.Should().BeEmpty();
        }

        [TestMethod]
        public void DBSet_Types_NotBeNull()
        {
            var dbset = _context.Types;
            dbset.Should().BeEmpty();
        }

        [TestMethod]
        public void DBSet_Structures_NotBeNull()
        {
            var dbset = _context.Structures;
            dbset.Should().BeEmpty();
        }

        [TestMethod]
        public void DBSet_StructureNodes_NotBeNull()
        {
            var dbset = _context.StructureNodes;
            dbset.Should().BeEmpty();
        }

        [TestMethod]
        public void DBSet_StructureAristas_NotBeNull()
        {
            var dbset = _context.StructureAristas;
            dbset.Should().BeEmpty();
        }

        [TestMethod]
        public void DBSet_StructureNodeDefinitions_NotBeNull()
        {
            var dbset = _context.StructureNodeDefinitions;
            dbset.Should().BeEmpty();
        }

        //[TestMethod]
        //public void DBSet_StatesTruck_NotBeNull()
        //{
        //    var dbset = _context.StatesTruck;
        //    dbset.Should().BeEmpty();
        //}

        [TestMethod]
        public void DBSet_ChangesTracking_NotBeNull()
        {
            var dbset = _context.ChangesTracking;
            dbset.Should().BeEmpty();
        }

        [TestMethod]
        public void DBSet_Motive_NotBeNull()
        {
            var dbset = _context.Motives;
            dbset.Should().BeEmpty();
        }

        [TestMethod]
        public void DBSet_MotiveStates_NotBeNull()
        {
            var dbset = _context.MotiveStates;
            dbset.Should().BeEmpty();
        }


        [TestMethod]
        public void DBSet_States_NotBeNull()
        {
            var dbset = _context.States;
            dbset.Should().BeEmpty();
        }

        [TestMethod]
        public void DBSet_StateGroups_NotBeNull()
        {
            var dbset = _context.StateGroups;
            dbset.Should().BeEmpty();
        }

        [TestMethod]
        public void DBSet_Country_NotBeNull()
        {
            var dbset = _context.Countrys;
            dbset.Should().BeEmpty();
        }

        public void StringsToUpperTest()
        {
            var dbset = _context.Countrys;
            dbset.Should().BeEmpty();
        }
    }
}
