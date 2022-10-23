using MallManagment.Shared.Extenssions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MallManagment.Shared.Enums
{
    public enum RoomStatus
    {
        [StringValue("Aviliable")] Aviliable = 111,
        [StringValue("On service")] OnService = 112,
        [StringValue("On repair")] OnRepair = 113,
        [StringValue("Out of service")] OutOfService = 114,
    }
}
