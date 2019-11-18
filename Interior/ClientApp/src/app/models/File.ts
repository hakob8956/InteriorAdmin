import { FileType } from './Enums';

export class FileModel
{
    fileId: number;
    fileName: string;
    imageData: Blob;
    imageMimeType: string;
    fileType:FileType;
}
