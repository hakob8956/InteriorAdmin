import { CategoryEditModel } from './Category';
import { ShopEditModel } from './Shop';
import { Content } from './Content';
import { FileModel } from './File';
import { BrandEditModel } from './Brand';

export class InteriorModelTake
{
    Id: number;
    FileName: string;
    NameContents: Content[];
    DescriptionContents: Content[];
    ImageFile: FileModel;

    IosFile: FileModel;
    AndroidFile: FileModel;

    GlbFile: FileModel;
    Price: number;
    Shops: ShopEditModel[];
    IsAvailable: boolean;
    BuyUrl: string;
    Brands: BrandEditModel[];
    Categories: CategoryEditModel[];

}