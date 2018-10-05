import { UserState } from './user.state';
import {
  userActionTypes,
  GetUserProfile,
  GetUserProfileSuccess
} from './user.actions';

export function UserReducer(state: UserState, action: any) {
  {
    switch (action.type) {
      case userActionTypes.getUserProfile:
        return getUserProfileHandler(state, action);
      case userActionTypes.getUserProfileSuccess:
        return getUserProfileSuccessHandler(state, action);
      default:
        return state;
    }
  }
}

function getUserProfileHandler(
  state: UserState,
  action: GetUserProfile
): UserState {
  const copyState: UserState = Object.assign({}, state);
  copyState.isLoading = true;
  return copyState;
}
function getUserProfileSuccessHandler(
  state: UserState,
  action: GetUserProfileSuccess
): UserState {
  const copyState: UserState = Object.assign({}, state);
  copyState.isLoading = false;
  copyState.userProfile = action.model;
  return copyState;
}
