import { Component, Inject } from '@angular/core';
import {
    NgForm,
    FormBuilder,
    FormsModule, ReactiveFormsModule,
    FormControl,
    FormGroup,
    Validators,
    AbstractControl
} from '@angular/forms';

import { Http } from '@angular/http';
import { AgendaService } from './agenda.service';
import { Observable } from "rxjs/Rx";
import { Agenda } from "./agenda.model";
import { LawBill } from "./lawBill.model";
import { Item2Agenda } from "./Item2Agenda.model";
import { ActivatedRoute } from '@angular/router';
import 'rxjs/add/operator/filter';

/*
https://blogs.msdn.microsoft.com/sqlexpress/2011/12/08/using-localdb-with-full-iis-part-2-instance-ownership/
https://blogs.msdn.microsoft.com/gaurav/2013/12/21/deployment-of-sql-express-localdb-on-iis/

 */
@Component({
    selector: 'agenda',
    templateUrl: './agenda.component.html'
})
    /*
    1. to do - the addin in  will get the parametes for agenda id and lawbill id from the parent page
    2. to do - the page will check the appropriate authorization
    3. the page will show the current data base on the relevant permission, for admin the data could be save, delete and update
    4. the agenda law data should be public using the web api 

    */
export class AgendaComponent {
    //getagendas: any;
    public agendaList: Agenda[];
    public lawBillList: LawBill[];
    public agendav: Agenda = new Agenda(1212, 'name', 'description');
    public item2Agenda: Item2Agenda;
    public _agendaService: AgendaService;


    public itemId : number;
    public agendaId: number;

    myForm: FormGroup;
    
    constructor(http: Http, fb: FormBuilder, private route: ActivatedRoute) {

        //debugger;
        // https://www.toptal.com/angular-js/angular-4-forms-validation 
        this.myForm = new FormGroup({
            'description': new FormControl("description", [
                Validators.required
            ]),
            'reason': new FormControl("reason"),
            'user_id': new FormControl("user_id", Validators.required)
        });

        // https://symbiotics.co.za/using-observables-in-angular-4-to-get-data-from-an-api-service/
        // , @Inject('BASE_URL') baseUrl: string
        this._agendaService = new AgendaService(http);
        //var baseUrl = 'http://localhost:54905/'; // // http://localhost:50564/api/AGENDA_dto
       
    }

    getItem2Agenda(itemId: number, agendaId: number): void {
        // https://www.concretepage.com/angular-2/angular-2-select-option-multiple-select-option-validation-example-using-template-driven-form
        // https://github.com/angular/angular/issues/4843
        console.log('getItem2Agenda start:');
        this._agendaService.getItem2Agenda(itemId, agendaId)
            .subscribe(
            resultArray => this.item2Agenda = resultArray,
            error => console.log("Error :: " + error)
            )
    }

    getLawBillList(): void {
        // https://www.concretepage.com/angular-2/angular-2-select-option-multiple-select-option-validation-example-using-template-driven-form
        // https://github.com/angular/angular/issues/4843
        console.log('getLawBillList start:' );
        //this._agendaService.getLawBillList()
        //    .subscribe(
        //    resultArray => this.lawBillList = resultArray.query_result.data.rows,
        //    error => (console.log("Error :: " + error)
        //    )

        this.lawBillList = [(new LawBill(28922, 'test', '1', new Date('1-1-2016')))
            , (new LawBill(28913, 'test2', '1', new Date('1-1-2016')))
            , (new LawBill(28882, 'test3', '1', new Date('1-1-2016')))];
    }

    getAgendas(): void {
        this._agendaService.getAgendaList()
            .subscribe(
            resultArray => this.agendaList = resultArray,
            error => console.log("Error :: " + error)
            )
    }

    ngOnInit(): void {

        this.route.queryParams
            .filter(params => params.itemId)
            .subscribe(params => {
                console.log(params); // {order: "popular"}

                this.itemId = params.itemId;
                console.log('params.itemId:' + this.itemId); // popular
            });

        this.route.queryParams
            .filter(params => params.agendaId)
            .subscribe(params => {
                console.log(params); // {order: "popular"}

                this.agendaId = params.agendaId;
                console.log('params.agendaId:' + this.agendaId); // popular
            });

        this.getLawBillList();
        this.getAgendas();
        //debugger;
        this.getItem2Agenda(this.itemId, this.agendaId);
        
    }

    onSubmit(frm: NgForm): void {
        // http://blog.ng-book.com/the-ultimate-guide-to-forms-in-angular-2/
        // https://scotch.io/tutorials/using-angular-2s-model-driven-forms-with-formgroup-and-formcontrol
        console.log('you submitted value: ', frm.value);
        if (frm.valid)
            // assume ITEM2AGENDA exist
            this.updateITEM2AGENDA(frm.value);
        else {
            console.log('frm is not valid: ', frm);
        }
    }

    onDelete(frm: NgForm): void {
        // http://blog.ng-book.com/the-ultimate-guide-to-forms-in-angular-2/
        // https://scotch.io/tutorials/using-angular-2s-model-driven-forms-with-formgroup-and-formcontrol
        console.log('you onDelete value: ', frm.value);
        if (frm.valid)
            // assume ITEM2AGENDA exist
            this.deleteITEM2item2Agenda(frm.value);
        else {
            console.log('frm is not valid: ', frm);
        }
    }

    createITEM2AGENDA(item2Agenda: Item2Agenda) {
        console.log('call createITEM2AGENDA item2Agenda: ', item2Agenda);
        //let food = { name: name };
        this._agendaService.createITEM2AGENDA(item2Agenda).subscribe(
            data => {
                // refresh the list
                console.log('refresh the list');
                this.getItem2Agenda(item2Agenda.item, item2Agenda.agenda);
                return true;
            },
            error => {
                console.error("Error saving item2Agenda!");
                console.error("error:" + error);
                return Observable.throw(error);
            }
        );
    }

    updateITEM2AGENDA(item2Agenda: Item2Agenda) {
        console.log('call updateITEM2AGENDA item2Agenda: ', item2Agenda);
        //let food = { name: name };
        this._agendaService.updateITEM2AGENDA(item2Agenda).subscribe(
            data => {
                // refresh the list
                console.log('refresh the list');
                this.getItem2Agenda(item2Agenda.item, item2Agenda.agenda);
                return true;
            },
            error => {
                console.error("Error saving item2Agenda!");
                console.error("error:" + error);
                return Observable.throw(error);
            }
        );
    }

    deleteITEM2item2Agenda(item2Agenda: Item2Agenda) {
        console.log('call deleteITEM2item2Agenda item2Agenda: ', item2Agenda);
        //let food = { name: name };
        this._agendaService.deleteITEM2item2Agenda(item2Agenda).subscribe(
            data => {
                // refresh the list
                console.log('refresh the list');
                this.getItem2Agenda(item2Agenda.item, item2Agenda.agenda);
                return true;
            },
            error => {
                console.error("Error saving item2Agenda!");
                console.error("error:" + error);
                return Observable.throw(error);
            }
        );
    }

    createAgenda(name: Agenda) {
        console.log('you submitted name: ', name);
        //let food = { name: name };
        this._agendaService.createAgenda(name).subscribe(
            data => {
                // refresh the list
                console.log('refresh the list');
                this.getAgendas();
                return true;
            },
            error => {
                console.error("Error saving Agenda!");
                console.error("error:" + error);
                return Observable.throw(error);
            }
        );
    }
}
