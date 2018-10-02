import { Injectable } from '@angular/core';
import { Actions, Effect } from '@ngrx/effects';
import { Observable, of } from 'rxjs';
import { Action } from '@ngrx/store';
import 'rxjs/Rx';
import { ProfileService } from '../../services/profile.service';
import UserProfile from '../../models/profile.dto';
import {
  GetUserProfile,
  GetUserProfileFailed,
  GetUserProfileSuccess,
  userActionTypes
} from './user.actions';

@Injectable()
export class UserEffects {
  constructor(
    private actions$: Actions,
    private profileService: ProfileService
  ) {}

  @Effect()
  getUserProfile$: Observable<Action> = this.actions$
    .ofType<GetUserProfile>(userActionTypes.getUserProfile)
    .debounceTime(500)
    .mergeMap((action) => {
      return this.profileService
        .GetUserProfile()
        .map((data: UserProfile) => {
          return new GetUserProfileSuccess(data);
        })
        .catch(() => of(new GetUserProfileFailed()));
    });
}
