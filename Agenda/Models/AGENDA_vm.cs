using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Agenda.Models
{
    // http://www.asp.net/web-api/overview/data/using-web-api-with-entity-framework/part-5
    public partial class AGENDA_DTO 
    {
        public int Id { get; set; }
        public string name { get; set; }
        public string description { get; set; }

       
    }
}