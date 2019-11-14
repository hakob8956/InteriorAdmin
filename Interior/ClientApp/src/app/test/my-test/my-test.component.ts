import { CategoryService } from './../../services/DataCenter.service';
import { Component, OnInit } from '@angular/core';
import { Content } from 'src/app/models/Content';

@Component({
  selector: 'app-my-test',
  templateUrl: './my-test.component.html',
  styleUrls: ['./my-test.component.scss'],
  providers:[CategoryService]
})
export class MyTestComponent  {

 

}
