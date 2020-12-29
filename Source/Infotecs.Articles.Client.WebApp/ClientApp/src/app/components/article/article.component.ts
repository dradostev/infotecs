import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { switchMap } from 'rxjs/operators';
import { Article } from '../../models/Article';
import { ArticleService } from '../../services/article.service';
import { Comment } from '../../models/Comment';
import { SignalService } from '../../services/signal.service';

@Component({
  selector: 'app-article',
  templateUrl: './article.component.html',
  styleUrls: ['./article.component.scss']
})
export class ArticleComponent implements OnInit {
  public article: Article;

  constructor (
    private articleService: ArticleService,
    private route: ActivatedRoute,
    private router: Router,
    private signal: SignalService
  ) { }

  ngOnInit () {
    const article = this.route.paramMap.pipe(
      switchMap((params) => this.articleService.showArticle(Number(params.get('id'))))
    );

    article.subscribe(x => {
      this.article = x;
    });

    this.signal.connection.on(
      'CommentAddedEvent', (x: Comment) => this.article.comments.push(x)
    );
  }

  /**
   * Submit comment
   * @param {Comment} comment
   */
  onSubmitComment (comment: Comment) {
    this.articleService.addComment(comment).subscribe();
  }

  /**
   * Remove Article and navigate to Home page.
   */
  onRemoveArticle () {
    this.articleService.deleteArticle(this.article.id).subscribe(
      () => {
        this.router.navigate(['']);
      }
    );
  }
}
