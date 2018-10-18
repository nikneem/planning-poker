import { PokerSession } from '../../models/poker.dto';

export interface PokerState {
  currentSession: PokerSession;
  isLoading: boolean;
  gotKicked: boolean;
  lastKnownError?: string;
}

export const INITIAL_POKER_STATE: PokerState = {
  currentSession: null,
  gotKicked: false,
  isLoading: false
};
