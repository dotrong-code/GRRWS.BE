using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using GRRWS.Domain.Entities;
using GRRWS.Infrastructure.Common;
using GRRWS.Infrastructure.DTOs.Common.Message;

namespace GRRWS.Application.Common.Validator.Abstract
{
    public abstract class ZoneValidator<T> : AbstractValidator<T>
    {
        private readonly UnitOfWork _unitOfWork;

        protected ZoneValidator(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // Validation for ZoneName
        protected void AddZoneNameRules(Expression<Func<T, string>> zoneNameExpression)
        {
            RuleFor(zoneNameExpression)
                .NotEmpty().WithState(_ => ZoneErrorMessage.FieldIsEmpty("Zone name"))
                .MaximumLength(100).WithState(_ => ZoneErrorMessage.FieldTooLong("Zone name", 100));
        }

        // Validation for AreaId
        protected void AddAreaIdRules(Expression<Func<T, Guid>> areaIdExpression)
        {
            RuleFor(areaIdExpression)
                .NotEmpty().WithState(_ => ZoneErrorMessage.FieldIsEmpty("AreaId"))
                .MustAsync(async (areaId, cancellation) => await _unitOfWork.AreaRepository.GetByIdAsync(areaId) != null)
                .WithState(_ => ZoneErrorMessage.AreaNotExist());
        }

        // Validation for Id (for update requests)
        protected void AddIdRules(Expression<Func<T, Guid>> idExpression)
        {
            RuleFor(idExpression)
                .NotEmpty().WithState(_ => ZoneErrorMessage.FieldIsEmpty("Id"));
        }
    }

}
