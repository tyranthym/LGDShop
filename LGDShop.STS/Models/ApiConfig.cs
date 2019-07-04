using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StsServerIdentity.Models
{
    /// <summary>
    /// web-api scopes
    /// </summary>
    public static class ApiScopes
    {
        public const string Api = IdentityServerConfig.ApiName;
        public const string General = "api.general";
    }

    /// <summary>
    /// web-api claims
    /// </summary>
    public static class ApiClaims
    {
        public const string General = "api.general";
    }

}
