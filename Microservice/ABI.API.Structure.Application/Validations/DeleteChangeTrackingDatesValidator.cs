using ABI.API.Structure.ACL.Truck.Repositories.Interfaces;
using ABI.API.Structure.Application.Commands.RequestsTray;
using ABI.API.Structure.Application.DTO;
using ABI.API.Structure.Application.Exceptions;
using ABI.API.Structure.Application.Interfaces;
using ABI.API.Structure.Domain.Entities;
using ABI.API.Structure.Infrastructure.Repositories.Interfaces;
using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ABI.API.Structure.Application.Validations
{
    public class DeleteChangeTrackingDatesValidator<TCommand> : AbstractValidator<TCommand> where TCommand : IDeleteChange
    {
        private readonly IChangeTrackingRepository _changeTrackingRepo;
        private readonly IVersionedRepository _versionedRepository;
        private IList<GenericValueDescriptionDto> _changeTracking;
        private List<ChangeTracking> _changeTrackingList;

        public DeleteChangeTrackingDatesValidator(IChangeTrackingRepository changeTrackingRepo, IVersionedRepository versionedRepository)
        {
            _changeTrackingRepo = changeTrackingRepo;
            _versionedRepository = versionedRepository;
            _changeTracking = new List<GenericValueDescriptionDto>();
            _changeTrackingList = new List<ChangeTracking>();

            RuleFor(command => command)
                .Must(ValidDateChange)
                .OnFailure(f => throw new ChangeTrackingDateException(_changeTracking));
        }

        protected override bool PreValidate(ValidationContext<TCommand> context, ValidationResult result)
        {
            var preValidate = base.PreValidate(context, result);

            if (!preValidate)
                return false;

            int commandStructureId = 0;
            DateTimeOffset validityFrom = DateTimeOffset.UtcNow;
            var typeCommand = context.InstanceToValidate.GetType();

            if (typeCommand.Name == typeof(DeleteChangeCommand).Name)
            {
                var commandDelete = context.InstanceToValidate as DeleteChangeCommand;
                var changeData = _changeTrackingRepo.GetById(commandDelete.Id).GetAwaiter().GetResult();
                commandStructureId = changeData.IdStructure;
                validityFrom = changeData.ValidityFrom;
            }
            else if (typeCommand.Name == typeof(DeleteChangeGroupCommand).Name)
            {
                var commandDelete = context.InstanceToValidate as DeleteChangeGroupCommand;
                commandStructureId = commandDelete.structureId;
                validityFrom = commandDelete.validity;
            }

            var extChangeTrackingResults = _versionedRepository.GetVersionByIdsStructureValidity(new List<Int32>(), validityFrom, DateTimeOffset.MaxValue).GetAwaiter().GetResult();
            
            var changeTrackingResults = _changeTrackingRepo.GetByStructureId(commandStructureId).GetAwaiter().GetResult();
            changeTrackingResults = changeTrackingResults
                                .Where(chg => chg.ValidityFrom > validityFrom)
                                .OrderBy(x => x.ValidityFrom)
                                .ToList();

            List<int> changeIds = new List<int>();
            foreach (var item in extChangeTrackingResults)
            {
                //DISTINTO A RECHAZADO O CANCELADO
                if (item.VersionedStatus.Id == 5 || item.VersionedStatus.Id == 6 )
                {
                    var changeS = changeTrackingResults
                    .Where(x => x.IdStructure == item.StructureId)
                    .Where(f => f.ValidityFrom == item.Validity)
                    .Select(x => x.Id)
                    .ToList();
                    changeIds.AddRange(changeS);
                }
            }
            _changeTrackingList = changeTrackingResults.Where(x => !changeIds.Contains(x.Id)).ToList();

            return true;
        }

        private bool ValidDateChange(TCommand request)
        {
            if (_changeTrackingList.Count > 0)
            {
                var groupedChanges = _changeTrackingList.GroupBy(x => x.ValidityFrom).ToList();
                foreach (var change in groupedChanges)
                {
                    var itemChange = new GenericValueDescriptionDto();
                    itemChange.Value = change.Key.ToString("s") + change.Key.ToString("zzz");
                    var descriptionsChanges = _changeTrackingList.Where(x => x.ValidityFrom == change.Key).ToList();
                    foreach (var itemDescription in descriptionsChanges)
                    {
                        if (string.IsNullOrEmpty(itemChange.Descripcion))
                            itemChange.Descripcion = itemDescription.KindOfChange();
                        else if (!itemChange.Descripcion.Contains(itemDescription.KindOfChange()))
                            itemChange.Descripcion += " - " + itemDescription.KindOfChange();
                    }
                    _changeTracking.Add(itemChange);
                }
                return false;
            }

            return true;
        }
    }
}
