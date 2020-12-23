import { Component, OnInit } from '@angular/core';
import {Article} from "../../models/Article";
import {ArticleService} from "../../services/article.service";
import {ActivatedRoute, Router} from "@angular/router";
import {switchMap} from "rxjs/operators";
import {Comment} from "../../models/Comment";
import {EventBusService} from "../../services/event-bus.service";
import {EventData} from "../../models/EventData";

@Component({
  selector: 'app-article',
  templateUrl: './article.component.html',
  styleUrls: ['./article.component.scss']
})
export class ArticleComponent implements OnInit {
  public article: Article;

  constructor(
    private articleService: ArticleService,
    private route: ActivatedRoute,
    private router: Router,
    private eventBus: EventBusService
  ) { }

  ngOnInit() {
    let article = this.route.paramMap.pipe(
      switchMap(params => this.articleService.showArticle(Number(params.get('id'))))
    );

    article.subscribe(x => this.article = x);
  }

  onSubmitComment(comment: Comment) {
    this.articleService.addComment(comment).subscribe(
      x => this.article.comments.push(x)
    );
  }

  onRemoveArticle() {
    this.articleService.deleteArticle(this.article.id).subscribe(
      () => {
        this.eventBus.emit(new EventData('ArticleDeleted', this.article.id))
        this.router.navigate(['']);
      }
    )
  }
}
