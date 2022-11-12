using MallManagment.Models.Extenssions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MallManagment.Models.Enums
{
    public enum UserStatus
    {
        [StringValue("Active")] Active = 111,
        [StringValue("Disabled")] Disabled = 112,
        [StringValue("Deleted")] Deleted = 113,
    }
}
