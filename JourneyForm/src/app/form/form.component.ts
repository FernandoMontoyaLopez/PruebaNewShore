import { Component } from '@angular/core';
import { FormControl } from '@angular/forms';
import { RestApiService } from '../services/rest-api.service';


@Component({
  selector: 'app-form',
  templateUrl: './form.component.html',
  styleUrls: ['./form.component.css']
})
export class FormComponent {
  Journey: any = [];
  origin = new FormControl('');
  destination = new FormControl('');
  constructor(public restApi: RestApiService) {}
  searchJourney(){
    this.restApi.searchJourney(this.origin.value, this.destination.value).subscribe((data: {}) => {
      this.Journey = data;
    });
  }
}
