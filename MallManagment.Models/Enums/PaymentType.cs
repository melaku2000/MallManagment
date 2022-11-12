using MallManagment.Models.Extenssions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MallManagment.Models.Enums
{
    public enum PaymentType
    {
        [StringValue("Monthly")] Monthly = 121,
        [StringValue("Quarterly")] Quarterly = 122,
        [StringValue("Half yearly")] HalfYearly = 123,
        [StringValue("Yearly")] Yearly = 124,
    }
}
