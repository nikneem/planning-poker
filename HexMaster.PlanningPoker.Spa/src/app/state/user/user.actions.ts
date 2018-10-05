import { Action } from '@ngrx/store';
import UserProfile from '../../models/profile.dto';

export const userActionTypes = {
  getUserProfile: '[UserActions] getUserProfile',
  getUserProfileSuccess: '[UserActions] getUserProfileSuccess',
  getUserProfileFailed: '[UserActions] getUserProfileFailed'
};

export class GetUserProfile implements Action {
  readonly type = userActionTypes.getUserProfile;
  constructor() {}
}

export class GetUserProfileSuccess implements Action {
  readonly type = userActionTypes.getUserProfileSuccess;
  constructor(public model: UserProfile) {}
}

export class GetUserProfileFailed implements Action {
  readonly type = userActionTypes.getUserProfileFailed;
  constructor() {}
}
