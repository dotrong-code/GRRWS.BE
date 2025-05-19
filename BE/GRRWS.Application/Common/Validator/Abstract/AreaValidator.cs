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
    public abstract class AreaValidator<T> : AbstractValidator<T>
    {
        private readonly UnitOfWork _unitOfWork;

        protected AreaValidator(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // Validation for AreaName
        protected void AddAreaNameRules(Expression<Func<T, string>> areaNameExpression)
        {
            RuleFor(areaNameExpression)
                .NotEmpty().WithState(_ => AreaErrorMessage.FieldIsEmpty("Area name"));
        }

        // Validation for Id (for update requests)
        protected void AddIdRules(Expression<Func<T, Guid>> idExpression)
        {
            RuleFor(idExpression)
                .NotEmpty().WithState(_ => AreaErrorMessage.FieldIsEmpty("Id"));
        }
    }
}
