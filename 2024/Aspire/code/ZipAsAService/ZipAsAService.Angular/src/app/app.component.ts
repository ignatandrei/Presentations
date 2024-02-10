import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { WeatherForecasts } from '../WeatherForecast';
@Component({
  selector: 'app-root',
  standalone: true,
  imports: [CommonModule,RouterOutlet],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'ZipAsAService.Angular';
  forecasts: WeatherForecasts = [];

  constructor(private http: HttpClient) {
    //TODO: put the api url in a config file
    var baseUrl= "http://localhost:5511/";
    //console.log('the api:'+process.env['services__weatherapi__1']);
    http.get<WeatherForecasts>(baseUrl+'weatherforecast').subscribe(result => {
      next: this.forecasts = result;
      error: console.error
    });
  }
}
