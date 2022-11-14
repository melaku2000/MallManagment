using MallManagment.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MallManagment.Models.Entities
{
    public class UserToken :BaseModel
    {
        [ForeignKey(nameof(Admin))]
        public string? AdminId { get; set; }
        public string? Token { get; set; }
        public TokenType TokenType { get; set; }
        public DateTime TokenExpireTime { get; set; }
        // NAVIGATION
        public virtual Adminstrator Admin{ get; set; } = null!;
    }
}
