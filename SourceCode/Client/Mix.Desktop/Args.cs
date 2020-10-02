using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mix.Desktop
{
    public class SignUpArgs
    {
        public string SessionId { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string VerificationCode { get; set; }
    }

    public class SignInArgs
    {
        public string UserName { get; set; }

        public string Password { get; set; }
    }
}