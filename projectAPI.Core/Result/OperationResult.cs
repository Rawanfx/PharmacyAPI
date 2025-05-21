using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace projectAPI.Result
{
    public class OperationResult
    {
        public bool success { get; set; }
        public HttpStatusCode statusCode { get; set; }
        public string message { get; set; }
    }
}
