import { FileModel } from './File';
import { ContentModel } from "./ContentModel";

export class BrandEditModel {
    id:       number;
    currentFile: FileModel;
    contents: ContentModel[];
    file:     File;
}
