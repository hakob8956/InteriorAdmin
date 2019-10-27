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
    RoleId: number;
    Token: string;
}