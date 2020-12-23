import { Component, OnInit } from '@angular/core'
import { Router } from '@angular/router'
import { Article } from '../../models/Article'
import { ArticleService } from '../../services/article.service'

@Component({
  selector: 'app-create-article-form',
  templateUrl: './create-article-form.component.html',
  styleUrls: ['./create-article-form.component.scss']
})
export class CreateArticleFormComponent implements OnInit {
  public article: Article = new Article();

  constructor (
    private articleService: ArticleService,
    private router: Router
  ) { }

  ngOnInit () {
  }

  onSubmit () {
    this.articleService.createArticle(this.article)
      .subscribe((x) => {
        this.router.navigate(['', x.id])
      })
  }
}
