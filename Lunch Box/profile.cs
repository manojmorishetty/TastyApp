//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Lunch_Box
{
    using System;
    using System.Collections.Generic;
    
    public partial class profile
    {
        public int pId { get; set; }
        public int uid { get; set; }
        public string address { get; set; }
        public string city { get; set; }
        public string province { get; set; }
        public string country { get; set; }
        public string pic_url { get; set; }
    
        public virtual user user { get; set; }
    }
}
