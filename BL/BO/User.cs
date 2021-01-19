using System;
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
        public string Email { get; set; }
        public String PhoneNumber { get; set; }
        public string Address { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String Picture { get; set; }

        public override string ToString()
        {
            return this.ToStringProperty<User>();
        }

    }
}
