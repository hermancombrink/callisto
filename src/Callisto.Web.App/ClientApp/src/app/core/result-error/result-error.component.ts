import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'app-result-error',
  templateUrl: './result-error.component.html',
  styleUrls: ['./result-error.component.css']
})
export class ResultErrorComponent implements OnInit {

  // tslint:disable-next-line:no-inferrable-types
  error: string = '';

  constructor() { }

  ngOnInit() {
  }

}
