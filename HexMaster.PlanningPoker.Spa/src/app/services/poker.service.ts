import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import {
  PokerSessionJoinRequest,
  PokerSession,
  PokerSessionCreateRequest
} from '../models/poker.dto';

@Injectable({
  providedIn: 'root'
})
export class PokerSessionService {
  private baseUrl: string;

  constructor(private http: HttpClient) {
    this.baseUrl = environment.backendApi;
  }

  public Join(model: PokerSessionJoinRequest): Observable<PokerSession> {
    return this.http.post<PokerSession>(
      `${this.baseUrl}/pokersessions/join`,
      model
    );
  }
  public Create(model: PokerSessionCreateRequest): Observable<PokerSession> {
    return this.http.post<PokerSession>(`${this.baseUrl}/pokersessions`, model);
  }
  public Get(id: string): Observable<PokerSession> {
    return this.http.get<PokerSession>(`${this.baseUrl}/pokersessions/${id}`);
  }
}
