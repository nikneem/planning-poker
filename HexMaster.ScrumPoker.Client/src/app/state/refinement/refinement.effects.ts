import { Injectable } from '@angular/core';
import { Actions, Effect } from '@ngrx/effects';
import { Observable, of } from 'rxjs';
import { Action } from '@ngrx/store';
import 'rxjs/Rx';
import { RefinementService } from '../../services/refinement.service';
import {
  refinementActionTypes,
  CreateRefinement,
  CreateRefinementSuccess,
  CreateRefinementFailed
} from './refinement.actions';
import Refinement from '../../models/refinement.dto';

@Injectable()
export class RefinementEffects {
  constructor(
    private actions$: Actions,
    private refinementService: RefinementService
  ) {}

  @Effect()
  createRefinement$: Observable<Action> = this.actions$
    .ofType<CreateRefinement>(refinementActionTypes.createRefinement)
    .debounceTime(500)
    .mergeMap((action) => {
      return this.refinementService
        .Post(action.refinement)
        .map((data: Refinement) => {
          return new CreateRefinementSuccess(data);
        })
        .catch((err) => of(new CreateRefinementFailed(err)));
    });
}
