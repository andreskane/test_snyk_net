using ABI.API.Structure.ACL.Truck.Repositories;
using ABI.API.Structure.ACL.Truck.Repositories.Interfaces;
using ABI.API.Structure.Application.Commands.RequestsTray;
using ABI.API.Structure.Application.Exceptions;
using ABI.API.Structure.Application.Validations;
using ABI.API.Structure.Infrastructure.Repositories;
using ABI.API.Structure.Infrastructure.Repositories.Interfaces;
using ABI.API.Structure.Unit.Tests.Inits;
using ABI.Framework.MS.Caching;
using FluentAssertions;
using FluentValidation.TestHelper;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace ABI.API.Structure.Unit.Tests.Validations
{
    [TestClass()]
    public class DeleteChangeTrackingGroupDatesValidatorTests
    {
        private static IChangeTrackingRepository _repository;
        private static IVersionedRepository _versionedRepository;
        private static ICacheStore _cacheStore;

        [ClassInitialize]
        public static void Setup(TestContext context)
        {
            var services = new ServiceCollection();
            services.AddMemoryCache();
            var serviceProvider = services.BuildServiceProvider();
            Dictionary<string, TimeSpan> cachingConfiguration = new Dictionary<string, TimeSpan>();
            cachingConfiguration.Add("default", new TimeSpan(0, 1, 0, 0));
            var memoryCache = serviceProvider.GetService<IMemoryCache>();
            _cacheStore = new MemoryCacheStore(memoryCache, cachingConfiguration);

            _repository = new ChangeTrackingRepository(AddDataContext._context, _cacheStore);
            _versionedRepository = new VersionedRepository(AddDataTruckACLContext._context, _cacheStore);
        }

        [TestMethod()]
        public void DeleteChangeTrackingGroupDatesValidatorTest()
        {
            var model = new DeleteChangeGroupCommand {
                structureId = 11,
                validity = DateTimeOffset.UtcNow
            };
            var validator = new DeleteChangeTrackingGroupCommandValidator(_repository, _versionedRepository);
            var result = validator.TestValidate(model);

            result.ShouldNotHaveAnyValidationErrors();
        }

        [TestMethod()]
        public void DeleteChangeTrackingGroupDatesValidatorExceptionTest()
        {
            var model = new DeleteChangeGroupCommand
            {
                structureId = 10,
                validity = Convert.ToDateTime("2021-07-18")
            };
            var validator = new DeleteChangeTrackingGroupCommandValidator(_repository, _versionedRepository);

            validator
                .Invoking(i => validator.TestValidate(model))
                .Should().Throw<ChangeTrackingDateException>();
        }

        [TestMethod()]
        public void DeleteChangeTrackingGroupDatesVersionedValidatorTest()
        {
            var model = new DeleteChangeGroupCommand
            {
                structureId = 12,
                validity = Convert.ToDateTime("2021-08-01")
            };
            var validator = new DeleteChangeTrackingGroupCommandValidator(_repository, _versionedRepository);
            //var result = validator.TestValidate(model);

            //result.ShouldNotHaveAnyValidationErrors();
            Assert.Inconclusive();//TODO: Revisar origen de archivo
        }
    }
}
