import { UserState, INITIAL_USER_STATE } from './user/user.state';
import { UserReducer } from './user/user.reducers';
import {
  RefinementState,
  INITIAL_REFINEMENT_STATE
} from './refinement/refinement.state';
import { RefinementReducer } from './refinement/refinement.reducers';
import { PokerState, INITIAL_POKER_STATE } from './poker/poker.state';
import { PokerReducer } from './poker/poker.reducers';

export interface AppState {
  userState: UserState;
  pokerState: PokerState;
  refinementState: RefinementState;
}

export const INITIAL_APPSTORE: AppState = {
  userState: INITIAL_USER_STATE,
  pokerState: INITIAL_POKER_STATE,
  refinementState: INITIAL_REFINEMENT_STATE
};

export const reducers = {
  userState: UserReducer,
  pokerState: PokerReducer,
  refinementState: RefinementReducer
};
