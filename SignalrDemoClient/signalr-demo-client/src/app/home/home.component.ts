import { Group, Message, User } from './../signalr.service';
import { SignalrService } from 'src/app/signalr.service';
import { Component, OnInit } from '@angular/core';
import { Console } from 'console';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss'],
})
export class HomeComponent implements OnInit {
  constructor(
    public signalrService: SignalrService //2Tutorial
  ) {}

  //vars
  //4Tutorial
  users: Array<User> = new Array<User>();
  groups: Array<Group> = new Array<Group>();
  selectedUser: User;
  selectGroup: Group;
  list = [];
  selectedGroup = [];
  
  GroupId = 0;
  //5Tutorial
  msg: string;
  Grpmsgs: string;

  submit() {
    // debugger;
    let groupName = prompt('Please Enter a Name', 'Friends');
    if (groupName != null) {
      this.signalrService.createGroup(groupName, this.selectedGroup);
    } else {
      alert('Not Created the Group');
    }
  }
  onChange(name: string, isChecked: boolean) {
    // const cartoons = (this.form.controls.name as FormArray);

    if (isChecked) {
      this.selectedGroup.push(name);
    } else {
      // const index = this.selectedGroup.findIndex(x => x.value === name);
      this.selectedGroup = this.selectedGroup.filter((x) => x != name);
    }
  }

  ngOnInit(): void {
    // debugger;
    this.list.push(this.signalrService.userData?.name);
    this.userOnLis();
    this.userOffLis();
    this.logOutLis();
    this.getOnlineUsersLis();
    this.sendMsgLis();
    this.sendMsgResponsegrp();
    this.recieveMsgFromGroup();
    //this.getGroups(Number(localStorage.getItem('personId')));
    this.getGroupsLis();

    //hubConnection.state is 1 when hub connection is connected.
    if (this.signalrService.hubConnection.state == 1) {
      this.getOnlineUsersInv();
      this.getGroups(Number(localStorage.getItem('personId')));
    } else {
      this.signalrService.ssSubj.subscribe((obj: any) => {
        if (obj.type == 'HubConnStarted') {
          this.getGroups(Number(localStorage.getItem('personId')));
          this.getOnlineUsersInv();
        }
      });
    }
  }

  //4Tutorial
  logOut(): void {
    this.signalrService.hubConnection
      .invoke('logOut', this.signalrService.userData.id)
      .catch((err) => console.error(err));
  }
  logOutLis(): void {
    this.signalrService.hubConnection.on('logoutResponse', () => {
      localStorage.removeItem('personId');
      location.reload();
      // this.signalrService.hubConnection.stop();
    });
  }

  getGroups(id: number): void {
    // debugger;
    this.signalrService.hubConnection
      .invoke('getGroups', id)
      .catch((err) => console.error(err));
  }
  private getGroupsLis(): void {
    this.signalrService.hubConnection.on(
      'getGroupsResponse',
      (UserGroups: Array<Group>) => {
        // debugger;
        this.groups = [...UserGroups];
        console.log('this is your groups', this.groups);
      }
    );
  }

  //4Tutorial
  userOnLis(): void {
    this.signalrService.hubConnection.on('userOn', (newUser: User) => {
      // debugger;
      console.log(newUser);
      this.users.push(newUser);
      this.list.push(newUser.name);
      console.log('this is online uxers', this.list);
    });
  }
  userOffLis(): void {
    this.signalrService.hubConnection.on('userOff', (personId: string) => {
      this.users = this.users.filter((u) => u.id != personId);
    });
  }

  //4Tutorial
  getOnlineUsersInv(): void {
    this.signalrService.hubConnection
      .invoke('getOnlineUsers')
      .catch((err) => console.error(err));
  }
  private getOnlineUsersLis(): void {
    this.signalrService.hubConnection.on(
      'getOnlineUsersResponse',
      (onlineUsers: Array<User>) => {
        this.users = [...onlineUsers];
        console.log('Hi I am checking', this.users);
        for (var i in this.users) {
          // debugger;
          this.list.push(this.users[i].name);
        }
        console.log('this is online uxers added newly', this.list);
      }
    );
  }

  //5Tutorial
  sendMsgInv(): void {
    if (this.msg?.trim() === '' || this.msg == null) return;

    this.signalrService.hubConnection
      .invoke('sendMsg', this.selectedUser.connId, this.msg)
      .catch((err) => console.error(err));

    if (this.selectedUser.msgs == null) this.selectedUser.msgs = [];
    this.selectedUser.msgs.push(new Message(this.msg, true));
    this.msg = '';
  }

  private sendMsgLis(): void {
    this.signalrService.hubConnection.on(
      'sendMsgResponse',
      (connId: string, msg: string) => {
        let receiver = this.users.find((u) => u.connId === connId);
        if (receiver.msgs == null) receiver.msgs = [];
        receiver.msgs.push(new Message(msg, false));
      }
    );
  }
  // <-------------------------->

  sendMsgtoGrp(groupId): void {
    this.SendMsgToGroup(groupId);

    if (this.Grpmsgs?.trim() === '' || this.Grpmsgs == null) return;
     debugger;
   
    this.signalrService.hubConnection
      .invoke(
        'sendMsgToGroup',
        Number(localStorage.getItem('personId')),
        this.Grpmsgs,
        this.selectGroup.groupName
      )
      .catch((err) => console.error(err));

    if (this.selectGroup.Grpmsgs == null) this.selectGroup.Grpmsgs = [];
    //this.selectGroup.Grpmsgs.push(new Message(this.Grpmsgs, true));
    this.Grpmsgs = '';
  }

  private sendMsgResponsegrp(): void {
    // debugger;
    this.signalrService.hubConnection.on(
      'sendMsgResponsegrp',
      (msg: string, grpid: Number) => {
        debugger;

        let receiver = this.groups.find((u) => u.groupId === grpid);
        if (receiver.Grpmsgs == null) receiver.Grpmsgs = [];
        receiver.Grpmsgs.push(new Message(msg, false));
      }
    );
  }

  // Adding Extra fuiless

  sendMsgGrp(): void {
    if (this.msg?.trim() === '' || this.msg == null) return;

    this.signalrService.hubConnection
      .invoke('sendMsg', this.selectedUser.connId, this.msg)

      .catch((err) => console.error(err));

    if (this.selectedUser.msgs == null) this.selectedUser.msgs = [];

    this.selectedUser.msgs.push(new Message(this.msg, true));

    console.log(this.selectedUser.msgs);

    this.msg = '';
  }

  grpMsgss: Message[] = [];

  private recieveMsgFromGroup(): void {

    debugger;

    this.signalrService.hubConnection.on(
      'RecieveMessageFromGroup',
      (Grpmsgs: string) => {
        console.log(Grpmsgs);
        console.log(this.selectGroup);
        if(this.selectGroup.Grpmsgs==null){
          console.log("inside")
          this.selectGroup.Grpmsgs = [];

        }
        // if (receiver.Grpmsgs == null) receiver.Grpmsgs = [];
        // this.grpMsgss.push(new Message(Grpmsgs, false));
        this.selectGroup.Grpmsgs.push(new Message(Grpmsgs, false));
        console.log("aftwr pushed ",this.selectGroup)
      }
    );
  }

  groupIds: number;

  joinGroup(user) {

    debugger
    this.selectGroup = user;
    this.groupIds = this.selectGroup.groupId;
    if(this.selectGroup.Grpmsgs==null){
      console.log("inside")
      this.selectGroup.Grpmsgs = [];

    }
    this.signalrService.hubConnection
      .invoke('joinGroup', this.groupIds.toString())
      .catch((err) => console.error(err));
      this.signalrService.getGroupMessages(this.groupIds).subscribe((res:any)=>{
        this.selectGroup.Grpmsgs = [];
        console.log(res)
        for(var i in res){
          console.log("in loop",res[i].message)
          this.selectGroup.Grpmsgs.push(new Message(res[i].message, false))
        }
        //this.selectGroup.Grpmsgs.push(res)
        console.log(this.selectGroup)
      })
  }
  SendMsgToGroup(groupId) {
    // debugger;
    this.Grpmsgs =this.signalrService.userData?.name + "  --->   "+ this.Grpmsgs
    this.signalrService.hubConnection
      .invoke('sendMessageToGroups', groupId.toString(), this.Grpmsgs,Number(localStorage.getItem('personId')))
      .catch(function (err) {
        return console.error(err.toString());
      });
  }
}
