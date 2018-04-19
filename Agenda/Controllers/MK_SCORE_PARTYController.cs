using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Agenda.Models;
using System.Web.Mvc;
using System.IO;
using Newtonsoft.Json;
using System.Globalization;
using Elmah;

namespace Agenda.Controllers
{
    public class MK_SCORE_PARTYController : ApiController
    {
        static Dictionary<string, string> cashRet = new Dictionary<string, string>();
        //private AgendaEntities db = new AgendaEntities();


        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <returns></returns>
        /// <see cref="http://www.codeproject.com/Tips/397574/Use-Csharp-to-get-JSON-Data-from-the-Web-and-Map-i"/>
        private static T _download_serialized_json_data<T>(string url) where T : new()
        {
            using (var w = new WebClient())
            {
                var json_data = string.Empty;
                var context = System.Web.HttpContext.Current;
                int errorId = 503;
                

                for (int i = 0; i < 30 && errorId == 503; i++)
                {
                    // attempt to download JSON data as a string
                    try
                    {
                        w.Headers[HttpRequestHeader.Accept] = "text/html, image/png, image/jpeg, image/gif, */*;q=0.1";
                        w.Headers[HttpRequestHeader.UserAgent] = "Mozilla/5.0 (Windows; U; Windows NT 6.1; de; rv:1.9.2.12) Gecko/20101026 Firefox/3.6.12";

                        //ErrorLog.GetDefault(context).Log(new Error(new Exception("before_download_serialized_json_data")));
                        if (!cashRet.ContainsKey(url))
                        {
                            json_data = w.DownloadString(url);
                            cashRet.Add(url, json_data);
                        }
                        else
                        {
                            cashRet.TryGetValue(url, out json_data);
                        }
                        //ErrorLog.GetDefault(context).Log(new Error(new Exception(json_data)));
                        errorId = 0;
                    }
                    catch (WebException e)
                    {
                        ErrorLog.GetDefault(context).Log(new Error(e));
                        errorId = 503;
                    }
                    catch (Exception e) {
                        errorId = -1;
                        ErrorLog.GetDefault(context).Log(new Error(e));
                    }

                    // if string with JSON data is not empty, deserialize it to class and return its instance 
                    //dynamic jsonObj = JsonConvert.DeserializeObject(json_data);
                    //string a = jsonObj.feed.entry[0]["gsx$Oknesseturl"]["$t"];
                }
                return !string.IsNullOrEmpty(json_data) ? JsonConvert.DeserializeObject<T>(json_data) : new T();
            }
        }

        public void ImportBillVotesByAgenda()
        {
            cashRet = new Dictionary<string, string>();
            var context = System.Web.HttpContext.Current;
            ErrorLog.GetDefault(context).Log(new Error(new Exception(" 1. read all bills from vaadat sarim")));
            //https://spreadsheets.google.com/feeds/list/1YoDf3KRRPgz8BpBPqnhQVD_AexqfJpSpOULDvMMAt3s/od6/public/values?alt=json
            //https://oknesset.org/api/v2/bill/7818/
            // 1. read all bills from vaadat sarim
            //var bills = _download_serialized_json_data<RootObjectBill>("https://spreadsheets.google.com/feeds/list/1KNFehdsumR-GVq4hlHUC7h2lfGwFKk-7pwluwdykL-w/od6/public/values?alt=json");
            var bills = _download_serialized_json_data<RootObjectBill>("https://www.kimonolabs.com/api/bvy1dd1q?apikey=6aa9923ce34e4dc4b5643059018dac67");
            //var votes = _download_serialized_json_data<RootObjectVote>("https://spreadsheets.google.com/feeds/list/1KNFehdsumR-GVq4hlHUC7h2lfGwFKk-7pwluwdykL-w/3/public/values?alt=json");
            var votes = _download_serialized_json_data<RootObjectVote>("https://www.kimonolabs.com/api/bksr4yfq?apikey=6aa9923ce34e4dc4b5643059018dac67");
            ErrorLog.GetDefault(context).Log(new Error(new Exception("2. for every valid connection to oknesset")));
            // 2. for every valid connection to oknesset
            //http://stackoverflow.com/questions/23478505/json-to-c-sharp-object-with-google-spreadsheet
            //var billsInOknesset = bills.feed.entry.Where(b => (b.gsxOknesseturl != null && !string.IsNullOrEmpty(b.gsxOknesseturl.t)));
            var votesInOknesset = votes.results.collection1.Where(b => (b.oknessetBill != null && !string.IsNullOrEmpty(b.oknessetBill.text)));
            ErrorLog.GetDefault(context).Log(new Error(new Exception(" a. update bill,vote tables")));
            //      a. update bill,vote tables
            updateBills(bills);
            ErrorLog.GetDefault(context).Log(new Error(new Exception("after updateBills")));
            updateVotes(votesInOknesset);
            ErrorLog.GetDefault(context).Log(new Error(new Exception(" generate Shadow Vote From Committee vote")));
            // generate Shadow Vote From Committee vote To the Agenda using the bills table
            // for any bill without a vote for mk generate one 
            
            generateShadowVoteFromCommitteeToAgenda();
            //      b. add/update the data for score on every bill for any agenda
            ErrorLog.GetDefault(context).Log(new Error(new Exception(" updateVoteToAgenda")));
            updateVoteToAgenda();
            // 3. recalculate the general score of every agenda 
            //      a. calc for known votes sammer 2015
            DateTime from = new DateTime(2015,5,3);
            DateTime to = new DateTime(2015,8,2);
            recalculate(from,to);
            //      b. recalculate all missing vote by clone data from the bill tab


        }

        private void updateBills(RootObjectBill billsInOknesset)
        {
            using (AgendaEntities db = new AgendaEntities())
            {
                var context = System.Web.HttpContext.Current;
                //ErrorLog.GetDefault(context).Log(new Error(new Exception("updateBills-billsInOknesset.results.collection1.Where")));
                //foreach (CollectionBill e in billsInOknesset.results.collection1.Where(en => (en.oknessetBill != null && !string.IsNullOrEmpty(en.oknessetBill.text))).ToList())
                foreach (CollectionBill e in billsInOknesset.results.collection1.Where(en => ( !string.IsNullOrEmpty(en.billName))).ToList())
                {
                    //ErrorLog.GetDefault(context).Log(new Error(new Exception("updateBills-BILL.Where")));
                    //BILL bill = db.BILL.Where(b => (e.oknessetBill.text == b.oknesset_url)).FirstOrDefault();
                    BILL bill = db.BILL.Where(b => (e.billName == b.name)).FirstOrDefault();

                    //ErrorLog.GetDefault(context).Log(new Error(new Exception("after-BILL.Where")));

                    if (bill == null || bill.Id == 0)
                    {
                        //ErrorLog.GetDefault(context).Log(new Error(new Exception("BILL add")));
                        bill = new BILL();
                        bill.oknesset_url = e.oknessetBill.text;
                        bill.name = e.billName;
                        string voteDate = e.voteDate;
                        //ErrorLog.GetDefault(context).Log(new Error(new Exception("before bill.date, voteDate:" + voteDate)));
                        if (!string.IsNullOrEmpty(voteDate) && voteDate.Length > 6 && voteDate.Contains('/'))
                        {
                            voteDate = voteDate.Split('/')[0].Length == 1 ? "0" + voteDate : voteDate;
                            //ErrorLog.GetDefault(context).Log(new Error(new Exception("before ParseExact, voteDate:" + voteDate)));
                            voteDate = voteDate.Split('/')[1].Length == 1 ? voteDate.Split('/')[0] + "/0" + voteDate.Split('/')[1] + "/" + voteDate.Split('/')[2] : voteDate;
                            //ErrorLog.GetDefault(context).Log(new Error(new Exception("before ParseExact, voteDate:" + voteDate)));
                            bill.date = DateTime.ParseExact(voteDate, "d", CultureInfo.InvariantCulture);
                        }
                        else
                        {
                            continue;
                        }
                        //ErrorLog.GetDefault(context).Log(new Error(new Exception("before bill.committee_vote")));

                        if (e.passed == "בעד" || e.passed == "׳‘׳¢׳“" || e.passed == "×‘×¢×“")
                            bill.committee_vote = 1;
                        else if (e.passed == "נגד" || e.passed == "׳ ׳’׳“" || e.passed == "× ×ž× ×¢")
                            bill.committee_vote = 2;
                        else // נמנע
                            bill.committee_vote = 3;

                        ErrorLog.GetDefault(context).Log(new Error(new Exception("e.passed:" + e.passed)));
                        ErrorLog.GetDefault(context).Log(new Error(new Exception("bill.committee_vote:" + bill.committee_vote)));
                        db.BILL.Add(bill);
                        db.SaveChanges();
                        //db.Database.Connection.
                        //bill = db.BILL.Where(b => (b.oknesset_url == vote_item.oknessetBill.text)).FirstOrDefault();
                    }
                    else
                    {
                        //ErrorLog.GetDefault(context).Log(new Error(new Exception("bill != null")));
                        bill.name = e.billName;
                        //ErrorLog.GetDefault(context).Log(new Error(new Exception("before bill.committee_vote")));
                        if (e.passed == "בעד" || e.passed == "׳‘׳¢׳“" || e.passed == "×‘×¢×“")
                            bill.committee_vote = 1;
                        else if (e.passed == "נגד" || e.passed == "׳ ׳’׳“" || e.passed == "× ×ž× ×¢")
                            bill.committee_vote = 2;
                        else // נמנע
                            bill.committee_vote = 3;

                        ErrorLog.GetDefault(context).Log(new Error(new Exception("2-e.passed:" + e.passed)));
                        ErrorLog.GetDefault(context).Log(new Error(new Exception("2-bill.committee_vote:" + bill.committee_vote)));

                        db.SaveChanges();
                    }
                }
            }
        }

        /// <summary>
        ///  recalculate the general score of every agenda 
        /// </summary>
        private void recalculate(DateTime from, DateTime to)
        {
            using (AgendaEntities db = new AgendaEntities())
            {
               foreach (AGENDA a in db.AGENDA)
                {
                    recalculate(a.Id, from, to);
                }
            }

        }
        /// <summary>
        ///  recalculate the general score of every agenda 
        /// </summary>
        private void recalculate(int agendaId, DateTime from, DateTime to)
        {
            // foreach VOTE between from - to
            //   (filter/update/delete and add) by agenda in VOTE_TO_AGENDA
            //      add score,volume/(num of all vote)*100, base on vote in VOTE to the relevant mk in MK_SCORE 

            using (AgendaEntities db = new AgendaEntities())
            {
                // foreach VOTE between from - to
                //   (filter/update/delete and add) by agenda in VOTE_TO_AGENDA
                var mksc = db.MK_SCORE.Where(mks => (mks.fromDate == from && mks.toDate == to && mks.agendaId == agendaId));
                foreach (MK_SCORE m in mksc.ToList())
                {
                    db.MK_SCORE.Remove(m);
                    db.SaveChanges();
                }
                //      add score,volume/(num of all vote)*100, base on vote in VOTE to the relevant mk in MK_SCORE 
                var vote = db.VOTE.Where(v => (v.meeting >= from && v.meeting <= to));
                foreach (VOTE v in vote.ToList())
                {
                    VOTE_TO_AGENDA v2a = v.VOTE_TO_AGENDA.Where(v_to_a => (v_to_a.agenda == agendaId && v_to_a.vote == v.Id)).FirstOrDefault();
                    List<MK_SCORE> ms_new_lst = db.MK_SCORE.Where(mks => (mks.fromDate == from && mks.toDate == to && mks.mk == v.mk && mks.agendaId == agendaId)).ToList();
                    if (v2a != null)
                    {
                        if (ms_new_lst != null && ms_new_lst.Count() > 0)
                        {
                            foreach (MK_SCORE m in ms_new_lst.ToList())
                            {
                                if (v.vote1 == 1)
                                    m.score += v2a.score;
                                else if (v.vote1 == 2)
                                    m.score -= v2a.score;
                                else
                                {
                                    // m.score -= v2a.score;
                                }
                                m.volume += v2a.volume;
                                db.SaveChanges();
                            }
                        }
                        else
                        {
                            MK_SCORE ms_new = new MK_SCORE();
                            ms_new.mk = v.mk.Value;
                            ms_new.score = v2a.score;
                            if (v.vote1 == 1)
                                ms_new.score  = v2a.score;
                            else if (v.vote1 == 2)
                                ms_new.score = -1 * v2a.score;
                            else
                            {
                                // m.score -= v2a.score;
                                ms_new.score = 0;
                            }
                            ms_new.volume = v2a.volume;
                            ms_new.fromDate = from;
                            ms_new.toDate = to;
                            ms_new.agendaId = agendaId;
                            db.MK_SCORE.Add(ms_new);
                            db.SaveChanges();
                        }
                    }
                }
                // normilazed by number of votes
                var ms_new_lstn = db.MK_SCORE.Where(mks => (mks.fromDate == from 
                    && mks.toDate == to && 
                    mks.agendaId == agendaId));

                int votes2Agenda = db.VOTE_TO_AGENDA.Where(v2a => (v2a.agenda == agendaId)).Count() / 12; // div by number of mk

                if (ms_new_lstn != null)
                {
                    foreach (MK_SCORE m in ms_new_lstn.ToList())
                    {
                        m.score = (Decimal)System.Data.SqlTypes.SqlDecimal.ConvertToPrecScale(m.score / votes2Agenda, 18, 3);
                        m.volume = (Decimal)System.Data.SqlTypes.SqlDecimal.ConvertToPrecScale(m.volume / votes2Agenda, 18, 3);
                        db.SaveChanges();
                    }
                }
            }
        }
        /// <summary>
        /// find in oknesset the relevant vote and update score etc...
        /// </summary>
        private void updateVoteToAgenda()
        {
            //      b. add/update the data for score on every bill for any agenda
            using (AgendaEntities dbv = new AgendaEntities())
            {
                var context = System.Web.HttpContext.Current;

                foreach (VOTE v in dbv.VOTE)
                {
                    if (v.BILL1.oknesset_url.Contains("https://oknesset.org/bill/"))
                    {
                        //ErrorLog.GetDefault(context).Log(new Error(new Exception("updateVoteToAgenda-1-v.BILL1.oknesset_url:" + v.BILL1.oknesset_url)));

                        string billOknessetId = v.BILL1.oknesset_url.Replace("https://oknesset.org/bill/", "").Replace("/", "");


                        var votesToAgenda = _download_serialized_json_data<RootObjectOknessetBill>("https://oknesset.org/api/v2/bill/" + billOknessetId);
                        if (votesToAgenda.agendas != null && votesToAgenda.agendas.agenda_list != null)
                        {
                            foreach (OknessetBillAgendaList a in votesToAgenda.agendas.agenda_list.ToList())
                            {
                                using (AgendaEntities db = new AgendaEntities())
                                {
                                    // find if vote already updated
                                    //db.Entry(aGENDA).State = EntityState.Modified;
                                    //db.SaveChanges();
                                    int agnId = int.Parse(a.resource_uri.Replace("api/v2/agenda/", "").Replace("/", ""));
                                    // check if agenda exist in the db, else add it
                                    if (db.AGENDA.Where(a1 => a1.Id == agnId).Count() == 0)
                                        ErrorLog.GetDefault(context).Log(new Error(new Exception("agenda missing, id = " + agnId)));

                                    VOTE_TO_AGENDA v2a = db.VOTE_TO_AGENDA.Where(va => va.agenda == agnId && va.vote == v.Id).FirstOrDefault();
                                    if (v2a == null)
                                    {
                                        v2a = new VOTE_TO_AGENDA();
                                        v2a.vote = v.Id;
                                        v2a.agenda = agnId;
                                        v2a.score = (decimal)a.score;
                                        v2a.volume = (decimal)a.importance;
                                        db.VOTE_TO_AGENDA.Add(v2a);
                                        db.SaveChanges();
                                    }
                                    else
                                    {
                                        v2a.vote = v.Id;
                                        v2a.agenda = agnId;
                                        v2a.score = (decimal)a.score;
                                        v2a.volume = (decimal)a.importance;
                                        //db.VOTE_TO_AGENDA.e(v2a);
                                        db.SaveChanges();
                                    }
                                }
                            }
                        }
                        else
                            ErrorLog.GetDefault(context).Log(new Error(new Exception("(votesToAgenda.agendas  == null")));
                    }

                    // do the same to a vote
                    if (v.BILL1.oknesset_url.Contains("https://oknesset.org/vote/"))
                    {
                        string billOknessetId = v.BILL1.oknesset_url.Replace("https://oknesset.org/vote/", "").Replace("/", "");

                        //ErrorLog.GetDefault(context).Log(new Error(new Exception("updateVoteToAgenda-2-v.BILL1.oknesset_url:" + v.BILL1.oknesset_url)));


                        var votesToAgenda = _download_serialized_json_data<RootObjectOknessetVoteApi>("https://oknesset.org/api/v2/vote/" + billOknessetId);
                        if (votesToAgenda.agendas != null )
                        {
                            foreach (AgendaOknessetVoteApi a in votesToAgenda.agendas.ToList())
                            {
                                using (AgendaEntities db = new AgendaEntities())
                                {
                                    // find if vote already updated
                                    //db.Entry(aGENDA).State = EntityState.Modified;
                                    //db.SaveChanges();
                                    int agnId = int.Parse(a.resource_uri.Replace("api/v2/agenda/", "").Replace("/", ""));
                                    // check if agenda exist in the db, else add it
                                    if (db.AGENDA.Where(a1 => a1.Id == agnId).Count() == 0)
                                        ErrorLog.GetDefault(context).Log(new Error(new Exception("agenda2 missing, id = " + agnId)));

                                    VOTE_TO_AGENDA v2a = db.VOTE_TO_AGENDA.Where(va => va.agenda == agnId && va.vote == v.Id).FirstOrDefault();
                                    if (v2a == null)
                                    {
                                        v2a = new VOTE_TO_AGENDA();
                                        v2a.vote = v.Id;
                                        v2a.agenda = agnId;
                                        v2a.score = (decimal)a.score;
                                        v2a.volume = (decimal)a.importance;
                                        db.VOTE_TO_AGENDA.Add(v2a);
                                        db.SaveChanges();
                                    }
                                    else
                                    {
                                        v2a.vote = v.Id;
                                        v2a.agenda = agnId;
                                        v2a.score = (decimal)a.score;
                                        v2a.volume = (decimal)a.importance;
                                        //db.VOTE_TO_AGENDA.e(v2a);
                                        db.SaveChanges();
                                    }
                                }
                            }
                        }
                        else
                            ErrorLog.GetDefault(context).Log(new Error(new Exception("2-(votesToAgenda.agendas  == null")));
                    }
                }
            }
        }

        /// <summary>
        /// generate Shadow Vote From Committee vote To the Agenda using the bills table
        /// for any bill without a vote for mk generate one 
        /// </summary>
        private void generateShadowVoteFromCommitteeToAgenda()
        {
            var context = System.Web.HttpContext.Current;
            //      b. add/update the data for score on every bill for any agenda
            using (AgendaEntities db = new AgendaEntities())
            {
                try
                { 
                    List<BILL> existBills4AllMks = (from b2d in db.BILL
                                     join v in db.VOTE
                                             on b2d.Id equals v.bill.Value
                                     join m in db.MK
                                            on v.mk.Value equals m.Id
                                     where !string.IsNullOrEmpty(b2d.oknesset_url)
                                     select b2d).ToList();

                    foreach (BILL b in db.BILL.ToList())
                    {
                    

                        foreach (MK m in db.MK.ToList())
                        {
                            //ErrorLog.GetDefault(context).Log(new Error(new Exception(" before existBills4AllMks.Contains,m.Id:" + m.Id)));


                            // if bill has votes for all ms continue
                            if (existBills4AllMks.Contains(b) && b.VOTE.Where(v_exist => v_exist.mk == m.Id).Count() > 0)
                                continue;

                            //ErrorLog.GetDefault(context).Log(new Error(new Exception(" after existBills4AllMks.Contains")));


                            VOTE v = new VOTE();
                            v.meeting = b.date;
                            v.bill = b.Id;
                            v.mk = m.Id;
                            if (b.committee_vote.HasValue)
                                v.vote1 = b.committee_vote.Value;
                            v.source = 2; // committee_vote
                            db.VOTE.Add(v);
                            db.SaveChanges();
                        }

                        // find bills which at least one mk missing a vote for it
                        //var existVotes = db.BILL.Where(b2d => (!string.IsNullOrEmpty(b2d.oknesset_url && )));

                        //foreach (OknessetBillAgendaList a in votesToAgenda.agendas.agenda_list.ToList())
                        //{
                        //    using (AgendaEntities db = new AgendaEntities())
                        //    {
                        //        // find if vote already updated
                        //        //db.Entry(aGENDA).State = EntityState.Modified;
                        //        //db.SaveChanges();
                        //        int agnId = int.Parse(a.resource_uri.Replace("api/v2/agenda/", "").Replace("/", ""));
                        //        VOTE_TO_AGENDA v2a = db.VOTE_TO_AGENDA.Where(va => va.agenda == agnId && va.vote == v.Id).FirstOrDefault();
                        //        if (v2a == null)
                        //        {
                        //            v2a = new VOTE_TO_AGENDA();
                        //            v2a.vote = v.Id;
                        //            v2a.agenda = agnId;
                        //            v2a.score = (decimal)a.score;
                        //            v2a.volume = (decimal)a.importance;
                        //            db.VOTE_TO_AGENDA.Add(v2a);
                        //            db.SaveChanges();
                        //        }
                        //        else
                        //        {
                        //            v2a.vote = v.Id;
                        //            v2a.agenda = agnId;
                        //            v2a.score = (decimal)a.score;
                        //            v2a.volume = (decimal)a.importance;
                        //            //db.VOTE_TO_AGENDA.e(v2a);
                        //            db.SaveChanges();
                        //        }
                        //    }
                        //}
                    }
                }
                catch (Exception e)
                {
                   
                    ErrorLog.GetDefault(context).Log(new Error(e));
                }
            }
        }

        private void updateVotes(IEnumerable<Collection1> v)
        {
            using (AgendaEntities db = new AgendaEntities())
            {
                var context = System.Web.HttpContext.Current;
                //ErrorLog.GetDefault(context).Log(new Error(new Exception("updateVotes-before Collection1 vote_item")));
                foreach (Collection1 vote_item in v)
                {
                    if (string.IsNullOrEmpty(vote_item.oknessetBill.text))
                    {
                         throw new ArgumentNullException();
                    }
                    // find if bill exist, if so find it and get the id, else create it
                    //int billOknessetId = int.Parse(vote_item.oknessetBill.text.Replace("https://oknesset.org/bill/","").Replace("/",""));
                    BILL bill = db.BILL.Where(b => (b.oknesset_url == vote_item.oknessetBill.text)).FirstOrDefault();
                    int billId = (bill != null?bill.Id : 0);
                    DateTime voteDate = DateTime.ParseExact(vote_item.voteDate, "MM/dd/yyyy", CultureInfo.InvariantCulture);
                    VOTE vote = db.VOTE.Where(b => (b.bill.Value == billId && b.meeting == voteDate)).FirstOrDefault();

                    if(bill == null || bill.Id == 0)
                    {
                        bill = new BILL();
                        bill.name = vote_item.billName;
                        bill.date = voteDate;
                        bill.oknesset_url = vote_item.oknessetBill.text;
                        //ErrorLog.GetDefault(context).Log(new Error(new Exception("updateVotes-before BILL.Add")));
                        db.BILL.Add(bill);
                        db.SaveChanges();
                        //db.Database.Connection.
                        //bill = db.BILL.Where(b => (b.oknesset_url == vote_item.oknessetBill.text)).FirstOrDefault();
                    }

                    if (vote == null)
                    {
                        VOTE newVote = new VOTE();
                        //newVote.meeting = DateTime.Parse(vote_item.voteDate);// 06/07/2015
                        newVote.meeting = voteDate;
                        newVote.bill = bill.Id;
                        newVote.mk = int.Parse(vote_item.minister_id);
                        newVote.source = 1; // from public vote
                        // 
                        if (vote_item.ministerVote == "בעד" || vote_item.ministerVote == "׳‘׳¢׳“" || vote_item.ministerVote == "×‘×¢×“")
                            newVote.vote1 = 1;
                        else if (vote_item.ministerVote == "נגד" || vote_item.ministerVote == "׳ ׳’׳“" || vote_item.ministerVote == "× ×ž× ×¢")
                            newVote.vote1 = 2;
                        else
                        {
                            // we will take the vote from the commitie vote if the minister didn't publish it
                            if (vote_item.passed == "בעד" || vote_item.passed == "׳‘׳¢׳“" || vote_item.passed == "×‘×¢×“")
                                newVote.vote1 = 1;
                            else if (vote_item.passed == "נגד" || vote_item.ministerVote == "׳ ׳’׳“" || vote_item.passed == "× ×ž× ×¢")
                                newVote.vote1 = 2;
                            else
                            {
                                ErrorLog.GetDefault(context).Log(new Error(new Exception("vote_item.ministerVote:" + vote_item.ministerVote)));
                                ErrorLog.GetDefault(context).Log(new Error(new Exception("vote_item.passed:" + vote_item.passed)));
                                newVote.vote1 = 0;
                            }
                        }
                        ErrorLog.GetDefault(context).Log(new Error(new Exception("updateVotes-before VOTE.Add")));
                        db.VOTE.Add(newVote);
                        db.SaveChanges();
                    }
                    else
                    {
                       // vote.
                    }
                    //ViewBag.mk = new SelectList(db.MK, "Id", "mk_name", mK_SCORE.mk);
                    //ViewBag.agendaId = new SelectList(db.AGENDA, "Id", "name", mK_SCORE.agendaId);
                    //return View(mK_SCORE);
                }
            }
        }

        // GET: api/MK_SCORE_PARTY
        public IQueryable<MK_SCORE_PARTY> GetMK_SCORE_PARTY()
        {
            ImportBillVotesByAgenda();
            using (AgendaEntities db = new AgendaEntities())
            {
                // http://www.asp.net/web-api/overview/data/using-web-api-with-entity-framework/part-5
                var MK_SCORE_PARTYs = from mk_s in db.MK_SCORE
                                      join mk_p in db.MK_TO_PARTY on mk_s.mk equals mk_p.mk
                                      select new MK_SCORE_PARTY()
                                      {
                                          IdMK_SCORE = mk_s.Id,
                                          mk = mk_s.mk,
                                          mkName = mk_s.MK1.mk_name,
                                          party = mk_p.party,
                                          partyName = mk_p.PARTY1.party1,
                                          fromDate = mk_s.fromDate,
                                          toDate = mk_s.toDate,
                                          knessetNumber = mk_p.knessetNumber,
                                          score = mk_s.score,
                                          volume = mk_s.volume,
                                          agendaId = mk_s.agendaId,
                                          agendaName = mk_s.AGENDA.name
                                      };
            
            return MK_SCORE_PARTYs;
            }
        }

        // GET: api/MK_SCORE_PARTY/5
        [ResponseType(typeof(MK_SCORE_PARTY))]
        //[Route("MK_SCORE_PARTY/{agendaId}/orders")]
        public IHttpActionResult GetMK_SCORE_PARTY(int id)
        {
            var context = System.Web.HttpContext.Current;

            using (AgendaEntities db = new AgendaEntities())
            {
                //ImportBillVotesByAgenda(id);
                //AGENDA_DTO aGENDA_DTO = db.AGENDA_DTO.Find(id);
                //AGENDA_DTO aMK_SCORE_PARTY = null;
                List<MK_SCORE_PARTY> aMK_SCORE_PARTY = (from mk_s in db.MK_SCORE
                                      join mk_p in db.MK_TO_PARTY on mk_s.mk equals mk_p.mk
                                      where mk_s.agendaId == id
                                      select new MK_SCORE_PARTY()
                                      {
                                          IdMK_SCORE = mk_s.Id,
                                          mk = mk_s.mk,
                                          mkName = mk_s.MK1.mk_name,
                                          party = mk_p.party,
                                          partyName = mk_p.PARTY1.party1,
                                          fromDate = mk_s.fromDate,
                                          toDate = mk_s.toDate,
                                          knessetNumber = mk_p.knessetNumber,
                                          score = mk_s.score,
                                          volume = mk_s.volume,
                                          agendaId = mk_s.agendaId,
                                          agendaName = mk_s.AGENDA.name
                                      }).ToList();

                if (aMK_SCORE_PARTY == null)
                {
                    ErrorLog.GetDefault(context).Log(new Error(new Exception(" GetMK_SCORE_PARTY(int id)-return NotFound")));
                    return NotFound();
                }

                ErrorLog.GetDefault(context).Log(new Error(new Exception(" GetMK_SCORE_PARTY(int id)-return Ok(aMK_SCORE_PARTY)")));

                return Ok(aMK_SCORE_PARTY);
            }
        }

       
    }
}
