using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Agenda.Models
{
    public partial class MK_SCORE_PARTY
    {
        public int IdMK_SCORE { get; set; }
        public int mk { get; set; }
        public string mkName { get; set; }
        public int party { get; set; }
        public string partyName { get; set; }
        public System.DateTime fromDate { get; set; }
        public System.DateTime toDate { get; set; }
        public int knessetNumber { get; set; }

      
        public decimal score { get; set; }
        public decimal volume { get; set; }

        public int agendaId { get; set; }
        public string agendaName { get; set; }
  
    }
}