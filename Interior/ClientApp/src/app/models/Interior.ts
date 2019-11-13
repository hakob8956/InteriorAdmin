import { CategoryEditModel } from './Category';
import { ShopModel } from './Shop';
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
    Shops: ShopModel[];
    IsAvailable: boolean;
    BuyUrl: string;
    Brands: BrandEditModel[];
    Categories: CategoryEditModel[];

}