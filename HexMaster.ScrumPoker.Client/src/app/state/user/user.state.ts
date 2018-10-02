import UserProfile from '../../models/profile.dto';

export interface UserState {
  userProfile: UserProfile;
  isLoading: boolean;
}

export const INITIAL_USER_STATE: UserState = {
  userProfile: null,
  isLoading: false
};
