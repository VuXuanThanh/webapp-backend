using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApp.Core.Entities
{
    public class ResponseResult
    {
        public string devMsg { get; set; }
        public string userMsg { get; set; }
        public string errorCode { get; set; }
        public string moreInfor { get; set; }
        public string traceId { get; set; }
        public Object data { get; set; }
        public bool Success { get; set; } = true;
    }
}
