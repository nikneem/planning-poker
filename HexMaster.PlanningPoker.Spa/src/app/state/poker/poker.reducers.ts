import * as _ from 'lodash';
import { PokerState } from './poker.state';
import {
  pokerActionTypes,
  CreateSession,
  CreateSessionSuccess,
  CreateSessionFailed,
  JoinSessionSuccess,
  JoinSessionFailed,
  RestoreSession,
  RestoreSessionSuccess,
  RestoreSessionFailed,
  LiveParticipantAdded,
  LiveParticipantLeft,
  LiveSessionReset,
  LiveSessionStarted,
  LiveParticipantEstimated,
  ActionGenericSucceeded,
  ActionGenericFailed,
  DoParticipantEstimate
} from './poker.actions';
import { Participant, PokerSession } from 'src/app/models/poker.dto';

export function PokerReducer(state: PokerState, action: any) {
  {
    switch (action.type) {
      case pokerActionTypes.restoreSession:
        return restoreSessionHandler(state, action);
      case pokerActionTypes.restoreSessionSuccess:
        return restoreSessionSuccessHandler(state, action);
      case pokerActionTypes.restoreSessionFailed:
        return restoreSessionFailedHandler(state, action);
      case pokerActionTypes.createSession:
        return createSessionHandler(state, action);
      case pokerActionTypes.createSessionSuccess:
        return createSessionSuccessHandler(state, action);
      case pokerActionTypes.createSessionFailed:
        return createSessionFailedHandler(state, action);
      case pokerActionTypes.joinSession:
        return joinSessionHandler(state, action);
      case pokerActionTypes.joinSessionSuccess:
        return joinSessionSuccessHandler(state, action);
      case pokerActionTypes.joinSessionFailed:
        return joinSessionFailedHandler(state, action);

      case pokerActionTypes.doParticipantEstimate:
        return doParticipantEstimateHandler(state, action);

      case pokerActionTypes.actionGenericSucceeded:
        return actionGenericSucceededHandler(state, action);
      case pokerActionTypes.actionGenericFailed:
        return actionGenericFailedHandler(state, action);

      case pokerActionTypes.liveParticipantAdded:
        return liveParticipantAddedHandler(state, action);
      case pokerActionTypes.liveParticipantLeft:
        return liveParticipantLeftHandler(state, action);
      case pokerActionTypes.liveSessionReset:
        return liveSessionResetHandler(state, action);
      case pokerActionTypes.liveSessionStarted:
        return liveSessionStartedHandler(state, action);
      case pokerActionTypes.liveParticipantEstimated:
        return liveParticipantEstimatedHandler(state, action);
      default:
        return state;
    }
  }
}

function restoreSessionHandler(
  state: PokerState,
  action: RestoreSession
): PokerState {
  const copyState: PokerState = Object.assign({}, state);
  copyState.isLoading = true;
  copyState.lastKnownError = null;
  return copyState;
}
function restoreSessionSuccessHandler(
  state: PokerState,
  action: RestoreSessionSuccess
): PokerState {
  const copyState: PokerState = Object.assign({}, state);
  copyState.isLoading = false;
  copyState.lastKnownError = null;
  copyState.currentSession = action.session;
  return copyState;
}
function restoreSessionFailedHandler(
  state: PokerState,
  action: RestoreSessionFailed
): PokerState {
  const copyState: PokerState = Object.assign({}, state);
  copyState.isLoading = false;
  copyState.lastKnownError = action.error.message;
  copyState.currentSession = null;
  return copyState;
}

function createSessionHandler(
  state: PokerState,
  action: CreateSession
): PokerState {
  const copyState: PokerState = Object.assign({}, state);
  copyState.isLoading = true;
  copyState.lastKnownError = null;
  return copyState;
}

function createSessionSuccessHandler(
  state: PokerState,
  action: CreateSessionSuccess
): PokerState {
  const copyState: PokerState = Object.assign({}, state);
  copyState.isLoading = false;
  copyState.currentSession = action.session;
  return copyState;
}

function createSessionFailedHandler(
  state: PokerState,
  action: CreateSessionFailed
): PokerState {
  const copyState: PokerState = Object.assign({}, state);
  copyState.isLoading = false;
  copyState.currentSession = null;
  copyState.lastKnownError = action.error.message;
  return copyState;
}

function joinSessionHandler(
  state: PokerState,
  action: CreateSession
): PokerState {
  const copyState: PokerState = Object.assign({}, state);
  copyState.isLoading = true;
  copyState.lastKnownError = null;
  return copyState;
}

function joinSessionSuccessHandler(
  state: PokerState,
  action: JoinSessionSuccess
): PokerState {
  const copyState: PokerState = Object.assign({}, state);
  copyState.isLoading = false;
  copyState.currentSession = action.pokerSession;
  return copyState;
}

function joinSessionFailedHandler(
  state: PokerState,
  action: JoinSessionFailed
): PokerState {
  const copyState: PokerState = Object.assign({}, state);
  copyState.isLoading = false;
  copyState.currentSession = null;
  copyState.lastKnownError = action.error.message;
  return copyState;
}

function doParticipantEstimateHandler(
  state: PokerState,
  action: DoParticipantEstimate
): PokerState {
  const copyState: PokerState = Object.assign({}, state);

  var targetState = _.cloneDeep(copyState.currentSession) as PokerSession;
  targetState.me.estimation = action.model.estimation;
  copyState.currentSession = targetState;

  return copyState;
}

function actionGenericSucceededHandler(
  state: PokerState,
  action: ActionGenericSucceeded
): PokerState {
  const copyState: PokerState = Object.assign({}, state);
  copyState.lastKnownError = null;
  return copyState;
}
function actionGenericFailedHandler(
  state: PokerState,
  action: ActionGenericFailed
): PokerState {
  const copyState: PokerState = Object.assign({}, state);
  copyState.lastKnownError = action.error.message;
  return copyState;
}

function liveParticipantAddedHandler(
  state: PokerState,
  action: LiveParticipantAdded
): PokerState {
  const copyState: PokerState = Object.assign({}, state);

  var targetState = _.cloneDeep(copyState.currentSession);
  var originalEntries = copyState.currentSession.others as Array<Participant>;
  let newEntries = new Array<Participant>(...originalEntries);
  newEntries.push(new Participant({ id: action.id, displayName: action.name }));
  targetState.others = newEntries;

  copyState.currentSession = targetState;

  return copyState;
}

function liveParticipantLeftHandler(
  state: PokerState,
  action: LiveParticipantLeft
): PokerState {
  const copyState: PokerState = Object.assign({}, state);

  var targetState = _.cloneDeep(copyState.currentSession);
  var originalEntries = copyState.currentSession.others as Array<Participant>;
  let newEntries = new Array<Participant>(...originalEntries);

  const entry = _.find(newEntries, { id: action.id });
  const index = newEntries.indexOf(entry, 0);
  if (index > -1) {
    newEntries.splice(index, 1);
  }
  targetState.others = newEntries;

  copyState.currentSession = targetState;

  return copyState;
}
function liveSessionResetHandler(
  state: PokerState,
  action: LiveSessionReset
): PokerState {
  const copyState: PokerState = Object.assign({}, state);

  var targetState = _.cloneDeep(copyState.currentSession) as PokerSession;
  for (let index = 0; index < targetState.others.length; index++) {
    targetState.others[index].estimation = null;
  }

  copyState.currentSession = targetState;

  return copyState;
}
function liveSessionStartedHandler(
  state: PokerState,
  action: LiveSessionStarted
): PokerState {
  const copyState: PokerState = Object.assign({}, state);

  var targetState = _.cloneDeep(copyState.currentSession) as PokerSession;
  targetState.isStarted = true;
  copyState.currentSession = targetState;

  return copyState;
}
function liveParticipantEstimatedHandler(
  state: PokerState,
  action: LiveParticipantEstimated
): PokerState {
  const copyState: PokerState = Object.assign({}, state);

  var targetState = _.cloneDeep(copyState.currentSession);
  var originalEntries = targetState.others as Array<Participant>;
  let newEntries = new Array<Participant>(...originalEntries);

  const entry = _.find(newEntries, { id: action.id }) as Participant;
  if (typeof entry !== 'undefined') {
    entry.estimation = action.estimation;
  }

  copyState.currentSession = targetState;
  return copyState;
}
