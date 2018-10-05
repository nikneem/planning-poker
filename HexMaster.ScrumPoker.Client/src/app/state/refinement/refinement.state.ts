import Refinement from '../../models/refinement.dto';

export interface RefinementState {
  currentRefinement: Refinement;
  isLoading: boolean;
  lastKnownError?: string;
}

export const INITIAL_REFINEMENT_STATE: RefinementState = {
  currentRefinement: null,
  isLoading: false
};
