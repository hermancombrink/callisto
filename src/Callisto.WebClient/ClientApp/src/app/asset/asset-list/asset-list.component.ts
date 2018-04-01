import { Component, OnInit } from '@angular/core';

declare var $: any;

@Component({
  selector: 'app-asset-list',
  templateUrl: './asset-list.component.html',
  styleUrls: ['./asset-list.component.css']
})
export class AssetListComponent implements OnInit {

  constructor() { }

  ngOnInit() {
    $('#jstree1').jstree({
      'core' : {
          'check_callback' : true
      },
      'plugins' : [ 'types', 'dnd' ],
      'types' : {
          'default' : {
              'icon' : 'fa fa-folder'
          },
          'html' : {
              'icon' : 'fa fa-file-code-o'
          },
          'svg' : {
              'icon' : 'fa fa-file-picture-o'
          },
          'css' : {
              'icon' : 'fa fa-file-code-o'
          },
          'img' : {
              'icon' : 'fa fa-file-image-o'
          },
          'js' : {
              'icon' : 'fa fa-file-text-o'
          }

      }
  });

  }

}
