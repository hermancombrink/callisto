import { Component } from '@angular/core';
import { AlertService, MessageSeverity } from '../../services/alert.service';
import { OnInit } from '@angular/core/src/metadata/lifecycle_hooks';
import { ToastyService } from 'ng2-toasty';

import 'chart.js';

declare var jQuery:any;

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  constructor(private alertService: AlertService, private toastyService: ToastyService) { }

  // Dounghunt chart

  ngOnInit() {
    this.alertService.showMessage("Welcome to Callisto", "ERP Management System", MessageSeverity.info);
  }

  public workPriotityOrderLabels:string[] = ['Shutdown', 'Urgent', 'Within 3 days', 'Within 7 days', 'Within 14 days'];
  public workPriotityOrderData:number[] = [2, 3, 10, 7, 16];

  public mostMoneyLabels:string[] = ['BES07', 'HYD04', 'HRA01', 'EXT01', 'ADMIN', 'SF02', 'WHKL', 'TNK05'];
  public mostMoneyData:any[] = [
    {data: [500, 1500, 129, 2000, 800, 452, 123, 790, 1244, 2333], label: '$'}
  ];
  public mostMoneyOptions:any = {
    scaleShowVerticalLines: false,
    responsive: true
  };
  public mostMoneyLegend:boolean = false;

  public backlogTradeLabels:string[] = ['Boilermaker', 'Electric', 'EngAssist', 'Filter', 'Mechanic', 'Tradesman'];
  public backlogTradeData:any[] = [
    {data: [2, 2, 5, 3, 7, 12], label: 'Open Work Orders'},
    {data: [5, 15, 7, 22, 16, 23], label: 'Man Hours'}
  ];
  public backlogTradeOptions:any = {
    scaleShowVerticalLines: false,
    responsive: true
  };
  public backlogTradeLegend:boolean = true;

  public assetByTypeLabels:string[] = ['FE', 'PUMP', 'ROOM', 'BUILDING', 'PSB', 'COMPRESSOR', 'EM', 'TANK', 'BOILER', 'FORKLIFT'];
  public assetByTypeData:number[] = [5.26, 5.26, 15.79, 14.04, 14.04, 10.53, 10.53, 8.77, 8.77, 7.02];

  public requestsByPriorityLabels:string[] = ['Pending Allocation','Pending Approval', 'In Progress', 'On Hold'];
  public requestsByPriorityData:number[] = [5, 5, 15, 14];

  public assestDownTimePerCategoryData:Array<any> = [
    {data: [6, 5, 8, 8, 5, 5, 4], label: 'Building', fill:false},
    {data: [2, 4, 0, 1, 8, 2, 9], label: 'Production', fill:false},
    {data: [1, 8, 7, 9, 10, 2, 4], label: 'Fixtures', fill:false}
  ];
  public assestDownTimePerCategoryLabels:Array<any> = ['January', 'February', 'March', 'April', 'May', 'June', 'July'];

  public barChartData:any[] = [
    {data: [65, 59, 80, 81, 56, 55, 40], label: 'Series A'},
    {data: [28, 48, 40, 19, 86, 27, 90], label: 'Series B'}
  ];
  
  public bar1Labels:string[] = ['Boilermaker', 'Electric', 'EngAssist', 'Filter', 'Mechanic', 'Tradesman'];
  public bar1Data:any[] = [
    {data: [2, 2, 5, 3, 7, 12], label: 'Open Work Orders'},
    {data: [5, 15, 7, 22, 16, 23], label: 'Man Hours'}
  ];

  public bar2Labels:string[] = ['Boilermaker', 'Electric', 'EngAssist', 'Filter', 'Mechanic', 'Tradesman'];
  public bar2Data:any[] = [
    {data: [2, 2, 5, 3, 7, 12], label: 'Critical'},
    {data: [5, 15, 7, 23, 12, 6], label: 'High'},
    {data: [12, 5, 12, 1, 22, 5], label: 'Normal'},
    {data: [2, 1, 4, 15, 12, 3], label: 'Low'}
  ];

  public bar3Labels:string[] =  ['January', 'February', 'March', 'April', 'May', 'June', 'July'];
  public bar3Data:any[] = [
    {data: [2, 2, 5, 3, 7, 12], label: 'Boilermaker', fill:false},
    {data: [5, 15, 7, 22, 16, 23], label: 'Electric', fill:false},
    {data: [2, 2, 5, 3, 7, 12], label: 'EngAssist', fill:false},
    {data: [5, 15, 7, 22, 16, 23], label: 'Filter', fill:false},
    {data: [2, 2, 5, 3, 7, 12], label: 'Mechanic', fill:false},
    {data: [5, 15, 7, 22, 16, 23], label: 'Tradesman', fill:false}
  ];

  public bar4Labels:string[] = ['January', 'February', 'March', 'April', 'May', 'June', 'July'];
  public bar4Data:any[] = [
    {data: [2, 2, 5, 3, 7, 12], label: 'Boilermaker', fill:false},
    {data: [5, 15, 7, 22, 16, 23], label: 'Electric', fill:false},
    {data: [2, 2, 5, 3, 7, 12], label: 'EngAssist', fill:false},
    {data: [5, 15, 7, 22, 16, 23], label: 'Filter', fill:false},
    {data: [2, 2, 5, 3, 7, 12], label: 'Mechanic', fill:false},
    {data: [5, 15, 7, 22, 16, 23], label: 'Tradesman', fill:false}
  ];
  // Main Chart

  public flotDataset:Array<any> = [
    [[0, 4], [1, 8], [2, 5], [3, 10], [4, 4], [5, 16], [6, 5], [7, 11], [8, 6], [9, 11], [10, 30], [11, 10], [12, 13], [13, 4], [14, 3], [15, 3], [16, 6]],
    [[0, 1], [1, 0], [2, 2], [3, 0], [4, 1], [5, 3], [6, 1], [7, 5], [8, 2], [9, 3], [10, 2], [11, 1], [12, 0], [13, 2], [14, 8], [15, 0], [16, 0]]
  ];

  public flotOptions:any =
  {
    series: {splines: {show: true, tension: 0.4, lineWidth: 1, fill: 0.4},},
    grid: {tickColor: "#d5d5d5", borderWidth: 1, color: '#d5d5d5'},
    colors: ["#1ab394", "#1C84C6"],
  };

  // Peity chart

  public peityType1:string = "bar";
  public peityOptions1:any = { fill: ["#1ab394", "#d7d7d7"], width:100};

  public peityType2:string = "line";
  public peityOptions2:any = { fill: '#1ab394',stroke:'#169c81', width: 64 };

  // Sparkline chart

  public sparklineData:Array<any> = [5, 6, 7, 2, 0, 4, 2, 4, 5, 7, 2, 4, 12, 14, 4, 2, 14, 12, 7];
  public sparklineOptions:any = { type: 'bar', barWidth: 8, height: '150px', barColor: '#1ab394', negBarColor: '#c6c6c6'};
}
