﻿using Microsoft.AspNetCore.Identity;
namespace LGDShop.DataAccess
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public bool IsAdmin { get; set; }
    }
}