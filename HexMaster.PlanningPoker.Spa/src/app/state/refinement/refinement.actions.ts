import { Action } from '@ngrx/store';
import Refinement from '../../models/refinement.dto';

export const refinementActionTypes = {
  getRefinement: '[RefinementActions] getUserProfile',
  getRefinementSuccess: '[RefinementActions] getUserProfileSuccess',
  getRefinementFailed: '[RefinementActions] getUserProfileFailed',

  createRefinement: '[RefinementActions] createRefinement',
  createRefinementSuccess: '[RefinementActions] createRefinementSuccess',
  createRefinementFailed: '[RefinementActions] createRefinementFailed'
};

export class CreateRefinement implements Action {
  readonly type = refinementActionTypes.createRefinement;
  constructor(public refinement: Refinement) {}
}

export class CreateRefinementSuccess implements Action {
  readonly type = refinementActionTypes.createRefinementSuccess;
  constructor(public refinement: Refinement) {}
}

export class CreateRefinementFailed implements Action {
  readonly type = refinementActionTypes.createRefinementFailed;
  constructor(public error: any) {}
}
