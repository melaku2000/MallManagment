using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MallManagment.Models.Dtos
{
    public class FileData
    {
        public string? UserId { get; set; }
        public string? FileBase64data { get; set; }
        public string? DataType { get; set; }
    }
}
