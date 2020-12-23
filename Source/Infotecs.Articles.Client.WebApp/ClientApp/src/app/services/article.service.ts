import {Inject, Injectable} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Observable} from "rxjs";
import {Article} from "../models/Article";
import {Comment} from "../models/Comment";

@Injectable({
  providedIn: 'root'
})
export class ArticleService {

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) { }

  listArticles(): Observable<Article[]> {
    return this.http.get<Article[]>(this.baseUrl + 'articles');
  }

  showArticle(articleId: number): Observable<Article> {
    return this.http.get<Article>(this.baseUrl + 'articles/' + articleId);
  }

  addComment(comment: Comment): Observable<Comment> {
    return this.http.post<Comment>(this.baseUrl + `articles/${comment.articleId}/comments`, comment);
  }
}
