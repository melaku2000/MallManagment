using MallManagment.Shared.Extenssions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MallManagment.Shared.Enums
{
    public enum TokenType
    {
        [StringValue("Email confirmation")] EmailConfirmation = 101,
        [StringValue("Password reset")] PasswordReset = 101,
        [StringValue("Approve")] Approve = 101,
        
    }
}
