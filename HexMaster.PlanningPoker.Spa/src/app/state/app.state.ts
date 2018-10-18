import {
  RefinementState,
  INITIAL_REFINEMENT_STATE
} from './refinement/refinement.state';
import { RefinementReducer } from './refinement/refinement.reducers';
import { PokerState, INITIAL_POKER_STATE } from './poker/poker.state';
import { PokerReducer } from './poker/poker.reducers';
import { routerReducer } from '@ngrx/router-store';
export interface AppState {
  pokerState: PokerState;
  refinementState: RefinementState;
}

export const INITIAL_APPSTORE: AppState = {
  pokerState: INITIAL_POKER_STATE,
  refinementState: INITIAL_REFINEMENT_STATE
};

export const reducers = {
  pokerState: PokerReducer,
  refinementState: RefinementReducer,
  routerReducer: routerReducer
};
