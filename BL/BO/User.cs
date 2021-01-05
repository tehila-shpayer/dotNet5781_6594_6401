﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{    public class User
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public AuthorizationManagement AuthorizationManagement { get; set; }
        public bool IsActive { get; set; }

        public override string ToString()
        {
            return this.ToStringProperty<User>();
        }

    }
}
