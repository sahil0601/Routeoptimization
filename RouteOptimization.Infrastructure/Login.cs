//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RouteOptimization.Infrastructure
{
    using System;
    using System.Collections.Generic;
    
    public partial class Login
    {
        public int LoginID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public Nullable<int> UserID { get; set; }
        public Nullable<int> UserTypeID { get; set; }
    
        public virtual User User { get; set; }
        public virtual UserType UserType { get; set; }
    }
}