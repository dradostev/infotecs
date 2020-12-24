import { Inject, Injectable } from '@angular/core'
import { HttpClient } from '@angular/common/http'
import { Observable } from 'rxjs'
import { Article } from '../models/Article'
import { Comment } from '../models/Comment'

@Injectable({
  providedIn: 'root'
})
export class ArticleService {
  constructor (private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) { }

  /**
   * Get list of Articles.
   * @returns {Observable<Article[]>}
   */
  listArticles (): Observable<Article[]> {
    return this.http.get<Article[]>(`${this.baseUrl}articles`)
  }

  /**
   * Get single Article by ID.
   * @param {number} articleId
   * @returns {Observable<Article>}
   */
  showArticle (articleId: number): Observable<Article> {
    return this.http.get<Article>(`${this.baseUrl}articles/${articleId}`)
  }

  /**
   * Save newly created Article.
   * @param {Article} article
   * @returns {Observable<Article>>}
   */
  createArticle (article: Article): Observable<Article> {
    return this.http.post<Article>(`${this.baseUrl}articles`, article)
  }

  /**
   * Delete Article by ID.
   * @param {number} articleId
   * @returns {Observable}
   */
  deleteArticle (articleId: number): Observable<any> {
    return this.http.delete(`${this.baseUrl}articles/${articleId}`)
  }

  /**
   * Create a Comment and add it to Article by ID.
   * @param {Comment} comment
   * @returns {Observable<Comment>}
   */
  addComment (comment: Comment): Observable<Comment> {
    return this.http.post<Comment>(`${this.baseUrl}articles/${comment.articleId}/comments`, comment)
  }
}
