import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { SignalrService } from '../signalr.service';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.css']
})
export class RegistrationComponent implements OnInit {

  constructor(public signalrService: SignalrService,public route:Router) { }

  ngOnInit(): void {
  }
  name=""
  email=""
  password=""
  async Registration(){
  debugger
    var person={
      Name:this.name,
      Username:this.email,
      Password:this.password
    }
    await this.signalrService.hubConnection.invoke("Register", person)

    .then(() => this.signalrService.toastr.info("Logged in"))
    .then(()=>{
     this.route.navigate(['/auth'])
    })
    
    .catch(err => console.error(err));
  }
}
