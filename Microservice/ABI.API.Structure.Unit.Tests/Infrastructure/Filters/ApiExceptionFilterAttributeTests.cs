using ABI.API.Structure.Application.Exceptions;
using ABI.Framework.MS.Net.RestClient;
using ABI.Framework.MS.Service.Exceptions;

using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;

namespace ABI.API.Structure.Infrastructure.Filters.Tests
{
    [TestClass()]
    public class ApiExceptionFilterAttributeTests
    {
        private static ApiExceptionFilterAttribute _filter;
        private static ActionContext _actionContext;


        [ClassInitialize]
        public static void Setup(TestContext context)
        {
            var httpContextMock = new Mock<HttpContext>();
            var routeDataMock = new Mock<RouteData>();
            var actionDescriptorMock = new Mock<ActionDescriptor>();
            var actionContextMock = new Mock<ActionContext>(httpContextMock.Object, routeDataMock.Object, actionDescriptorMock.Object);
            var loggerMock = new Mock<ILogger<ApiExceptionFilterAttribute>>();

            _filter = new ApiExceptionFilterAttribute(loggerMock.Object);
            _actionContext = actionContextMock.Object;
        }


        [TestMethod()]
        public void ApiExceptionFilterAttributeTest()
        {
            var result = new ApiExceptionFilterAttribute(null);
            result.Should().NotBeNull();
        }

        [TestMethod()]
        public void OnExceptionBadRequestTest()
        {
            var contextMock = new Mock<ExceptionContext>(_actionContext, new List<IFilterMetadata>());
            contextMock
                .Setup(s => s.Exception)
                .Returns(new BadRequestException(400, "BAD"));

            _filter
                .Invoking((i) => i.OnException(contextMock.Object))
                .Should().NotThrow();
        }

        [TestMethod()]
        public void OnExceptionBadRequestNoCodeTest()
        {
            var contextMock = new Mock<ExceptionContext>(_actionContext, new List<IFilterMetadata>());
            contextMock
                .Setup(s => s.Exception)
                .Returns(new BadRequestException("BAD"));

            _filter
                .Invoking((i) => i.OnException(contextMock.Object))
                .Should().NotThrow();
        }

        [TestMethod()]
        public void OnExceptionNotFoundExceptionTest()
        {
            var contextMock = new Mock<ExceptionContext>(_actionContext, new List<IFilterMetadata>());
            contextMock
                .Setup(s => s.Exception)
                .Returns(new NotFoundException("NOTFOUND"));

            _filter
                .Invoking((i) => i.OnException(contextMock.Object))
                .Should().NotThrow();
        }

        [TestMethod()]
        public void OnExceptionCorruptedFileExceptionTest()
        {
            var contextMock = new Mock<ExceptionContext>(_actionContext, new List<IFilterMetadata>());
            contextMock
                .Setup(s => s.Exception)
                .Returns(new CorruptedFileException("CORRUPTED"));

            _filter
                .Invoking((i) => i.OnException(contextMock.Object))
                .Should().NotThrow();
        }

        [TestMethod()]
        public void OnExceptionValidationExceptionTest()
        {
            var contextMock = new Mock<ExceptionContext>(_actionContext, new List<IFilterMetadata>());
            contextMock
                .Setup(s => s.Exception)
                .Returns(new ValidationException());

            _filter
                .Invoking((i) => i.OnException(contextMock.Object))
                .Should().NotThrow();
        }

        [TestMethod()]
        public void OnExceptionHandleUknownTest()
        {
            var contextMock = new Mock<ExceptionContext>(_actionContext, new List<IFilterMetadata>());
            contextMock
                .Setup(s => s.Exception)
                .Returns(new Exception());

            _filter
                .Invoking((i) => i.OnException(contextMock.Object))
                .Should().NotThrow();
        }

        [TestMethod()]
        public void OnExceptionBadRequestListTest()
        {
            var contextMock = new Mock<ExceptionContext>(_actionContext, new List<IFilterMetadata>());
            contextMock
                .Setup(s => s.Exception)
                .Returns(new ChangeTrackingDateException(new List<Application.DTO.GenericValueDescriptionDto>{ 
                    new Application.DTO.GenericValueDescriptionDto
                    {
                        Value ="BAD",
                        Descripcion ="BAD"
                    }
                }));

            _filter
                .Invoking((i) => i.OnException(contextMock.Object))
                .Should().NotThrow();
        }

        [TestMethod()]
        public void OnExceptionConfirmExceptionTest()
        {
            var contextMock = new Mock<ExceptionContext>(_actionContext, new List<IFilterMetadata>());
            contextMock
                .Setup(s => s.Exception)
                .Returns(new ConfirmException("CORRUPTED"));

            _filter
                .Invoking((i) => i.OnException(contextMock.Object))
                .Should().NotThrow();
        }

        [TestMethod()]
        public void OnExceptionNameExistsExceptionTest()
        {
            var contextMock = new Mock<ExceptionContext>(_actionContext, new List<IFilterMetadata>());
            contextMock
                .Setup(s => s.Exception)
                .Returns(new NameExistsException("CORRUPTED"));

            _filter
                .Invoking((i) => i.OnException(contextMock.Object))
                .Should().NotThrow();
        }
    }
}