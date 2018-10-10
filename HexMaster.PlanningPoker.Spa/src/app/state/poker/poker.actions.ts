import { Action } from '@ngrx/store';
import {
  PokerSessionJoinRequest,
  PokerSession,
  PokerSessionCreateRequest
} from '../../models/poker.dto';

export const pokerActionTypes = {
  createSession: '[PokerActions] createSession',
  createSessionSuccess: '[PokerActions] createSessionSuccess',
  createSessionFailed: '[PokerActions] createSessionFailed',

  joinSession: '[PokerActions] joinSession',
  joinSessionSuccess: '[PokerActions] joinSessionSuccess',
  joinSessionFailed: '[PokerActions] joinSessionFailed'
};

export class CreateSession implements Action {
  readonly type = pokerActionTypes.createSession;
  constructor(public joinSession: PokerSessionCreateRequest) {}
}
export class CreateSessionSuccess implements Action {
  readonly type = pokerActionTypes.createSessionSuccess;
  constructor(public session: PokerSession) {}
}
export class CreateSessionFailed implements Action {
  readonly type = pokerActionTypes.createSessionFailed;
  constructor(public error: any) {}
}

export class JoinSession implements Action {
  readonly type = pokerActionTypes.joinSession;
  constructor(public joinSession: PokerSessionJoinRequest) {}
}

export class JoinSessionSuccess implements Action {
  readonly type = pokerActionTypes.joinSessionSuccess;
  constructor(public pokerSession: PokerSession) {}
}

export class JoinSessionFailed implements Action {
  readonly type = pokerActionTypes.joinSessionFailed;
  constructor(public error: any) {}
}
