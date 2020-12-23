import { Comment } from './Comment'

export class Article {
  public id: number;

  public username: string;

  public title: string;

  public content: string;

  public comments: Comment[];
}
