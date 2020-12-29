import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateArticleFormComponent } from './create-article-form.component';
import { FormsModule } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { ArticleService } from '../../services/article.service';
import { Router } from '@angular/router';

describe('CreateArticleFormComponent', () => {
  let component: CreateArticleFormComponent;
  let fixture: ComponentFixture<CreateArticleFormComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [FormsModule],
      declarations: [CreateArticleFormComponent],
      providers: [
        { provide: HttpClient },
        { provide: Router },
        { provide: ArticleService }
      ]
    })
      .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CreateArticleFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
