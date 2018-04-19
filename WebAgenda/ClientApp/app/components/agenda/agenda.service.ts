import { Component, Inject, Injectable } from '@angular/core';
import { Http, Response, Headers, RequestOptions } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/do';
import 'rxjs/add/operator/catch';
import { AgendaComponent } from './agenda.component';
import { Subscription } from 'rxjs/Subscription';
import { Agenda } from "./agenda.model";
import { Item2Agenda } from "./Item2Agenda.model";


//import { MessageService } from './_services/index';

// https://www.metaltoad.com/blog/angular-2-using-http-service-write-data-api
// https://www.codeproject.com/Articles/1181888/Angular-in-ASP-NET-MVC-Web-API-Part
@Injectable()
export class AgendaService {
    public baseUrl = 'http://localhost:50564'

    constructor(private http: Http) {
        // , @Inject('BASE_URL') baseUrl: string
        //var baseUrl = 'http://localhost:50564' // http://localhost:50564/api/AGENDA_dto
        console.log('agendaComponent-baseUrl:' + this.baseUrl);
        //http.get((this.baseUrl + 'Api/ITEM2AGENDA').subscribe(result => {
        //    this.threegoodaction = result.json() as agenda[];
        //}, error => console.log(error));
    }
   

    //get(url: string): Observable<any> {
    //    return this._http.get(url)
    //        .map((response: Response) => <any>response.json())
    //        // .do(data => console.log("All: " + JSON.stringify(data)))
    //        .catch(this.handleError);
    //}


    // Uses http.get() to load a single JSON file
    getAgendaList(): Observable<any>  {
        //return this.http.get(this.baseUrl + '/Api/ITEM2AGENDA').map((res: Response) => res.json());
        return this.http.get(this.baseUrl + '/api/AGENDA_dto')
            .map((response: Response) => <any>response.json())
            .catch(this.handleError)
            ;
    }


    // Uses http.get() to load a single JSON file
    getItem2Agenda(itemId: number, agendaId: number): Observable<any> {
        //return this.http.get(this.baseUrl + '/Api/ITEM2AGENDA').map((res: Response) => res.json());
        return this.http.get(this.baseUrl + '/api/ITEM2AGENDA/' + itemId + '/' + agendaId)
            .map((response: Response) => <any>response.json())
            .catch(this.handleError)
            ;
    }

    

    // Uses http.get() to load a single JSON file
    getLawBillList(): Observable<any> {
        // http://data.obudget.org/api/queries/1458/results.json?api_key=5SbTE9eOrV2oqfAI0ybXyNvBaYiVdoCzDO4EpEVK&p_%D7%9E%D7%9E%D7%A1%D7%A4%D7%A8%20%D7%9B%D7%A0%D7%A1%D7%AA=20&p_%D7%A2%D7%93%20%D7%9E%D7%A1%D7%A4%D7%A8%20%D7%9B%D7%A0%D7%A1%D7%AA=20&p_%D7%9E%D7%9E%D7%95%D7%A9%D7%91=2&p_%D7%A2%D7%93%20%D7%9E%D7%95%D7%A9%D7%91=2&p_%D7%9E%D7%9B%D7%A0%D7%A1=4&p_%D7%A2%D7%93%20%D7%9B%D7%A0%D7%A1=4

        // http://data.obudget.org/queries/1458?p_%D7%9E%D7%9E%D7%A1%D7%A4%D7%A8%20%D7%9B%D7%A0%D7%A1%D7%AA=20&p_%D7%A2%D7%93%20%D7%9E%D7%A1%D7%A4%D7%A8%20%D7%9B%D7%A0%D7%A1%D7%AA=20&p_%D7%9E%D7%9E%D7%95%D7%A9%D7%91=2&p_%D7%A2%D7%93%20%D7%9E%D7%95%D7%A9%D7%91=2&p_%D7%9E%D7%9B%D7%A0%D7%A1=4&p_%D7%A2%D7%93%20%D7%9B%D7%A0%D7%A1=4
        //return this.http.get(this.baseUrl + '/api/C3gt').map((res: Response) => res.json());
        // https://app.redash.io/hasadna/api/queries/86759/results.json?api_key=QXjzW6od3AUDzmwznPNYg9zn8TBlijZgTSfCso1Y&p_%D7%9E%D7%9E%D7%A1%D7%A4%D7%A8%20%D7%9B%D7%A0%D7%A1%D7%AA=20&p_%D7%A2%D7%93%20%D7%9E%D7%A1%D7%A4%D7%A8%20%D7%9B%D7%A0%D7%A1%D7%AA=20&p_%D7%9E%D7%9E%D7%95%D7%A9%D7%91=1&p_%D7%A2%D7%93%20%D7%9E%D7%95%D7%A9%D7%91=1&p_%D7%9E%D7%9B%D7%A0%D7%A1=1&p_%D7%A2%D7%93%20%D7%9B%D7%A0%D7%A1=1')

        return this.http.get('https://app.redash.io/hasadna/api/queries/86759/results.json?api_key=QXjzW6od3AUDzmwznPNYg9zn8TBlijZgTSfCso1Y&p_%D7%9E%D7%9E%D7%A1%D7%A4%D7%A8%20%D7%9B%D7%A0%D7%A1%D7%AA=20&p_%D7%A2%D7%93%20%D7%9E%D7%A1%D7%A4%D7%A8%20%D7%9B%D7%A0%D7%A1%D7%AA=20&p_%D7%9E%D7%9E%D7%95%D7%A9%D7%91=1&p_%D7%A2%D7%93%20%D7%9E%D7%95%D7%A9%D7%91=1&p_%D7%9E%D7%9B%D7%A0%D7%A1=1&p_%D7%A2%D7%93%20%D7%9B%D7%A0%D7%A1=1')
            .map((response: Response) => <any>response.json())
            .catch(this.handleError)
            ;
    }

    private handleError(error: Response) {
        console.log('error.statusText:' + error.statusText);
        console.log('error:' + error);
        return Observable.throw(error.statusText);
    }


    createITEM2AGENDA(item2Agenda: Item2Agenda) {

        let headers = new Headers({ 'Content-Type': 'application/json' });
        //let responseType = new ResponseT({ 'ResponseType': 'text' });
        //let options = new RequestOptions({ headers: headers });
        const options = { headers: headers, ResponseType: 'text' };
        //options.responseType = new ResponseType({ 'text'});
        console.log('before JSON.stringify');
        let body = JSON.stringify(item2Agenda);
        console.log('after JSON.stringify');
        return this.http.post(this.baseUrl + '/api/ITEM2AGENDA/', body, options);//.map((res: Response) => res.json());
    }
    updateITEM2AGENDA(item2Agenda: Item2Agenda) {
        console.log('call updateITEM2AGENDA item2Agenda: ', item2Agenda);
        var itemId = item2Agenda.item;
        var agendaId = item2Agenda.agenda;
        let headers = new Headers({ 'Content-Type': 'application/json' });
        let options = new RequestOptions({ headers: headers });
        let body = JSON.stringify(item2Agenda);
        //  [Route("api/ITEM2AGENDA/{itemId}/{agendaId}")]
        return this.http.put(this.baseUrl + '/api/ITEM2AGENDA/' + itemId + '/' + agendaId, body, options).map((res: Response) => res.json());
    }
    deleteITEM2item2Agenda(item2Agenda: Item2Agenda) {
        console.log('call deleteITEM2item2Agenda item2Agenda: ', item2Agenda);
        var itemId = item2Agenda.item;
        var agendaId = item2Agenda.agenda;
        return this.http.delete(this.baseUrl + '/api/ITEM2AGENDA/' + itemId + '/' + agendaId);
    }

    createAgenda(agenda: Agenda) {

        let headers = new Headers({ 'Content-Type': 'application/json' });
        //let responseType = new ResponseT({ 'ResponseType': 'text' });
        //let options = new RequestOptions({ headers: headers });
        const options = { headers: headers, ResponseType: 'text' }; 
        //options.responseType = new ResponseType({ 'text'});
        console.log('before JSON.stringify');
        let body = JSON.stringify(agenda);
        console.log('after JSON.stringify');
        return this.http.post(this.baseUrl + '/api/AGENDA_DTO/', body, options);//.map((res: Response) => res.json());
    }
    updateAgenda(agenda: Agenda) {
        var agenda_id = 1;
        let headers = new Headers({ 'Content-Type': 'application/json' });
        let options = new RequestOptions({ headers: headers });
        let body = JSON.stringify(agenda);
        return this.http.put(this.baseUrl + '/api/AGENDA_DTO/' + agenda_id, body, options).map((res: Response) => res.json());
    }
    deleteAgenda(agenda_id : number) {
        return this.http.delete(this.baseUrl + '/api/AGENDA_DTO/' + agenda_id);
    }
}
