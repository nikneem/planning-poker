import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import {
  PokerSessionJoinRequest,
  PokerSession,
  PokerSessionCreateRequest,
  PokerSessionLeaveRequest,
  Estimation
} from '../models/poker.dto';

@Injectable({
  providedIn: 'root'
})
export class PokerSessionService {
  private baseUrl: string;

  constructor(private http: HttpClient) {
    this.baseUrl = environment.backendApi;
  }

  public Restore(
    sessionId: string,
    participantId: string
  ): Observable<PokerSession> {
    return this.http.get<PokerSession>(
      `${this.baseUrl}/pokersessions/${sessionId}${participantId}`
    );
  }

  public Estimate(model: Estimation): Observable<number> {
    return this.http.post<number>(`${this.baseUrl}/estimations`, model);
  }

  public Join(model: PokerSessionJoinRequest): Observable<PokerSession> {
    return this.http.post<PokerSession>(
      `${this.baseUrl}/pokersessions/join`,
      model
    );
  }

  public Leave(model: PokerSessionLeaveRequest): Observable<boolean> {
    return this.http.post<boolean>(
      `${this.baseUrl}/pokersessions/leave`,
      model
    );
  }

  public Start(pokerSessionId: string): Observable<boolean> {
    return this.http.get<boolean>(
      `${this.baseUrl}/pokersessions/${pokerSessionId}/start`
    );
  }

  public Reset(pokerSessionId: string): Observable<boolean> {
    return this.http.get<boolean>(
      `${this.baseUrl}/pokersessions/${pokerSessionId}/reset`
    );
  }

  public Create(model: PokerSessionCreateRequest): Observable<PokerSession> {
    return this.http.post<PokerSession>(`${this.baseUrl}/pokersessions`, model);
  }
  public Get(id: string): Observable<PokerSession> {
    return this.http.get<PokerSession>(`${this.baseUrl}/pokersessions/${id}`);
  }
}
