﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorasRaras.Domain.Contracts.Settings
{
    public class EmailSettings
    {
     public string Host { get; set; }
     public int Port { get; set; }
     public string Username { get; set; }
     public string Password { get; set; }
     public bool EnableSsl { get; set; }
    }
}
