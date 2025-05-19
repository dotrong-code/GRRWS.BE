using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GRRWS.Application.Common.Validator.Abstract;
using GRRWS.Application.Common.Validator.AreaVali;
using GRRWS.Infrastructure.Common;
using GRRWS.Infrastructure.DTOs.Area;

namespace GRRWS.Application.Common.Validator.AreaVali
{
    public class UpdateAreaValidator : AreaValidator<UpdateAreaRequest>
    {
        public UpdateAreaValidator(UnitOfWork unitOfWork) : base(unitOfWork)
        {
            AddIdRules(request => request.Id);
            AddAreaNameRules(request => request.AreaName);
        }
    }
}