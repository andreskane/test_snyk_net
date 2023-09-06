using ABI.API.Structure.Application.DTO;
using ABI.API.Structure.Application.Exceptions;
using ABI.Framework.MS.Domain.Exceptions;
using ABI.Framework.MS.Helpers.Response;
using ABI.Framework.MS.Service.Exceptions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ABI.API.Structure.Infrastructure.Extensions
{
    public static class ExceptionExtensions
    {
        public static GenericResponse ToGenericResponse(this PropertyException ex)
        {
            return ToGenericResponse(ex, ex.Property);
        }

        public static GenericResponse ToGenericResponse(this DomainException ex, string property)
        {
            var errors = new Dictionary<string, string[]> { { property, new string[] { ex.Message } } };
            return new GenericResponse(StatusGenericResponse.BadRequest, errors);
        }

        public static async Task<GenericResponse> CatchNodeValidation(this Func<Task<int?>> action, StatusGenericResponse status, int level)
        {
            try
            {
                var result = await action();
                return new GenericResponse(StatusGenericResponse.Created) { Result = result };
            }
            catch (PropertyException e)
            {
                return e.ToGenericResponse();
            }
            catch (NodeEmployeeResponsableZonesException e)
            {
                var property = (level == 7 ? "EmployeeId" : "AttentionModeId");
                return e.ToGenericResponse(property);
            }
            catch (NodeEmployeeNoResponsableZonesException e)
            {
                var property = (level == 7 ? "EmployeeId" : "AttentionModeId");
                return e.ToGenericResponse(property);
            }
            catch (NameExistsException e)
            {
                var errors = new Dictionary<string, string[]> { { "Name", new string[] { e.Message } } };
                return new GenericResponse(StatusGenericResponse.BadRequest, errors);
            }
            catch (StructureCodeExistsException e)
            {
                return e.ToGenericResponse("Code");
            }
            catch (NodeEditSameDateException e)
            {
                var invalidData = new GenericResponse(StatusGenericResponse.BadRequest);
                invalidData.AddMessage(e.Message);
                return invalidData;
            }

        }
        public static async Task<GenericResponse> CatchStructureValidation(this Func<Task<StructureDTO?>> action, StatusGenericResponse status)
        {
            try
            {
                var result = await action();
                return new GenericResponse(StatusGenericResponse.Created) { Result = result };
            }
            catch (NameExistsException e)
            {
                var errors = new Dictionary<string, string[]> { { "Name", new string[] { e.Message } } };
                return new GenericResponse(StatusGenericResponse.BadRequest, errors);
            }
            catch (StructureCodeExistsException e)
            {
                return e.ToGenericResponse("Code");
            }
            catch (NodeCodeLengthException e)
            {
                return e.ToGenericResponse("Code");
            }
        }
    }
}
