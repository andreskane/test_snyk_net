
using ABI.API.Structure.ACL.Truck.Domain.Enums;
using ABI.API.Structure.ACL.Truck.Infrastructure.Repositories.Interfaces;
using ABI.Framework.MS.Helpers.Message;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using E = ABI.API.Structure.ACL.Truck.Domain.Entities;

namespace ABI.API.Structure.Application.Commands.ImportProcess
{
    public class CreateCommand : IRequest
    {
        [Required(ErrorMessage = ErrorMessageText.Required)]
        public DateTime ProcessDate { get; set; }
        [Required(ErrorMessage = ErrorMessageText.Required)]
        public string Condition { get; set; }
        [Required(ErrorMessage = ErrorMessageText.Required)]
        public Periodicity Periodicity { get; set; }
        public DateTime? To { get; set; }
        [Required(ErrorMessage = ErrorMessageText.Required)]
        public ImportProcessSource Source { get; set; }
        [Required(ErrorMessage = ErrorMessageText.Required)]
        public String companyId { get; set; }
    }

    public class CreateCommandHandler : IRequestHandler<CreateCommand>
    {
        private readonly IImportProcessRepository _repository;
        private readonly IPeriodicityDaysRepository _periodicityDaysRepo;

        public CreateCommandHandler(IImportProcessRepository repository, IPeriodicityDaysRepository periodicityDaysRepo)
        {
            _repository = repository;
            _periodicityDaysRepo = periodicityDaysRepo;
        }
        public async Task<Unit> Handle(CreateCommand request, CancellationToken cancellationToken)
        {
            var periodicityDictionary = await _periodicityDaysRepo.GetPeriodicityToDaysDictionary(cancellationToken);

            await _repository.BulkInsertAsync(GenerateProcesses(request, periodicityDictionary), cancellationToken);

            return Unit.Value;
        }

        private IList<E.ImportProcess> GenerateProcesses(CreateCommand request, IDictionary<Periodicity, int> periodicityDict)
        {
            var generatorDict = new Dictionary<Periodicity, Func<CreateCommand, IDictionary<Periodicity, int>, IList<E.ImportProcess>>>()
            {
                { Periodicity.OneTime, (req, dict) => GenerateOneTimeProcess(req) },
                { Periodicity.Daily, (req, dict) => GenerateCustomPeriodicityProcesses(req, dict[req.Periodicity], 1) },
                { Periodicity.Weekly, (req, dict) => GenerateCustomPeriodicityProcesses(req, dict[req.Periodicity], 7) },
                { Periodicity.Monthly, (req, dict) => GenerateMonthlyProcess(req) },
            };

            return generatorDict[request.Periodicity].Invoke(request, periodicityDict);
        }
        private IList<E.ImportProcess> GenerateOneTimeProcess(CreateCommand request)
        {
            return new List<E.ImportProcess>() {
                new E.ImportProcess(request.ProcessDate, request.Condition, request.Periodicity, request.Source,request.companyId)
            };
        }

        private IList<E.ImportProcess> GenerateCustomPeriodicityProcesses(CreateCommand request, int daysCount, int iteration)
        {
            var processes = new List<E.ImportProcess>();

            for (DateTime i = request.ProcessDate; i <= request.To; i = i.AddDays(iteration))
                processes.Add(new E.ImportProcess(i, i.AddDays(daysCount + 1), i, request.Condition, request.Periodicity, request.Source,request.companyId));

            return processes;
        }
        private IList<E.ImportProcess> GenerateMonthlyProcess(CreateCommand request)
        {
            var processes = new List<E.ImportProcess>();

            for (int i = 0; request.ProcessDate.AddMonths(i) <= request.To; i++)
            {
                var previousMonth = request.ProcessDate.AddMonths(i).AddMonths(-1);
                var from = new DateTime(previousMonth.Year, previousMonth.Month, 1);
                var to = from.AddMonths(1).AddDays(-1);

                processes.Add(new E.ImportProcess(request.ProcessDate.AddMonths(i), from, to, request.Condition, request.Periodicity, request.Source,request.companyId));
            }

            return processes;
        }
        //public async Task<Unit> Handle(CreateCommand request, CancellationToken cancellationToken)
        //{
        //    var process = new List<E.ImportProcess>();

        //    int daysCount;

        //    switch (request.Periodicity)
        //    {
        //        case Periodicity.OneTime:
        //            process.Add(new E.ImportProcess(request.ProcessDate, request.Condition, request.Periodicity, request.Source));
        //            break;
        //        case Periodicity.Daily:
        //            daysCount = await _periodicityDaysRepo.GetDaysCount(request.Periodicity);

        //            for (DateTime i = request.ProcessDate; i <= request.To; i = i.AddDays(1))
        //                process.Add(new E.ImportProcess(i, BuildWhere(i.AddDays(daysCount + 1), i), request.Periodicity, request.Source));
        //            break;
        //        case Periodicity.Weekly:
        //            daysCount = await _periodicityDaysRepo.GetDaysCount(request.Periodicity);

        //            for (DateTime i = request.ProcessDate; i <= request.To; i = i.AddDays(7))
        //                process.Add(new E.ImportProcess(i, BuildWhere(i.AddDays(daysCount + 1), i), request.Periodicity, request.Source));
        //            break;
        //        case Periodicity.Monthly:
        //            for (int i = 0; request.ProcessDate.AddMonths(i) <= request.To; i++)
        //            {
        //                var previousMonth = request.ProcessDate.AddMonths(i).AddMonths(-1);
        //                var from = new DateTime(previousMonth.Year, previousMonth.Month, 1);
        //                var to = from.AddMonths(1).AddDays(-1);

        //                process.Add(new E.ImportProcess(request.ProcessDate.AddMonths(i), BuildWhere(from, to), request.Periodicity, request.Source));
        //            }
        //            break;
        //        default:
        //            break;
        //    }

        //    await _repository.BulkInsertAsync(process, cancellationToken);

        //    return Unit.Value;
        //}
        //private string BuildWhere(DateTime from, DateTime to) =>
        //    $" WHERE SOCIEDAD_ID = '01AR' AND " +
        //    $"((fecha_emision_venta >= {from:yyyyMMdd} AND fecha_emision_venta <= {to:yyyyMMdd}) OR " +
        //    $"(fecha_cierre_venta >= {from:yyyyMMdd} AND fecha_cierre_venta <= {to:yyyyMMdd}))";
    }
}
