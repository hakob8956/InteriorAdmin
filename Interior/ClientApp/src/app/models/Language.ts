import { FileModel } from './File';

export class LanguageShowModel{
    id:number;
    name:string;
    code:string;
}
export class LanguageModel{
    id:number;
    name:string;
    code:string;
    currentFile: FileModel;
    file:File;
}