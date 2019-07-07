using System;
using System.Collections.Generic;
using System.Text;

namespace LGDShop.Domain.Constants
{
    /// <summary>
    /// claim type: ApiClaims.
    /// </summary>
    public static class AppPermissions
    {
        //super-admin
        public const string FullAccess = "api_full_access";

        //general
        public const string Readonly = "api_readonly";
        public const string Delete = "api_delete";

        //specific
        public const string EmployeesManage = "api_employees_manage";       //read, create, update and delete
        public const string EmployeesReadonly = "api_employees_readonly";   //read only
    }
}
