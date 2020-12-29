import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ArticleComponent } from './article.component';
import { SignalService } from '../../services/signal.service';
import { ArticleService } from '../../services/article.service';
import { AddCommentFormComponent } from '../add-comment-form/add-comment-form.component';
import { HttpClient } from '@angular/common/http';
import { CommentComponent } from '../comment/comment.component';
import { FormsModule } from '@angular/forms';
import { RouterTestingModule } from '@angular/router/testing';
import { Observable } from 'rxjs';
import { Article } from '../../models/Article';

describe('ArticleComponent', () => {
  let component: ArticleComponent;
  let fixture: ComponentFixture<ArticleComponent>;

  beforeEach(async(() => {
    const article = {
      id: 1,
      title: 'Article Title',
      username: 'Author',
      content: 'This is Article Content',
      comments: [
        {
          commentId: 1,
          articleId: 1,
          content: 'This is Comment Content',
          username: 'Author'
        },
        {
          commentId: 2,
          articleId: 1,
          content: 'This is Comment Content',
          username: 'Author'
        }
      ]
    };

    TestBed.configureTestingModule({
      imports: [
        RouterTestingModule.withRoutes([
          { path: ':id', component: ArticleComponent }
        ]),
        FormsModule
      ],
      declarations: [ArticleComponent, AddCommentFormComponent, CommentComponent],
      providers: [
        { provide: SignalService },
        {
          provide: ArticleService,
          useValue: {
            showArticle: (articleId: number): Observable<Article> => new Observable<Article>()
          }
        },
        { provide: 'BASE_URL', useValue: 'https://localhost:6001/' },
        { provide: HttpClient },
        { provide: SignalService, useValue: { connection: { on: (method: string, action: any) => {} } } }
      ]
    })
      .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ArticleComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
