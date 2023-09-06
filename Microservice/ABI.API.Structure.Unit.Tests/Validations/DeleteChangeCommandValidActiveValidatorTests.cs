using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using ABI.API.Structure.Application.Commands.RequestsTray;
using ABI.API.Structure.Application.Validations;
using ABI.API.Structure.Domain.AggregatesModel.StructureAggregate;
using ABI.API.Structure.Infrastructure.Repositories;
using ABI.API.Structure.Infrastructure.Repositories.Interfaces;
using ABI.API.Structure.Infrastructure.RepositoriesDomain;
using ABI.API.Structure.Unit.Tests.Inits;
using ABI.Framework.MS.Caching;
using ABI.Framework.MS.Net.RestClient;

using FluentAssertions;

using FluentValidation.TestHelper;

using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace ABI.API.Structure.Unit.Tests.Validations
{
    [TestClass()]
    public class DeleteChangeCommandValidActiveValidatorTests
    {
        private static IChangeTrackingRepository _repository;
        private static IStructureNodeRepository _structureNodeRepository;
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
            _structureNodeRepository = new StructureNodeRepository(AddDataContext._context);
        }

        [TestMethod()]
        public async Task DeleteChangeCommandValidActiveValidatorExceptionArgumentTest()
        {
            var mockLogger = new Mock<ILogger<DeleteChangeCommandValidActiveValidator>>();
            var throws = await Assert.ThrowsExceptionAsync<ArgumentNullException>(() =>
                Task.Run(() => new DeleteChangeCommandValidActiveValidator(mockLogger.Object, null, _structureNodeRepository))
            );

            throws.Should().BeOfType(typeof(ArgumentNullException));
        }

        [TestMethod()]
        public async Task DeleteChangeCommandValidActiveValidatorExceptionArgument2Test()
        {
            var mockLogger = new Mock<ILogger<DeleteChangeCommandValidActiveValidator>>();
            var throws = await Assert.ThrowsExceptionAsync<ArgumentNullException>(() =>
                Task.Run(() => new DeleteChangeCommandValidActiveValidator(mockLogger.Object, _repository, null))
            );

            throws.Should().BeOfType(typeof(ArgumentNullException));
        }

        [TestMethod()]
        public void DeleteChangeCommandValidActiveValidatorTest()
        {
            var mockLogger = new Mock<ILogger<DeleteChangeCommandValidActiveValidator>>();
            var model = new DeleteChangeCommand { DeleteConfirm = false, Id = 34 };
            var validator = new DeleteChangeCommandValidActiveValidator(mockLogger.Object, _repository, _structureNodeRepository);
            var result = validator.TestValidate(model);

            result.ShouldNotHaveAnyValidationErrors();
        }

        [TestMethod()]
        public void DeleteChangeCommandValidActiveValidatorValidParentTest()
        {
            var mockLogger = new Mock<ILogger<DeleteChangeCommandValidActiveValidator>>();
            var model = new DeleteChangeCommand { DeleteConfirm = false, Id = 35 };
            var validator = new DeleteChangeCommandValidActiveValidator(mockLogger.Object, _repository, _structureNodeRepository);
            
            var result = validator.TestValidate(model);
            result.ShouldNotHaveAnyValidationErrors();
        }

        [TestMethod()]
        public void DeleteChangeCommandValidActiveValidatorValidClientsTest()
        {
            var mockLogger = new Mock<ILogger<DeleteChangeCommandValidActiveValidator>>();
            var model = new DeleteChangeCommand { DeleteConfirm = false, Id = 36 };
            var validator = new DeleteChangeCommandValidActiveValidator(mockLogger.Object, _repository, _structureNodeRepository);

            var result = validator.TestValidate(model);
            result.ShouldNotHaveAnyValidationErrors();
        }

        [TestMethod()]
        public void DeleteChangeCommandValidActiveValidatorValidChildsTest()
        {
            var mockLogger = new Mock<ILogger<DeleteChangeCommandValidActiveValidator>>();
            var model = new DeleteChangeCommand { DeleteConfirm = false, Id = 37 };
            var validator = new DeleteChangeCommandValidActiveValidator(mockLogger.Object, _repository, _structureNodeRepository);

            var result = validator.TestValidate(model);
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}
