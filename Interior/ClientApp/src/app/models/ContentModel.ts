import { ContentType } from './Enums';

export class ContentModel {
  id: number;
  languageId: number;
  text: string;
  type: ContentType;
}
