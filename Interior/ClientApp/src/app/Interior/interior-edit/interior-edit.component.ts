import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { Router, ActivatedRoute } from '@angular/router';
import { InteriorService } from 'src/app/services/DataCenter.service';
import { InteriorModelTake } from 'src/app/models/Interior';

@Component({
  selector: 'app-interior-edit',
  templateUrl: './interior-edit.component.html',
  styleUrls: ['./interior-edit.component.scss'],
  providers:[InteriorService]
})
export class InteriorEditComponent implements OnInit {
  constructor(
    private router: Router,
    private route: ActivatedRoute,
    private interiorService:InteriorService) 
    { }
  
  interiorTakeModel:InteriorModelTake;
  ngOnInit(): void {
    this.interiorService.getInteriorbyId(2).subscribe(response=>
      {
      console.log(response["data"]);
        this.interiorTakeModel=response["data"];
    });
  }



}
