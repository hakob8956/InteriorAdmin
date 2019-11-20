import { ContentModel } from "./ContentModel";
import { FileModel } from './File';

export class RecommendationModel {
    id:       number;
    currentFile: FileModel;
    contents: ContentModel[];
    file:     File;
    shopId:number;
    interiorId:number;
    categoryId:number;
    brandId:number;
}
