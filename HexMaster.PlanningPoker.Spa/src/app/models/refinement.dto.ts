export default class Refinement {
  public name: string;
  public invitees: Array<Invitee>;
  public ProductBacklogItems: Array<Pbi>;
  public constructor(init?: Partial<Refinement>) {
    Object.assign(this, init);
  }
}

export class Pbi {
  public title: string;
  public description: string;
  public url: string;
}
export class Invitee {
  public name: string;
  public email: string;
}
