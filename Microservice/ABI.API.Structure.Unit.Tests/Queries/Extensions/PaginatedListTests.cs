using ABI.API.Structure.Application.DTO;
using ABI.API.Structure.Application.Queries.Extensions;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace ABI.API.Structure.Unit.Tests.Queries.Extensions
{
    [TestClass()]
    public class PaginatedListTests
    {
        [TestMethod()]
        public void PaginatedListTest()
        {
            var model = new PaginatedSearchDTO();
            model.PageIndex = 2;
            model.PageSize = 1;
            model.SortDirection = "DESC";
            model.SortOrder = "Id";
            List<ItemDTO> result = new List<ItemDTO>();
            result.Add(new ItemDTO() { Description = "asd", Id = 1, Name = "Nombre" });
            result.Add(new ItemDTO() { Description = "asd", Id = 2, Name = "Nombre2" });
            result.Add(new ItemDTO() { Description = "asd", Id = 3, Name = "Nombre3" });
            var paginatedResult = PaginatedList<ItemDTO>.Create(result, model.PageIndex, model.PageSize, model.SortOrder, model.SortDirection);
            paginatedResult.Should().NotBeNull();
            paginatedResult.HasNextPage.Should().BeTrue();
            paginatedResult.HasPreviousPage.Should().BeTrue();
            paginatedResult.TotalCount.Should().Be(3);
            paginatedResult.PageSize.Should().Be(1);
            paginatedResult.To.Should().Be(2);
        }

        [TestMethod()]
        public void PaginatedListWithoutOrderTest()
        {
            var model = new PaginatedSearchDTO();
            model.PageIndex = 2;
            model.PageSize = 1;
            List<ItemDTO> result = new List<ItemDTO>();
            result.Add(new ItemDTO() { Description = "asd", Id = 1, Name = "Nombre" });
            result.Add(new ItemDTO() { Description = "asd", Id = 2, Name = "Nombre2" });
            result.Add(new ItemDTO() { Description = "asd", Id = 3, Name = "Nombre3" });
            var paginatedResult = PaginatedList<ItemDTO>.Create(result, model.PageIndex, model.PageSize, model.SortOrder, model.SortDirection);
            paginatedResult.Should().NotBeNull();
        }

        [TestMethod()]
        public void PaginatedListAscTest()
        {
            var model = new PaginatedSearchDTO();
            model.PageIndex = 1;
            model.PageSize = 5;
            model.Search = "a";
            model.SortDirection = "ASC";
            model.SortOrder = "Id";
            List<ItemDTO> result = new List<ItemDTO>();
            result.Add(new ItemDTO() { Description = "asd", Id = 1, Name = "Nombre" });
            result.Add(new ItemDTO() { Description = "asd", Id = 2, Name = "Nombre2" });
            result.Add(new ItemDTO() { Description = "asd", Id = 3, Name = "Nombre3" });
            var paginatedResult = PaginatedList<ItemDTO>.Create(result, model.PageIndex, model.PageSize, model.SortOrder, model.SortDirection);
            paginatedResult.Should().NotBeNull();
            paginatedResult.Items[0].Id.Should().Be(1);
        }

        [TestMethod()]
        public void PaginatedListDescTest()
        {
            var model = new PaginatedSearchDTO();
            model.PageIndex = 1;
            model.PageSize = 5;
            model.SortDirection = "DESC";
            model.SortOrder = "Id";
            List<ItemDTO> result = new List<ItemDTO>();
            result.Add(new ItemDTO() { Description = "asd", Id = 1, Name = "Nombre" });
            result.Add(new ItemDTO() { Description = "asd", Id = 2, Name = "Nombre2" });
            result.Add(new ItemDTO() { Description = "asd", Id = 3, Name = "Nombre3" });
            var paginatedResult = PaginatedList<ItemDTO>.Create(result, model.PageIndex, model.PageSize, model.SortOrder, model.SortDirection);
            paginatedResult.Should().NotBeNull();
            paginatedResult.Items[0].Id.Should().Be(3);
        }

        [TestMethod()]
        public void PaginatedListNextTest()
        {
            var model = new PaginatedSearchDTO();
            model.PageIndex = 1;
            model.PageSize = 1;
            model.SortDirection = "DESC";
            model.SortOrder = "Id";
            List<ItemDTO> result = new List<ItemDTO>();
            result.Add(new ItemDTO() { Description = "asd", Id = 1, Name = "Nombre" });
            result.Add(new ItemDTO() { Description = "asd", Id = 2, Name = "Nombre2" });
            result.Add(new ItemDTO() { Description = "asd", Id = 3, Name = "Nombre3" });
            var paginatedResult = PaginatedList<ItemDTO>.Create(result, model.PageIndex, model.PageSize, model.SortOrder, model.SortDirection);
            paginatedResult.Should().NotBeNull();
            paginatedResult.HasNextPage.Should().BeTrue();
            paginatedResult.HasPreviousPage.Should().BeFalse();
        }

        [TestMethod()]
        public void PaginatedListLastTest()
        {
            var model = new PaginatedSearchDTO();
            model.PageIndex = 3;
            model.PageSize = 1;
            model.SortDirection = "DESC";
            model.SortOrder = "Id";
            List<ItemDTO> result = new List<ItemDTO>();
            result.Add(new ItemDTO() { Description = "asd", Id = 1, Name = "Nombre" });
            result.Add(new ItemDTO() { Description = "asd", Id = 2, Name = "Nombre2" });
            result.Add(new ItemDTO() { Description = "asd", Id = 3, Name = "Nombre3" });
            var paginatedResult = PaginatedList<ItemDTO>.Create(result, model.PageIndex, model.PageSize, model.SortOrder, model.SortDirection);
            paginatedResult.Should().NotBeNull();
            paginatedResult.HasNextPage.Should().BeFalse();
            paginatedResult.HasPreviousPage.Should().BeTrue();
        }
    }
}
