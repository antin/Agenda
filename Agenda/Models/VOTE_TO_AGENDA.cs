//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Agenda.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class VOTE_TO_AGENDA
    {
        public int Id { get; set; }
        public int vote { get; set; }
        public int agenda { get; set; }
        public decimal score { get; set; }
        public decimal volume { get; set; }
    
        public virtual AGENDA AGENDA1 { get; set; }
        public virtual VOTE VOTE1 { get; set; }
    }
}
