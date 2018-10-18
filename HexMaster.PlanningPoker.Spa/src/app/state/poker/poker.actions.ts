import { Action } from '@ngrx/store';
import {
  PokerSessionJoinRequest,
  PokerSession,
  PokerSessionCreateRequest,
  Estimation,
  PokerSessionLeaveRequest
} from '../../models/poker.dto';

export const pokerActionTypes = {
  restoreSession: '[PokerActions] restoreSession',
  restoreSessionSuccess: '[PokerActions] restoreSessionSuccess',
  restoreSessionFailed: '[PokerActions] restoreSessionFailed',

  createSession: '[PokerActions] createSession',
  createSessionSuccess: '[PokerActions] createSessionSuccess',
  createSessionFailed: '[PokerActions] createSessionFailed',

  joinSession: '[PokerActions] joinSession',
  joinSessionSuccess: '[PokerActions] joinSessionSuccess',
  joinSessionFailed: '[PokerActions] joinSessionFailed',

  doRemoveParticipant: '[PokerActions] doRemoveParticipant',
  doStartSession: '[PokerActions] doStartSession',
  doResetSession: '[PokerActions] doResetSession',
  doParticipantEstimate: '[PokerActions] doParticipantEstimate',

  actionGenericSucceeded: '[PokerActions] actionGenericSucceeded',
  actionGenericFailed: '[PokerActions] actionGenericFailed',

  liveParticipantAdded: '[PokerActions] liveParticipantAdded',
  liveParticipantLeft: '[PokerActions] liveParticipantLeft',
  liveSessionStarted: '[PokerActions] liveSessionStarted',
  liveSessionReset: '[PokerActions] liveSessionReset',
  liveSessionReveal: '[PokerActions] liveSessionReveal',
  liveParticipantEstimated: '[PokerActions] liveParticipantEstimated'
};

export class RestoreSession implements Action {
  readonly type = pokerActionTypes.restoreSession;
  constructor(public sessionId: string, public participantId: string) {}
}
export class RestoreSessionSuccess implements Action {
  readonly type = pokerActionTypes.restoreSessionSuccess;
  constructor(public session: PokerSession) {}
}
export class RestoreSessionFailed implements Action {
  readonly type = pokerActionTypes.restoreSessionFailed;
  constructor(public error: any) {}
}

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

export class DoParticipantEstimate implements Action {
  readonly type = pokerActionTypes.doParticipantEstimate;
  constructor(public model: Estimation) {}
}

export class DoStartSession implements Action {
  readonly type = pokerActionTypes.doStartSession;
  constructor(public sessionId: string) {}
}
export class DoResetSession implements Action {
  readonly type = pokerActionTypes.doResetSession;
  constructor(public sessionId: string) {}
}
export class DoRemoveParticipant implements Action {
  readonly type = pokerActionTypes.doRemoveParticipant;
  constructor(public model: PokerSessionLeaveRequest) {}
}
export class ActionGenericSucceeded implements Action {
  readonly type = pokerActionTypes.actionGenericSucceeded;
  constructor() {}
}
export class ActionGenericFailed implements Action {
  readonly type = pokerActionTypes.actionGenericFailed;
  constructor(public error: any) {}
}
export class LiveParticipantAdded implements Action {
  readonly type = pokerActionTypes.liveParticipantAdded;
  constructor(public id: string, public name: string) {}
}
export class LiveParticipantLeft implements Action {
  readonly type = pokerActionTypes.liveParticipantLeft;
  constructor(public id: string) {}
}
export class LiveSessionStarted implements Action {
  readonly type = pokerActionTypes.liveSessionStarted;
  constructor() {}
}
export class LiveSessionReset implements Action {
  readonly type = pokerActionTypes.liveSessionReset;
  constructor() {}
}
export class LiveSessionReveal implements Action {
  readonly type = pokerActionTypes.liveSessionReveal;
  constructor() {}
}
export class LiveParticipantEstimated implements Action {
  readonly type = pokerActionTypes.liveParticipantEstimated;
  constructor(public id: string, public estimation: number) {}
}
