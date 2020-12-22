import { Component, OnInit } from '@angular/core';
import {ArticleService} from "../../services/article.service";
import {Article} from "../../models/Article";

@Component({
  selector: 'app-sidebar',
  templateUrl: './sidebar.component.html',
  styleUrls: ['./sidebar.component.css']
})
export class SidebarComponent implements OnInit {
  public articles: Article[];

  constructor(private articleService: ArticleService) { }

  ngOnInit() {
    this.articleService.listArticles().subscribe(x => this.articles = x);
  }

}
