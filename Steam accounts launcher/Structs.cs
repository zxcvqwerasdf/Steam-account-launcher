using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steam_accounts_launcher
{
    public static class Structs
    {
        public struct AccountSettings
        {
            public string name;
            public string login;
            public string steamDirectory;
        }
        public struct Settings
        {
            public List<AccountSettings> accounts;
        }
    }
}
