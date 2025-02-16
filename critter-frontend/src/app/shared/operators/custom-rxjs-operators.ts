import {
  catchError,
  Observable,
  OperatorFunction,
  switchMap,
  take,
  throwError,
} from 'rxjs';
import { PostOutputDto, PostService } from '../services/service-proxies';

export class CustomRxJsOperators {
  constructor(private postService: PostService) {}

  public updatePost(postId: number): OperatorFunction<any, PostOutputDto> {
    return (source$: Observable<number>) => {
      return source$.pipe(
        take(1),
        catchError(() => throwError('Could not update post')),
        switchMap(() => this.postService.getPost(postId))
      );
    };
  }
}
