export class CategoryEditModel {
    id:       number;
    fileName: string;
    contents: Content[];
    file:     File;
}

export class Content {
    id:         number;
    languageId: number;
    text:       string;
}