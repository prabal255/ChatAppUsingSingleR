import { ToastrService } from 'ngx-toastr';
import { Injectable } from '@angular/core';
import * as signalR from '@aspnet/signalr';
import { Router } from '@angular/router';
import { Observable, Subject } from 'rxjs';
import { HttpClient, HttpHeaders } from '@angular/common/http';


//4Tutorial
export class User {
  public id: string;
  public name: string;
  public connId: string;//signalr
  public msgs: Array<Message>;//5Tutorial (only client-side property)
}
export class Group{
  public  usergroupid : number;
  public  useid :number;
  public  groupId :number;
  public  groupName  : string;
  public Grpmsgs: Array<Message>

}


//5Tutorial
export class Message {
  constructor(
    public content: string,
    public mine: boolean
  ) {}
}





@Injectable({ providedIn: 'root' })
export class SignalrService {
    constructor(
        public toastr: ToastrService,
        public router: Router ,//2Tutorial,
        public _http: HttpClient
        ) { }


    hubConnection:signalR.HubConnection;
    //4Tutorial
    userData: User;

    //3Tutorial
    ssSubj = new Subject<any>();
    ssObs(): Observable<any> {
        return this.ssSubj.asObservable();
    }

    startConnection = () => {
      // debugger
        this.hubConnection = new signalR.HubConnectionBuilder()
        .withUrl('https://localhost:5001/toastr', {
            skipNegotiation: true,
            transport: signalR.HttpTransportType.WebSockets
        })
        .build();

        this.hubConnection
        .start()
        .then(() => {
            this.ssSubj.next({type: "HubConnStarted"});
        })
        .catch(err => console.log('Error while starting connection: ' + err))
    }

   async createGroup(groupName:string,selectedGroup:any)
    {
      // debugger
      var Group={
        GroupName:groupName,
      }
      await this.hubConnection.invoke("GroupName",Group,JSON.stringify(selectedGroup))
      .then(()=>{

      })
      
      .catch(err => console.error(err));
    }

    getGroupMessages(grpId : Number)
    {
      var link = "https://localhost:5001/api/Chat/getGroupMessage?grpId="+grpId
      return this._http.get(link)
    }


}
