import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
// import {MyNameModule} from 'my-name';
import {MyNameModule} from 'projects/my-name/src/lib/my-name.module';

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    MyNameModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
