import { Content } from './Content';

export class CategoryEditModel {
    id:       number;
    fileName: string;
    contents: Content[];
    file:     File;
}
