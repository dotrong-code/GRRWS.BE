using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRRWS.Domain.Enum
{
    public enum MachineActionType
    {
        StockOut,         // Retrieve device from stock
        StockIn,          // Return device to stock
        Installation,     // Confirm installation of replacement device
        WarrantySubmission // Submit device for warranty
    }
}
