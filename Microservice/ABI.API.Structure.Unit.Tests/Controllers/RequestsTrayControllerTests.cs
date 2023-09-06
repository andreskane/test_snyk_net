using ABI.API.Structure.Application.Commands.RequestsTray;
using ABI.API.Structure.Application.DTO;
using ABI.API.Structure.Application.Exceptions;
using ABI.API.Structure.Application.Queries.Extensions;
using ABI.API.Structure.Application.Queries.RequestsTray;
using ABI.Framework.MS.Helpers.Response;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Q = ABI.API.Structure.Application.Queries.RequestsTray;

namespace ABI.API.Structure.Controllers.Tests
{
    [TestClass()]
    public class RequestsTrayControllerTests
    {
        private static RequestsTrayController _controller;


        [ClassInitialize]
        public static void Setup(TestContext context)
        {
            var mediatrMock = new Mock<IMediator>();
            mediatrMock
                .Setup(s => s.Send(It.IsAny<Q.GetFiltersOptionsQuery>(), default))
                .ReturnsAsync(new RequestTrayFiltersDTO());
            mediatrMock
                .Setup(s => s.Send(It.Is<DeleteChangeGroupCommand>(p => p.structureId.Equals(10)), default))
                .ThrowsAsync(new ChangeTrackingDateException (new List<GenericValueDescriptionDto>
                {
                    new GenericValueDescriptionDto
                    {
                        Descripcion = "TEST",
                        Value = "TEST"
                    }
                }
                ));

            _controller = new RequestsTrayController(mediatrMock.Object);
        }

        [TestMethod()]
        public async Task GetAllPaginatedSearchTest()
        {
            var model = new PaginatedSearchDTO();
            model.PageIndex = 1;
            model.PageSize = 5;
            var result = await _controller.GetAllPaginatedSearch(model);
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(GenericResponse<PaginatedList<RequestTrayDTO>>));
        }

        [TestMethod()]
        public async Task GetPaginatedSearchByStructureTest()
        {
            var model = new PaginatedSearchDTO();
            model.PageIndex = 1;
            model.PageSize = 5;
            var result = await _controller.GetPaginatedSearchByStructure(1, DateTime.UtcNow.Date, DateTime.UtcNow.Date, model);
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(GenericResponse<PaginatedList<RequestTrayDTO>>));
        }

        [TestMethod()]
        public async Task GetOneWithDetailTest()
        {
            var result = await _controller.GetOneWithDetail(1, new Guid("3e8fa66e-3619-48f0-9f0b-60500005d7ef"), DateTime.UtcNow.Date);
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(GenericResponse<IList<RequestTrayDetailDTO>>));
        }

        [TestMethod()]
        public async Task GetFiltersOptionsTest()
        {

            var filter = new GetFiltersOptionsQuery();
            filter.sId = new Int32[3] { 1,2,3};
            filter.PeriodFrom = DateTime.Now.AddYears(-1);
            filter.PeriodTo = DateTime.Now.AddYears(1);
            var result = await _controller.GetFiltersOptions(filter);
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(ActionResult<GenericResponse<RequestTrayFiltersDTO>>));
        }

        [TestMethod()]
        public async Task DeleteOneItemTest()
        {
            var result = await _controller.DeleteDataAsync(new Application.Commands.RequestsTray.DeleteChangeCommand { Id = 13 });
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<ActionResult<GenericResponse>>();
        }

        [TestMethod()]
        public async Task DeleteGroupItemAsyncTest()
        {
            var result = await _controller.DeleteGroupItemAsync(
                new DeleteChangeGroupCommand { structureId = 1, validity = DateTimeOffset.UtcNow.Date }
            );
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<ActionResult<GenericResponse>>();
        }

        [TestMethod()]
        public async Task GetPaginatedSearchByParametresTest()
        {
            var input = new GetPaginatedSearchByParametersQuery { PageIndex = 1, PageSize = 5 };
            var result = await _controller.GetPaginatedSearchByParametres(input);
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<ActionResult<GenericResponse<PaginatedList<RequestTrayDTO>>>>();
        }
    }
}