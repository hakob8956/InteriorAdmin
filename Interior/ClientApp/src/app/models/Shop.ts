import { FileModel } from './File';
import { Content } from './Content';

export class ShopModel {
    id:       number;
    currentFile: FileModel;
    contents: Content[];
    file:     File;
}

