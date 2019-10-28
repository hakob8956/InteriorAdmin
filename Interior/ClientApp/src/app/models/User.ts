export class RegisterUserModel
{
    id:number;
    username:string;
    firstName: string;
    lastName: string;
    password: string;
    roleName: string;
    email:string;
}

export class LoginUserModel
{
    Username: string;
    Password: string;
    RoleName: string;
    Token: string;
}