using MallManagment.Models.Extenssions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MallManagment.Models.Enums
{
    public enum RoleType
    {
        [StringValue("Super admin")] SuperAdmin = 101,
        [StringValue("Finance")] Finance = 102,
        [StringValue("Operation Manager")] OperationManager = 103,
        [StringValue("Client")] Client = 104,
    }
}
