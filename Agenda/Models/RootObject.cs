using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Agenda.Models
{
    public class AgendaList
    {
        public string name { get; set; }
        public double importance { get; set; }
        public string image { get; set; }
        public double score { get; set; }
        public string reasoning { get; set; }
        public string public_owner_name { get; set; }
        public string resource_uri { get; set; }
    }

    public class Agendas
    {
        public List<AgendaList> agenda_list { get; set; }
        public object suggest_agendas { get; set; }
        public object formset { get; set; }
        public object suggested_agendas { get; set; }
        public bool suggest_agendas_login { get; set; }
    }

    public class Proposal
    {
        public string title { get; set; }
        public int id { get; set; }
        public int knesset_id { get; set; }
        public string source_url { get; set; }
        public string date { get; set; }
        public int proposal_id { get; set; }
        public string content_html { get; set; }
        public string resource_uri { get; set; }
    }

    public class RootObject
    {
        public Agendas agendas { get; set; }
        public string stage_date { get; set; }
        public List<string> tags { get; set; }
        public string absolute_url { get; set; }
        public object explanation { get; set; }
        public string title { get; set; }
        public List<Proposal> proposals { get; set; }
        public List<string> proposers { get; set; }
        public string slug { get; set; }
        public string full_title { get; set; }
        public object first_vote { get; set; }
        public object approval_vote { get; set; }
        public string resource_uri { get; set; }
        public string legal_code { get; set; }
        public string popular_name_slug { get; set; }
        public string popular_name { get; set; }
        public List<string> pre_votes { get; set; }
        public int id { get; set; }
        public string stage { get; set; }
    }
}