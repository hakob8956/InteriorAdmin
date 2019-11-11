import { FileModel } from './File';
import { Content } from './Content';

export class BrandEditModel {
    id:       number;
    currentFile: FileModel;
    contents: Content[];
    file:     File;
}
