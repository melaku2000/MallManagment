using MallManagment.Models.Extenssions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MallManagment.Models.Enums
{
    public enum RowStatus
    {
        [StringValue("Pending")] Pending = 131,
        [StringValue("Approved")] Approved = 132,
        [StringValue("Canceled")] Canceled = 133,
    }
}
