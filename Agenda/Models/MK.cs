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
    
    public partial class MK
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MK()
        {
            this.MK_SCORE = new HashSet<MK_SCORE>();
            this.MK_TO_PARTY = new HashSet<MK_TO_PARTY>();
            this.VOTE = new HashSet<VOTE>();
        }
    
        public int Id { get; set; }
        public string mk_name { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MK_SCORE> MK_SCORE { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MK_TO_PARTY> MK_TO_PARTY { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<VOTE> VOTE { get; set; }
    }
}