import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'app-list-notes',
  templateUrl: './list-notes.component.html',
  styleUrls: ['./list-notes.component.css']
})
export class ListNotesComponent implements OnInit {

  @Input()
  entityType = "";

  @Input()
  entityId = "";

  constructor() { }

  ngOnInit() {
  }

}
