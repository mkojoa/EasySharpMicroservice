using System;
using System.Collections.Generic;
using System.Text;

namespace EasySharp.Core.Messages.Response
{
    public class ApiGenericResponse<TBody>
    {
        public int Status { get; set; }
        public TBody Data { get; set; }
        public string Message { get; set; } 
    }
}
