export class PokerSessionJoinRequest {
  public firstName: string;
  public lastName: string;
  public sessionCode: string;
  public constructor(init?: Partial<PokerSessionJoinRequest>) {
    Object.assign(this, init);
  }
}

export class PokerSessionCreateRequest {
  public firstName: string;
  public lastName: string;
  public sessionName: string;
  public controlType: string;
  public startType: string;
  public constructor(init?: Partial<PokerSessionCreateRequest>) {
    Object.assign(this, init);
  }
}

export class PokerSession {
  public sessionId: string;
  public sessionName: string;
  public me: Participant;
  public others: Array<Participant>;
  public lastActivity: Date;
  public firstActivity: Date;
  public constructor(init?: Partial<PokerSession>) {
    Object.assign(this, init);
  }
}

export class Participant {
  public id: string;
  public displayName: string;
  public estimation?: number;
  public canEdit: boolean;
  public constructor(init?: Partial<Participant>) {
    Object.assign(this, init);
  }
}

export class Estimation {
  public sessionId: string;
  public participantId: string;
  public estimation: number;
  public constructot(init?: Partial<Estimation>) {
    Object.assign(this, init);
  }
}
