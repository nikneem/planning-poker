import * as _ from 'lodash';
import { PokerState } from './poker.state';
import {
  pokerActionTypes,
  CreateSession,
  CreateSessionSuccess,
  CreateSessionFailed,
  JoinSessionSuccess,
  JoinSessionFailed,
  AddParticipant,
  RestoreSession,
  RestoreSessionSuccess,
  RestoreSessionFailed
} from './poker.actions';
import { Participant } from 'src/app/models/poker.dto';

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
      case pokerActionTypes.addParticipant:
        return addParticipantHandler(state, action);
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
  copyState.lastKnownError = action.error;
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

function addParticipantHandler(
  state: PokerState,
  action: AddParticipant
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
