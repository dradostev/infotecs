import {Component, OnInit} from '@angular/core';
import {ArticleService} from "../../services/article.service";
import {Article} from "../../models/Article";
import {SignalService} from "../../services/signal.service";

@Component({
  selector: 'app-sidebar',
  templateUrl: './sidebar.component.html',
  styleUrls: ['./sidebar.component.scss']
})
export class SidebarComponent implements OnInit {
  public articles: Article[];

  constructor(
    private articleService: ArticleService,
    private signal: SignalService) { }

  ngOnInit() {
    this.signal.connection.on('ArticleCreatedEvent',
      (e: Article) => this.articles.push(e));
    this.signal.connection.on('ArticleDeletedEvent',
      (e: Article) => this.articles = this.articles.filter(x => x.id !== e.id));
    this.articleService.listArticles().subscribe(x => this.articles = x);
  }
}
