using MallManagment.Models.Extenssions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MallManagment.Models.Enums
{
    public enum IdentificationType
    {
        [StringValue("Citizen ID")] CitizenId = 121,
        [StringValue("Driving License")] Employeer = 122,
        [StringValue("Personal Tin")] PersonalTin = 123,
        [StringValue("Passport")] Passport = 124,
        [StringValue("Yellow Card")] YellowCard = 125,
        [StringValue("Others")] Others = 126,
    }
}
