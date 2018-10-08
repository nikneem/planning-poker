export class PokerSessionJoinRequest {
  public firstName: string;
  public lastName: string;
  public sessionCode: string;
  public constructor(init?: Partial<PokerSessionJoinRequest>) {
    Object.assign(this, init);
  }
}

export class PokerSession {
  public sessionId: string;
  public me: Participant;
  public others: Array<Participant>;
  public constructor(init?: Partial<PokerSession>) {
    Object.assign(this, init);
  }
}

export class Participant {
  public id: string;
  public displayName: string;
  public constructor(init?: PokerSession) {
    Object.assign(this, init);
  }
}
