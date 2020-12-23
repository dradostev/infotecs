import {Component, OnInit} from '@angular/core';
import {ArticleService} from "../../services/article.service";
import {Article} from "../../models/Article";
import {EventBusService} from "../../services/event-bus.service";

@Component({
  selector: 'app-sidebar',
  templateUrl: './sidebar.component.html',
  styleUrls: ['./sidebar.component.scss']
})
export class SidebarComponent implements OnInit {
  public articles: Article[];

  constructor(private articleService: ArticleService, private eventBus: EventBusService) { }

  ngOnInit() {
    this.eventBus.on('ArticleCreated', (payload: Article) => this.articles.push(payload));
    this.articleService.listArticles().subscribe(x => this.articles = x);
  }
}
