using MallManagment.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MallManagment.Models.Dtos
{
    public class ResponseDto<T> where T : class
    {
        public T? Model { get; set; }
        public string? Message { get; set; }
        public ResponseStatus Status { get; set; }
    }
}
