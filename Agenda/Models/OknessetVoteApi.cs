using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Agenda.Models
{
    public class VoteOknessetVoteApi
    {
        public bool against_own_bill { get; set; }
        public string vote_time { get; set; }
        public string vote_title { get; set; }
        public bool against_coalition { get; set; }
        public string vote_type { get; set; }
        public bool against_party { get; set; }
        public string vote_url { get; set; }
        public string member { get; set; }
        public string party { get; set; }
        public string vote { get; set; }
        public string member_title { get; set; }
        public string member_url { get; set; }
        public bool against_opposition { get; set; }
    }

    public class AgendaOknessetVoteApi
    {
        public string name { get; set; }
        public double importance { get; set; }
        public object image { get; set; }
        public double score { get; set; }
        public string reasoning { get; set; }
        public string resource_uri { get; set; }
    }

    public class RootObjectOknessetVoteApi
    {
        public string full_text { get; set; }
        public int votes_count { get; set; }
        public int controversy { get; set; }
        public int id { get; set; }
        public int for_votes_count { get; set; }
        public List<VoteOknessetVoteApi> votes { get; set; }
        public int src_id { get; set; }
        public string title { get; set; }
        public int against_coalition { get; set; }
        public int against_party { get; set; }
        public string time_string { get; set; }
        public int against_opposition { get; set; }
        public int vote_number { get; set; }
        public int against_votes_count { get; set; }
        public int meeting_number { get; set; }
        public List<string> tags { get; set; }
        public double importance { get; set; }
        public string vote_type { get; set; }
        public string src_url { get; set; }
        public List<AgendaOknessetVoteApi> agendas { get; set; }
        public int against_own_bill { get; set; }
        public string summary { get; set; }
        public object full_text_url { get; set; }
        public string time { get; set; }
        public string resource_uri { get; set; }
    }
}