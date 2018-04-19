using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Agenda.Models
{

    public class OknessetBill
    {
        public string href { get; set; }
        public string text { get; set; }
    }

    public class Collection1
    {
        public string voteDate { get; set; }
        public string billName { get; set; }
        public string ministerName { get; set; }
        public string ministerVote { get; set; }
        public string passed { get; set; }
        public string minister_id { get; set; }
        public OknessetBill oknessetBill { get; set; }
        public int index { get; set; }
        public string url { get; set; }
    }

    public class Results
    {
        public List<Collection1> collection1 { get; set; }
    }

    public class RootObjectVote
    {
        public string name { get; set; }
        public int count { get; set; }
        public string frequency { get; set; }
        public int version { get; set; }
        public bool newdata { get; set; }
        public string lastrunstatus { get; set; }
        public string thisversionstatus { get; set; }
        public string nextrun { get; set; }
        public string thisversionrun { get; set; }
        public Results results { get; set; }
    }

    //    public class IdVote
    //    {
    //        public string __invalid_name__T { get; set; }
    //}

    //public class UpdatedVote
    //    {
    //    public string __invalid_name__T { get; set; }
    //}

    //public class CategoryVote
    //    {
    //    public string scheme { get; set; }
    //    public string term { get; set; }
    //}

    //public class TitleVote
    //    {
    //    public string type { get; set; }
    //    public string __invalid_name__T { get; set; }
    //}

    //public class LinkVote
    //    {
    //    public string rel { get; set; }
    //    public string type { get; set; }
    //    public string href { get; set; }
    //}

    //public class NameVote
    //    {
    //    public string __invalid_name__T { get; set; }
    //}

    //public class EmailVote
    //    {
    //    public string __invalid_name__T { get; set; }
    //}

    //public class AuthorVote
    //    {
    //    public Name name { get; set; }
    //    public Email email { get; set; }
    //}

    //public class OpenSearchTotalResultsVote
    //    {
    //    public string __invalid_name__T { get; set; }
    //}

    //public class OpenSearchStartIndexVote
    //    {
    //    public string __invalid_name__T { get; set; }
    //}

    //public class TitleVotes
    //{
    //    public string type { get; set; }
    //    public string __invalid_name__T { get; set; }
    //}

    //public class ContentVotes
    //{
    //    public string type { get; set; }
    //    public string __invalid_name__T { get; set; }
    //}

    //public class GsxMeeting
    //{
    //    public string __invalid_name__T { get; set; }
    //}

    //public class GsxBill
    //{
    //    public string __invalid_name__T { get; set; }
    //}

    //public class GsxMinister
    //{
    //    public string __invalid_name__T { get; set; }
    //}

    //public class GsxVote
    //{
    //    public string __invalid_name__T { get; set; }
    //}

    //public class GsxOknesseturl2
    //{
    //    public string __invalid_name__T { get; set; }
    //}

    //public class GsxMinisteroknessetid
    //{
    //    public string __invalid_name__T { get; set; }
    //}

    //public class EntryVote
    //{
    //    public Id2 id { get; set; }
    //    public Updated2 updated { get; set; }
    //    public List<Category2> category { get; set; }
    //    public TitleVotes title { get; set; }
    //    public ContentVotes content { get; set; }
    //    public List<LinkVote> link { get; set; }
    //    public GsxMeeting __invalid_name__gsxMeeting { get; set; }
    //public GsxBill __invalid_name__gsxBill { get; set; }
    //    public GsxMinister __invalid_name__gsxMinister { get; set; }
    //    public GsxVote __invalid_name__gsxVote { get; set; }
    //    public GsxOknesseturl __invalid_name__gsxOknesseturl { get; set; }
    //    public GsxMinisteroknessetid __invalid_name__gsxMinisteroknessetid { get; set; }
    //}

    //public class FeedVote
    //    {
    //    public string xmlns { get; set; }
    //    public string __invalid_name__xmlnsOpenSearch { get; set; }
    //public string __invalid_name__xmlnsGsx { get; set; }
    //    public IdVote id { get; set; }
    //public UpdatedVote updated { get; set; }
    //public List<CategoryVote> category { get; set; }
    //public TitleVote title { get; set; }
    //public List<LinkVote> link { get; set; }
    //public List<AuthorVote> author { get; set; }
    //public OpenSearchTotalResultsVote __invalid_name__openSearchTotalResults { get; set; }
    //    public OpenSearchStartIndexVote __invalid_name__openSearchStartIndex { get; set; }
    //    public List<EntryVote> entry { get; set; }
    //}

    //public class RootObjectVote
    //{
    //    public string version { get; set; }
    //    public string encoding { get; set; }
    //    public FeedVote feed { get; set; }
    //}
}