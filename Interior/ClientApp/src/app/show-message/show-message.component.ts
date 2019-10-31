import { Component, OnInit, Input } from '@angular/core';
import { MessageBox } from '../models/MessageBox';

@Component({
  selector: 'app-show-message',
  templateUrl: './show-message.component.html',
  styleUrls: ['./show-message.component.scss']
})
export class ShowMessageComponent implements OnInit {
  @Input() messageBox:MessageBox;
  constructor() { }

  ngOnInit() {
    console.log(this.messageBox.statusCode + '  ' + this.messageBox.message);
  }

}
