import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SidebarComponent } from './sidebar.component';
import { RouterTestingModule } from '@angular/router/testing';
import { CreateArticleFormComponent } from '../create-article-form/create-article-form.component';
import { ArticleService } from '../../services/article.service';
import { HttpClient } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { Observable } from 'rxjs';
import { Article } from '../../models/Article';

describe('SidebarComponent', () => {
  let component: SidebarComponent;
  let fixture: ComponentFixture<SidebarComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [
        RouterTestingModule.withRoutes([
          { path: 'create-article', component: CreateArticleFormComponent }
        ]),
        FormsModule
      ],
      declarations: [SidebarComponent, CreateArticleFormComponent],
      providers: [
        { provide: HttpClient },
        { provide: 'BASE_URL', useValue: 'https://localhost:6001/' },
        { provide: ArticleService, useValue: { listArticles: () => new Observable<Article>() } }
      ]
    })
      .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SidebarComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
