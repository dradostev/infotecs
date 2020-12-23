import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateArticleFormComponent } from './create-article-form.component';

describe('CreateArticleFormComponent', () => {
  let component: CreateArticleFormComponent;
  let fixture: ComponentFixture<CreateArticleFormComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CreateArticleFormComponent ]
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
