import { FileType } from './Enums';

export class FileModel
{
    fileId: number;
    fileName: string;
    imageData: Blob;
    imageMimeType: string;
    fileType:FileType;
}
export class FileIdStorage{
    fileId: number;
    fileType:FileType;
}