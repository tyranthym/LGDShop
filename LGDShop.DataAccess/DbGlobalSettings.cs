using System;
using System.Collections.Generic;
using System.Text;

namespace LGDShop.DataAccess
{
    public static class DbGlobalSettings
    {
        public static string ConnectionString { get; set; }     //provide database connection string everywhere
    }
}
