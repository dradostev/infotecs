import {Component, EventEmitter, OnInit, Output} from '@angular/core';
import {Article} from "../../models/Article";
import {ArticleService} from "../../services/article.service";
import {EventBusService} from "../../services/event-bus.service";
import {EventData} from "../../models/EventData";
import {Router} from "@angular/router";

@Component({
  selector: 'app-create-article-form',
  templateUrl: './create-article-form.component.html',
  styleUrls: ['./create-article-form.component.scss']
})
export class CreateArticleFormComponent implements OnInit {
  public article: Article = new Article();

  constructor(
    private articleService: ArticleService,
    private eventBus: EventBusService,
    private router: Router) { }

  ngOnInit() {
  }

  onSubmit() {
    this.articleService.createArticle(this.article)
      .subscribe(x => {
        this.eventBus.emit(new EventData('ArticleCreated', x));
        this.router.navigate(['', x.id])
      });
  }
}
