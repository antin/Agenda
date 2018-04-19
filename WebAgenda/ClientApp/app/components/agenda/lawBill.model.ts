export class LawBill {
    /*
    session_num: 303,
total_for: 38,
vote_item_dscr: "להעביר את הצעת החוק לוועדה שתקבע ועדת הכנסת",
vote_date: "2018-01-24",
id: 28708,
PlenumStart: "2017-10-22",
vote_nbr_in_sess: 11,
PlenumFinish: "2018-03-18",
is_accepted: 1,
is_elctrnc_vote: 1,
vote_item_id: 101,
sess_item_nbr: 20,
vote_stat: 1,
vote_type: 1,
reason: 4,
knesset_num: 20,
voteurl: "http://www.knesset.gov.il/vote/heb/Vote_Res_Map.asp?vote_id_t=28708",
remark: null,
sess_item_id: 2020214,
sess_item_dscr: "הצעת חוק מינוי ייצוג משפטי לקטין (תיקוני חקיקה), התשע"ח-2017",
session_id: 2063780,
nldurl: "http://main.knesset.gov.il/Activity/Legislation/Laws/Pages/LawBill.aspx?t=lawsuggestionssearch&lawitemid=2020214",
total_against: 0,
total_abstain: 0,
modifier: "Unknown"
    */

    constructor(
        public id: number,
        public sess_item_dscr: string,
        public vote_type: string,
        public vote_date: Date
    ) {

    }

}