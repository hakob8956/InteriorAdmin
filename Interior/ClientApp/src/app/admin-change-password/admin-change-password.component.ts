import { Router } from '@angular/router';
import { UserService } from './../services/DataCenter.service';
import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { ChangeUserPasswordModel } from '../models/User';

@Component({
  selector: 'app-admin-change-password',
  templateUrl: './admin-change-password.component.html',
  styleUrls: ['./admin-change-password.component.scss'],
  providers:[UserService]
})
export class AdminChangePasswordComponent implements OnInit {

  constructor(private  userService:UserService,private route:ActivatedRoute,private router:Router) { }
  form: FormGroup;
  userId:number;
  changeModel:ChangeUserPasswordModel;
  ngOnInit() {
    //TODO:Add validation
    this.form = new FormGroup({
      password:new FormControl("",Validators.required),
      rePassword:new FormControl("",Validators.required)
    });
    this.userId = +this.route.snapshot.params["id"];

    
  }
  submitForm(){
      this.changeModel={
        id:this.userId,
        newPassword:this.form.get("password").value,
        reNewPassword:this.form.get("rePassword").value
      }
      this.userService.ChangePasswordUser(this.changeModel).subscribe(response=>{
        this.checkValidRequest(response["success"]);
      })
  }
  cancelButton(){
    this.router.navigate(["/adminView"]);
  }
  private checkValidRequest(success:Boolean){
    if(success)
      this.router.navigate(["/adminView"]);
    else
      alert("Error");

  }


}
