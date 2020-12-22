import { Component, OnInit } from '@angular/core';
import {Article} from "../../models/Article";
import {ArticleService} from "../../services/article.service";
import {ActivatedRoute} from "@angular/router";
import {switchMap} from "rxjs/operators";

@Component({
  selector: 'app-article',
  templateUrl: './article.component.html',
  styleUrls: ['./article.component.css']
})
export class ArticleComponent implements OnInit {
  public article: Article;

  constructor(
    private articleService: ArticleService,
    private route: ActivatedRoute
  ) { }

  ngOnInit() {
    let article = this.route.paramMap.pipe(
      switchMap(params => this.articleService.showArticle(Number(params.get('id'))))
    );

    article.subscribe(x => this.article = x);
  }

}
