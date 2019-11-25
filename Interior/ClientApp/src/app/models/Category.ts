import { ContentModel } from "./ContentModel";
import { FileModel } from "./File";

export class CategoryEditModel {
  id: number;
  parentId: number;
  currentFile: FileModel;
  contents: ContentModel[];
  file: File;
}
