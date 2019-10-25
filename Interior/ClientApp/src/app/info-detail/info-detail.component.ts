import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'app-info-detail',
  templateUrl: './info-detail.component.html',
  styleUrls: ['./info-detail.component.scss']
})
export class InfoDetailComponent implements OnInit {
  @Input() public detail: Object;
  constructor() { }

  ngOnInit() {
    console.log('heey');
    console.log(this.detail)

  }

}
