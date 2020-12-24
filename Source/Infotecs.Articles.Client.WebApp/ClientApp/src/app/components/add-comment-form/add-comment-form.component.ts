import {
  Component, EventEmitter, Input, OnInit, Output
} from '@angular/core'
import { Comment } from '../../models/Comment'

@Component({
  selector: 'app-add-comment-form',
  templateUrl: './add-comment-form.component.html',
  styleUrls: ['./add-comment-form.component.scss']
})
export class AddCommentFormComponent implements OnInit {
  @Input() public articleId: number;

  @Output() submitComment: EventEmitter<Comment> = new EventEmitter<Comment>();

  public comment: Comment = new Comment();

  ngOnInit () {
  }

  /**
   * Submit newly created Comment and clear the form.
   */
  onSubmit () {
    this.comment.articleId = this.articleId
    this.submitComment.emit(this.comment)
    this.comment = new Comment()
  }
}
