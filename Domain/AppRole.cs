using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Domain
{
    public class AppRole:IdentityRole
    {
        public bool IsStatic { get; set; }
        
    }
}