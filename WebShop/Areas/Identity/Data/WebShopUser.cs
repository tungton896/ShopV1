using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace WebShop.Areas.Identity.Data;

// Add profile data for application users by adding properties to the WebShopUser class
public class WebShopUser : IdentityUser
{
    public string? FullName { get; set; }
}

