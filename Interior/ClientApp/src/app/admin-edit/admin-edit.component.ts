import { RegisterUserModel } from "./../models/User";
import { UserService } from "./../services/DataCenter.service";
import { Component, OnInit } from "@angular/core";
import { FormGroup, FormControl, Validators } from "@angular/forms";
import { Router, ActivatedRoute } from "@angular/router";

@Component({
  selector: "app-admin-edit",
  templateUrl: "./admin-edit.component.html",
  styleUrls: ["./admin-edit.component.scss"],
  providers: [UserService]
})
export class AdminEditComponent implements OnInit {
  userId: number;
  constructor(
    private router: Router,
    private route: ActivatedRoute,
    private userService: UserService
  ) {}
  form: FormGroup;
  charLength = 4;
  user: RegisterUserModel;
  userCreate: boolean = true;
  ngOnInit() {
    this.form = new FormGroup({
      fName: new FormControl(""),
      lName: new FormControl(""),
      email: new FormControl("", [Validators.required, Validators.email]),
      userName:new FormControl("",[Validators.required]),
      password: new FormControl("")
    });
    this.userId = +this.route.snapshot.params["id"];
    if (!Number.isNaN(this.userId) && this.userId > 0) {
      this.userCreate = false;
      this.userService.getUserById(this.userId).subscribe(response => {
        this.user = response["data"];
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
  }
  submitForm() {
     this.user = {
       id:this.userId,
       username:this.form.get("userName").value,
       firstName:this.form.get("fName").value,
       lastName:this.form.get("lName").value,
       password: this.form.get("password").value,
       roleId:2,
       email:this.form.get("email").value 
    }

    this.userService.ChangeCreateUser(this.user).subscribe(response=>console.log(response));
  }
  cancelButton() {
    this.router.navigate(["/adminView"]);
  }
  checkForLength(control: FormControl) {
    if (control.value.length <= this.charLength) {
      return {
        lengthError: true
      };
    }
    return null;
  }
}
