import {Inject, Injectable} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Observable} from "rxjs";
import {Article} from "../models/Article";

@Injectable({
  providedIn: 'root'
})
export class ArticleService {

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) { }

  listArticles(): Observable<Article[]> {
    return this.http.get<Article[]>(this.baseUrl + 'articles');
  }
}
