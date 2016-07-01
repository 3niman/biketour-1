﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeTourCore.ServiceMessage
{
    public class ResponseBase
    {        
        public List<ErrorMessage> Log { get; set; }
    }

    public class ErrorMessage
    {
        public string FileName { get; set; }
        public bool Status { get; set; }
        public string Code { get; set; }
        public string Message { get; set; }
    }
}