//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebApplication1.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class ctable
    {
        public int id { get; set; }
        public string name { get; set; }
        public string label { get; set; }
        public bool isLeaf { get; set; }
        public int firstLevelCatId { get; set; }
        public Nullable<int> lscSetId { get; set; }
        public bool variationCat { get; set; }
        public bool active { get; set; }
        public int Parentcatid { get; set; }
    }
}
