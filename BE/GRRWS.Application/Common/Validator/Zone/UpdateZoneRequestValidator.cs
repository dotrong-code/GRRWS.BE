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
    public class UpdateZoneRequestValidator : ZoneValidator<UpdateZoneRequest>
    {
        public UpdateZoneRequestValidator(UnitOfWork unitOfWork) : base(unitOfWork)
        {
            AddIdRules(request => request.Id);
            AddZoneNameRules(request => request.ZoneName);
            AddAreaIdRules(request => request.AreaId);
        }
    }
}
