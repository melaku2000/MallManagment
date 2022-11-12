using MallManagment.Models.Extenssions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MallManagment.Models.Enums
{
    public enum MessageType
    {
        [StringValue("Notification")] Notification = 141,
        [StringValue("Attention")] Attention = 142,
        [StringValue("Payment reminder")] PaymentReminder = 143,
        [StringValue("Warrning")] Warrning = 144,
        [StringValue("Invoice")] Invoice = 145,
    }
}
