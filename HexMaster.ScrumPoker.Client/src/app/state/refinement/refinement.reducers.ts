import { RefinementState } from './refinement.state';
import {
  refinementActionTypes,
  CreateRefinement,
  CreateRefinementSuccess,
  CreateRefinementFailed
} from './refinement.actions';

export function RefinementReducer(state: RefinementState, action: any) {
  {
    switch (action.type) {
      case refinementActionTypes.createRefinement:
        return createRefinementHandler(state, action);
      case refinementActionTypes.createRefinementSuccess:
        return createRefinementSuccessHandler(state, action);
      case refinementActionTypes.createRefinementFailed:
        return createRefinementFailedHandler(state, action);
      default:
        return state;
    }
  }
}

function createRefinementHandler(
  state: RefinementState,
  action: CreateRefinement
): RefinementState {
  const copyState: RefinementState = Object.assign({}, state);
  copyState.isLoading = true;
  copyState.lastKnownError = null;
  return copyState;
}

function createRefinementSuccessHandler(
  state: RefinementState,
  action: CreateRefinementSuccess
): RefinementState {
  const copyState: RefinementState = Object.assign({}, state);
  copyState.isLoading = false;
  copyState.currentRefinement = action.refinement;
  return copyState;
}

function createRefinementFailedHandler(
  state: RefinementState,
  action: CreateRefinementFailed
): RefinementState {
  const copyState: RefinementState = Object.assign({}, state);
  copyState.isLoading = false;
  copyState.currentRefinement = null;
  copyState.lastKnownError = action.error.message;
  return copyState;
}
