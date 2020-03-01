import { Component, OnInit } from '@angular/core';
import { Subject } from 'rxjs';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
 
  title = 'newAng8';
  theValue =1;
  openProgressbar = new Subject<boolean>();
  public changeSomeValue():boolean{
    this.openProgressbar.next(true);
    return (this.theValue % 2) === 1;
  }

  ngOnInit() {
    this.openProgressbar
      .asObservable()
      .subscribe(ifOpen => {
        this.theValue +=1;
    });
  }

}
