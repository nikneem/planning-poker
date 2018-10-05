import { UserState, INITIAL_USER_STATE } from './user/user.state';
import { UserReducer } from './user/user.reducers';
import {
  RefinementState,
  INITIAL_REFINEMENT_STATE
} from './refinement/refinement.state';
import { RefinementReducer } from './refinement/refinement.reducers';

export interface AppState {
  userState: UserState;
  refinementState: RefinementState;
}

export const INITIAL_APPSTORE: AppState = {
  userState: INITIAL_USER_STATE,
  refinementState: INITIAL_REFINEMENT_STATE
};

export const reducers = {
  userState: UserReducer,
  refinementState: RefinementReducer
};
