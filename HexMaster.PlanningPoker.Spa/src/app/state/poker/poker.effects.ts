import { Injectable } from '@angular/core';
import { Effect, Actions } from '@ngrx/effects';
import { Observable, of } from 'rxjs';
import { Action } from '@ngrx/store';
import {
  pokerActionTypes,
  JoinSession,
  JoinSessionSuccess,
  CreateSession,
  CreateSessionFailed,
  CreateSessionSuccess,
  JoinSessionFailed
} from './poker.actions';

import { PokerSessionService } from 'src/app/services/poker.service';
import { PokerSession } from 'src/app/models/poker.dto';
import { Router } from '@angular/router';

@Injectable()
export class PokerEffects {
  constructor(
    private actions$: Actions,
    private service: PokerSessionService,
    private router: Router
  ) {}

  @Effect()
  createSession$: Observable<Action> = this.actions$
    .ofType<CreateSession>(pokerActionTypes.createSession)
    .debounceTime(500)
    .mergeMap((action) => {
      return this.service
        .Create(action.joinSession)
        .map((data: PokerSession) => {
          return new CreateSessionSuccess(data);
        })
        .do(() => this.router.navigate(['/poker/home']))
        .catch((err) => of(new CreateSessionFailed(err)));
    });

  @Effect()
  joinSession$: Observable<Action> = this.actions$
    .ofType<JoinSession>(pokerActionTypes.joinSession)
    .debounceTime(500)
    .mergeMap((action) => {
      return this.service
        .Join(action.joinSession)
        .map((data: PokerSession) => {
          return new JoinSessionSuccess(data);
        })
        .do(() => this.router.navigate(['/poker/home']))
        .catch((err) => of(new JoinSessionFailed(err)));
    });
}
