export class LanguageShowModel{
    id:number;
    name:string;
    code:string;
}
export class LanguageEditModel{
    id:number;
    name:string;
    code:string;
    file:File;
    fileName:string;
}
export class LanguageGetModel{
    id:number;
    name:string;
    code:string;
    fileName:string;
    imageData:Blob;
    imageMimeType:string;
}