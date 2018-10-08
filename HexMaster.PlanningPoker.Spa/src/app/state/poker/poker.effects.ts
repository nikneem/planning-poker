import { Injectable } from '@angular/core';
import { Effect, Actions } from '@ngrx/effects';
import { Observable, of } from 'rxjs';
import { Action } from '@ngrx/store';
import { pokerActionTypes, JoinSession } from './poker.actions';

import {
  CreateRefinementSuccess,
  CreateRefinementFailed
} from '../refinement/refinement.actions';

@Injectable()
export class PokerEffects {
  constructor(private actions$: Actions) {}

  @Effect()
  joinSession$: Observable<Action> = this.actions$
    .ofType<JoinSession>(pokerActionTypes.joinSession)
    .debounceTime(500)
    .mergeMap((action) => {
      return null;
      //   this.refinementService
      //     .Post(action.refinement)
      //     .map((data: Refinement) => {
      //       return new CreateRefinementSuccess(data);
      //     })
      //     .catch((err) => of(new CreateRefinementFailed(err)));
    });
}
