import { FileModel } from 'src/app/models/File';
import { Content } from '@angular/compiler/src/render3/r3_ast';
import { OptionContentModel } from './OptionDescription';

export class InteriorRequestModel
{
    id: number;
    nameContent: Content[];
    descriptionContent: Content[];
    imageFile: FileModel;
    iosFile: FileModel;
    androidFile: FileModel;
    glbFile: FileModel;
    price: number;
    isAvailable: boolean;
    buyUrl: string;
    shopId: number;
    brandId: number;
    categoryId: number;
    isVisible: boolean;
    optionContents: OptionContentModel[];
}