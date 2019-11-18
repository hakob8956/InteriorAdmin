import { FileModel, FileIdStorage } from 'src/app/models/File';
import { OptionContentModel } from './OptionDescription';
import { ContentModel } from './ContentModel';

export class InteriorRequestModel
{
    id: number;
    nameContent: ContentModel[];
    descriptionContent: ContentModel[];
    currentImageFile: FileModel;
    currentIosFile: FileModel;
    currentAndroidFile: FileModel;
    currentGlbFile: FileModel;
    price: number;
    isAvailable: boolean;
    buyUrl: string;
    shopId: number;
    brandId: number;
    categoryId: number;
    isVisible: boolean;
    optionContents: OptionContentModel[];
    imageFile:File;
    iosFile: File;
    androidFile: File;
    glbFile: File;
    fileIdStorage:FileIdStorage[];
    

}
