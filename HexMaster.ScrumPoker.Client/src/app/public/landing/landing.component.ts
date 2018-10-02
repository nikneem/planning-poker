import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../services/auth.service';
import { Store } from '@ngrx/store';
import { AppState } from '../../state/app.state';
import { GetUserProfile } from '../../state/user/user.actions';
@Component({
  selector: 'app-landing',
  templateUrl: './landing.component.html',
  styleUrls: ['./landing.component.scss']
})
export class LandingComponent implements OnInit {
  constructor(
    private authService: AuthService,
    private store: Store<AppState>
  ) {}

  isAuthenticated(): boolean {
    return this.authService.isAuthenticated();
  }
  authenticate() {
    this.authService.login();
  }

  ngOnInit() {
    if (this.isAuthenticated()) {
      this.store.dispatch(new GetUserProfile());
    }
  }
}
