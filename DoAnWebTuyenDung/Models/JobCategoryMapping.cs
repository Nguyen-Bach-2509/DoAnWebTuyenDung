//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DoAnWebTuyenDung.Models
{
    using System;
    using System.Collections.Generic;

    public partial class JobCategoryMapping
    {
        public int JobID { get; set; }
        public Job Job { get; set; }
        public int CategoryID { get; set; }
        public JobCategory JobCategory { get; set; }
    }
}