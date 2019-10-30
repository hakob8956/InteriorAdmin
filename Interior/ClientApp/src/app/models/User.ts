export class RegisterUserModel
{
    id:number;
    username:string;
    firstName: string;
    lastName: string;
    password: string;
    roleId: number;
    email:string;
}

export class LoginUserModel
{
    Username: string;
    Password: string;
    RoleId: string;
    Token: string;
}
export class UpdateUserModel
{
    id:number;
    username:string;
    firstName: string;
    lastName: string;
    roleId: number;
    email:string;
    
}
export class ChangeUserPasswordModel{
    id:number;
    newPassword:string;
    reNewPassword:string;

}