using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GRRWS.Application.Common.Validator.Abstract;
using GRRWS.Infrastructure.Common;
using GRRWS.Infrastructure.DTOs.Zone;

namespace GRRWS.Application.Common.Validator.Zone
{
    public class CreateZoneRequestValidator : ZoneValidator<CreateZoneRequest>
    {
        public CreateZoneRequestValidator(UnitOfWork unitOfWork) : base(unitOfWork)
        {
            AddZoneNameRules(request => request.ZoneName);
            AddAreaIdRules(request => request.AreaId);
        }
    }
}
