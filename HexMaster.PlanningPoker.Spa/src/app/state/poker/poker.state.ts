import { PokerSession } from '../../models/poker.dto';

export interface PokerState {
  currentSession: PokerSession;
  isLoading: boolean;
  lastKnownError?: string;
}

export const INITIAL_POKER_STATE: PokerState = {
  currentSession: null,
  isLoading: false
};
