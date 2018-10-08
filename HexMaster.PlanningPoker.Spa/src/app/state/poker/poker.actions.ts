import { Action } from '@ngrx/store';
import { PokerSessionJoinRequest, PokerSession } from '../../models/poker.dto';

export const pokerActionTypes = {
  joinSession: '[PokerActions] joinSession',
  joinSessionSuccess: '[PokerActions] joinSessionSuccess',
  joinSessionFailed: '[PokerActions] joinSessionFailed'
};

export class JoinSession implements Action {
  readonly type = pokerActionTypes.joinSession;
  constructor(public joinSession: PokerSessionJoinRequest) {}
}

export class JoinSessionSuccess implements Action {
  readonly type = pokerActionTypes.joinSessionSuccess;
  constructor(public refinement: PokerSession) {}
}

export class JoinSessionFailed implements Action {
  readonly type = pokerActionTypes.joinSessionFailed;
  constructor(public error: any) {}
}
