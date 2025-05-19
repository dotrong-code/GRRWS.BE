using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GRRWS.Application.Common.Validator.Abstract;
using GRRWS.Infrastructure.Common;
using GRRWS.Infrastructure.DTOs.Position;

namespace GRRWS.Application.Common.Validator.Position
{
    public class UpdatePositionRequestValidator : PositionValidator<UpdatePositionRequest>
    {
        public UpdatePositionRequestValidator(UnitOfWork unitOfWork) : base(unitOfWork)
        {
            AddIdRules(request => request.Id);
            AddIndexRules(request => request.Index);
            AddZoneIdRules(request => request.ZoneId);
        }
    }
}
