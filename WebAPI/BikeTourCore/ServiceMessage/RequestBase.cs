﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeTourCore.ServiceMessage
{
    public class RequestBase
    {
        public string LoginName { get; set; }
        public string Password { get; set; }
        public string SecurityToken { get; set; }
    }
}
