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
  JoinSessionFailed,
  RestoreSessionSuccess,
  RestoreSession,
  RestoreSessionFailed
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
  restoreSession$: Observable<Action> = this.actions$
    .ofType<RestoreSession>(pokerActionTypes.restoreSession)
    .debounceTime(500)
    .mergeMap((action) => {
      return this.service
        .Restore(action.sessionId, action.participantId)
        .map((data: PokerSession) => {
          return new RestoreSessionSuccess(data);
        })
        .catch((err) => of(new RestoreSessionFailed(err)));
    });

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
        .catch((err) => of(new JoinSessionFailed(err)));
    });
}
