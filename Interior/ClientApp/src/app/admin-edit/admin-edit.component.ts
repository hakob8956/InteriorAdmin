import { Observable } from 'rxjs';
import { RegisterUserModel } from "./../models/User";
import { UserService,RoleService } from "./../services/DataCenter.service";
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
  user: RegisterUserModel;
  userCreate: boolean = true;
  roles:any;
  ngOnInit() {
    this.form = new FormGroup({
      fName: new FormControl(""),
      lName: new FormControl(""),
      email: new FormControl("", [Validators.required, Validators.email]),
      userName:new FormControl("",[Validators.required]),
      password: new FormControl(""),
      roles:new FormControl("",Validators.required)
    });

    this.roleService.getRoles().subscribe(response=>this.roles=response);
    this.userId = +this.route.snapshot.params["id"];
    if (!Number.isNaN(this.userId) && this.userId > 0) {
        this.userCreate = false;
        this.userService.getUserById(this.userId).subscribe(response => {
        this.user = response["data"];
        console.log(this.user)
        this.initForm();
      });
    } else {
      this.userId = 0;
    }
  }
  initForm() {
    this.form
      .get("fName")
      .setValue(this.user.firstName != null ? this.user.firstName : "");
    this.form
      .get("lName")
      .setValue(this.user.lastName != null ? this.user.lastName : "");
    this.form
      .get("email")
      .setValue(this.user.email != null ? this.user.email : "");
      this.form
      .get("userName")
      .setValue(this.user.username != null ? this.user.username : "");
      this.form
      .get("roles")
      .setValue(this.user.roleName != null ? this.user.roleName : "Error");
      
  }
  submitForm() {
      this.user = {
        id:this.userId,
        username:this.form.get("userName").value,
        firstName:this.form.get("fName").value,
        lastName:this.form.get("lName").value,
        password: this.form.get("password").value,
        roleName:this.form.get("roles").value,
        email:this.form.get("email").value 
     }
     if(this.userId == 0)
        this.userService.CreateUser(this.user).subscribe(response=>console.log(response));
     else
        this.userService.UpdateUser(this.user)
  }
  cancelButton() {
    this.router.navigate(["/adminView"]);
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
