﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Rodar.Service.Models
{
    public class Login
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
    }
}