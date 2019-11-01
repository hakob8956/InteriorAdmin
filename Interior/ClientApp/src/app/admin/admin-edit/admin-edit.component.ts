import { Observable } from 'rxjs';
import { UpdateUserModel,RegisterUserModel } from "../../models/User";
import { UserService,RoleService } from "../../services/DataCenter.service";
import { Component, OnInit } from "@angular/core";
import { FormGroup, FormControl, Validators } from "@angular/forms";
import { Router, ActivatedRoute } from "@angular/router";

@Component({
  selector: "app-admin-edit",
  templateUrl: "./admin-edit.component.html",
  styleUrls: ["./admin-edit.component.scss"],
  providers: [UserService,RoleService]
})
export class AdminEditComponent implements OnInit {
  userId: number;
  constructor(
    private router: Router,
    private route: ActivatedRoute,
    private userService: UserService,
    private roleService:RoleService
  ) {}
  form: FormGroup;
  charLength = 4;
  userUpdateModel: UpdateUserModel;
  userCreateModel:RegisterUserModel;
  isUserCreate: boolean = true;
  roles:any;
  ngOnInit() {
    this.form = new FormGroup({
      fName: new FormControl(""),
      lName: new FormControl(""),
      email: new FormControl("", [Validators.required, Validators.email]),
      userName:new FormControl("",[Validators.required]),
      roles:new FormControl("",Validators.required),
      password:new FormControl("",Validators.required)
    });
    this.roleService.getRoles().subscribe(response=>this.roles=response);
    this.userId = +this.route.snapshot.params["id"];
    if (!Number.isNaN(this.userId) && this.userId > 0) {
        this.isUserCreate = false;
        this.userService.getUserById(this.userId).subscribe(response => {
        this.userUpdateModel = response["data"];
        console.log(this.userUpdateModel)
        this.initForm();
      });
    } else {
      this.userId = 0;
    }
  }
  initForm() {
    this.form
      .get("fName")
      .setValue(this.userUpdateModel.firstName != null ? this.userUpdateModel.firstName : "");
    this.form
      .get("lName")
      .setValue(this.userUpdateModel.lastName != null ? this.userUpdateModel.lastName : "");
    this.form
      .get("email")
      .setValue(this.userUpdateModel.email != null ? this.userUpdateModel.email : "");
      this.form
      .get("userName")
      .setValue(this.userUpdateModel.username != null ? this.userUpdateModel.username : "");
      this.form
      .get("roles")
      .setValue(this.userUpdateModel.roleId != null ? this.getRoleNameById(this.userUpdateModel.roleId) : "Error");
      this.form.get("password").disable();
      
  }
  submitForm() {
      this.userCreateModel = {
        id:this.userId,
        username:this.form.get("userName").value,
        firstName:this.form.get("fName").value,
        lastName:this.form.get("lName").value,
        roleId: this.getRoleIdByName(this.form.get("roles").value),
        email:this.form.get("email").value,
        password:this.form.get("password").value,      
     }
     this.userUpdateModel = this.userCreateModel;  
     if(this.isUserCreate)
        this.userService.CreateUser(this.userCreateModel).subscribe(response=>{
          this.checkValidRequest(response["success"])
        });
     else
     {
        this.userUpdateModel = this.userCreateModel; 
        this.userService.UpdateUser(this.userUpdateModel).subscribe(response=>{
          this.checkValidRequest(response["success"])
        });
     }
  }
  cancelButton() {
    this.router.navigate(["/adminView"]);
  }

  private checkValidRequest(success:Boolean){
    if(success)
      this.router.navigate(["/adminView"]);
    else
      alert("Error");

  }
  private getRoleIdByName(roleName:string):number {
    console.log(this.roles)
    for(var x in this.roles){
      if(this.roles[x].name == roleName){
          return this.roles[x].id;
      }    
    }
    return -1;
  }
  private getRoleNameById(id:number):string {
    console.log(this.roles)
    for(var x in this.roles){
      if(this.roles[x].id == id){
          return this.roles[x].name;
      }    
    }
    return "";
  }
  checkForLength(control: FormControl) {
    if (this.userId > 0 && control.value.length <= this.charLength) {
      return {
        lengthError: true
      };
    }
    return null;
  }
}
