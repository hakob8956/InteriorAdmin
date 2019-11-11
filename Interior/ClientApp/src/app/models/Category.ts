import { Content } from './Content';
import { FileModel } from './File';

export class CategoryEditModel {
    id:       number;
    currentFile: FileModel;
    contents: Content[];
    file:     File;
}

