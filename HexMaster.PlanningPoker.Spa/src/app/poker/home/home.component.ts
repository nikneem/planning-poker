import { Component, OnInit } from '@angular/core';
import { Store } from '@ngrx/store';
import { AppState } from 'src/app/state/app.state';
import { PokerSession } from 'src/app/models/poker.dto';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {
  pokerSession: PokerSession;
  isLoading: boolean = true;
  errorMessage: string;

  constructor(private store: Store<AppState>) {
    const self = this;
    this.store
      .select((state) => state.pokerState.currentSession)
      .subscribe((value) => (self.pokerSession = value));
    this.store
      .select((state) => state.pokerState.isLoading)
      .subscribe((value) => (self.isLoading = value));
    this.store
      .select((state) => state.pokerState.lastKnownError)
      .subscribe((value) => (self.errorMessage = value));
  }

  ngOnInit() {}
}
