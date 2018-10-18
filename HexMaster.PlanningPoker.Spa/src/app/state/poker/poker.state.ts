import { PokerSession } from '../../models/poker.dto';

export interface PokerState {
  currentSession: PokerSession;
  isLoading: boolean;
  gotKicked: boolean;
  revealed: boolean;
  lastKnownError?: string;
}

export const INITIAL_POKER_STATE: PokerState = {
  currentSession: null,
  gotKicked: false,
  revealed: false,
  isLoading: false
};
