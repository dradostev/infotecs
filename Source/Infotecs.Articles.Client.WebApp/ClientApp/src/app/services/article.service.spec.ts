import { TestBed } from '@angular/core/testing'

import { ArticleService } from './article.service'
import { HttpClient, HttpXhrBackend } from '@angular/common/http'
import { Observable } from 'rxjs'
import { Article } from '../models/Article'
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing'

describe('ArticleService', () => {
  beforeEach(() => TestBed.configureTestingModule({
    imports: [HttpClientTestingModule],
    providers: [
      { provide: HttpClient },
      { provide: 'BASE_URL', useValue: 'https://localhost:6001/' },
      { provide: HttpXhrBackend }
    ]
  }))

  it('should be created', () => {
    const service: ArticleService = TestBed.get(ArticleService)
    expect(service).toBeTruthy()
  })
})
