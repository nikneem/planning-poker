import { UserState, INITIAL_USER_STATE } from './user/user.state';
import { UserReducer } from './user/user.reducers';

export interface AppState {
  userState: UserState;
}

export const INITIAL_APPSTORE: AppState = {
  userState: INITIAL_USER_STATE
};

export const reducers = {
  userState: UserReducer
};
