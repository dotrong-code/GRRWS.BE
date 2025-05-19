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
    public abstract class PositionValidator<T> : AbstractValidator<T>
    {
        private readonly UnitOfWork _unitOfWork;

        protected PositionValidator(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // Validation for Index
        protected void AddIndexRules(Expression<Func<T, int>> indexExpression)
        {
            RuleFor(indexExpression)
                .GreaterThan(0).WithState(_ => PositionErrorMessage.FieldInvalid("Index"));
        }

        // Validation for ZoneId
        protected void AddZoneIdRules(Expression<Func<T, Guid>> zoneIdExpression)
        {
            RuleFor(zoneIdExpression)
                .NotEmpty().WithState(_ => PositionErrorMessage.FieldIsEmpty("ZoneId"))
                .MustAsync(async (zoneId, cancellation) => await _unitOfWork.ZoneRepository.GetByIdAsync(zoneId) != null)
                .WithState(_ => PositionErrorMessage.ZoneNotExist());
        }

        // Validation for Id (for update requests)
        protected void AddIdRules(Expression<Func<T, Guid>> idExpression)
        {
            RuleFor(idExpression)
                .NotEmpty().WithState(_ => PositionErrorMessage.FieldIsEmpty("Id"));
        }
    }
}
