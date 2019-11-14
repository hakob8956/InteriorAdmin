import { FileModel } from 'src/app/models/File';
import { Content } from '@angular/compiler/src/render3/r3_ast';
import { OptionContentModel } from './OptionDescription';

export class InteriorRequestModel
{
    Id: number;
    NameContent: Content[];
    DescriptionContent: Content[];
    ImageFile: FileModel;
    IosFile: FileModel;
    AndroidFile: FileModel;
    GlbFile: FileModel;
    Price: number;
    IsAvailable: boolean;
    BuyUrl: string;
    ShopId: number;
    BrandId: number;
    CategoryId: number;
    IsVisible: boolean;
    OptionContents: OptionContentModel[];
}