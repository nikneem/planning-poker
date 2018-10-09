import { PokerState } from './poker.state';
import {
  pokerActionTypes,
  CreateSession,
  CreateSessionSuccess,
  CreateSessionFailed
} from './poker.actions';

export function PokerReducer(state: PokerState, action: any) {
  {
    switch (action.type) {
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
      default:
        return state;
    }
  }
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
  action: CreateSessionSuccess
): PokerState {
  const copyState: PokerState = Object.assign({}, state);
  copyState.isLoading = false;
  copyState.currentSession = action.session;
  return copyState;
}

function joinSessionFailedHandler(
  state: PokerState,
  action: CreateSessionFailed
): PokerState {
  const copyState: PokerState = Object.assign({}, state);
  copyState.isLoading = false;
  copyState.currentSession = null;
  copyState.lastKnownError = action.error.message;
  return copyState;
}
