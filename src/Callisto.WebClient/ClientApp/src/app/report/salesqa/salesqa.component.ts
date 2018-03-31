import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'app-salesqa',
  templateUrl: './salesqa.component.html',
  styleUrls: ['./salesqa.component.css']
})
export class SalesqaComponent implements OnInit {

  @Input() reportQuestion: string;
  @Input() reportToken: string;
  @Input() reportUrl: string;
  @Input() reportDatasetIds: any[];

   config = {
    type: 'qna',
    tokenType: 1,
    accessToken: '',
    embedUrl: '',
    datasetIds: [],
    viewMode: 'Interactive',
    question: this.reportQuestion
  };

  constructor() { }

  ngOnInit() {
  }

}
