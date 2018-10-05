import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import Refinement from '../models/refinement.dto';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class RefinementService {
  private baseUrl: string;

  constructor(private http: HttpClient) {
    this.baseUrl = environment.backendApi;
  }

  public Post(model: Refinement): Observable<Refinement> {
    return this.http.post<Refinement>(`${this.baseUrl}/api/refinements`, model);
  }
}
