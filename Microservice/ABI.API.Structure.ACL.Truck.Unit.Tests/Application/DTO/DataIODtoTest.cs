using ABI.API.Structure.ACL.Truck.Application.DTO.ImportProcess;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace ABI.API.Structure.Application.DTO.Tests
{
    [TestClass()]
    public class DataIODtoTests
    {

        [TestMethod]
        public void CreationTest()
        {
            var dto = new DataIODto();


            dto.CliId = "047905";
            dto.CliNom = "INC S.A.";
            dto.CliSts = "1";
            dto.CliTrrId = "29001";
            dto.EmpId = "001";


            dto.CliId.Should().Be("047905");
            dto.CliNom.Should().Be("INC S.A.");
            dto.CliSts.Should().Be("1");
            dto.CliTrrId.Should().Be("29001");
            dto.EmpId.Should().Be("001");
             
             
        }


        


       
}
}
