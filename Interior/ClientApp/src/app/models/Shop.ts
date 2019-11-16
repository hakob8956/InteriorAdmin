import { FileModel } from './File';
import { ContentModel } from "./ContentModel";

export class ShopModel {
    id:       number;
    currentFile: FileModel;
    contents: ContentModel[];
    file:     File;
}

