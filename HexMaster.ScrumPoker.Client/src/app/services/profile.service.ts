import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import UserProfile from '../models/profile.dto';

@Injectable({
  providedIn: 'root'
})
export class ProfileService {
  constructor(private http: HttpClient) {}

  public GetUserProfile(): Observable<UserProfile> {
    return this.http.get<UserProfile>(
      'https://planning-poker.eu.auth0.com/userinfo'
    );
  }
}
