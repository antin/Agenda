using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Agenda.Models
{
    public class OknessetBillVSB
    {
        public string href { get; set; }
        public string text { get; set; }
    }

    public class CollectionBill
    {
        public string voteDate { get; set; }
        public string billName { get; set; }
        public OknessetBillVSB oknessetBill { get; set; }
        public string passed { get; set; }
        public int index { get; set; }
        public string url { get; set; }
    }

    public class ResultsBill
    {
        public List<CollectionBill> collection1 { get; set; }
    }

    public class RootObjectBill
    {
        public string name { get; set; }
        public int count { get; set; }
        public string frequency { get; set; }
        public int version { get; set; }
        public bool newdata { get; set; }
        public string lastrunstatus { get; set; }
        public string thisversionstatus { get; set; }
        public string thisversionrun { get; set; }
        public ResultsBill results { get; set; }
    }
}