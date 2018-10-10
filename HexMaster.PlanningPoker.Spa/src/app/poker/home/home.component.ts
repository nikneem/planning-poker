import { Component, OnInit } from '@angular/core';
import { Store } from '@ngrx/store';
import { AppState } from 'src/app/state/app.state';
import {
  PokerSession,
  PokerSessionCreateRequest
} from 'src/app/models/poker.dto';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { CreateSession } from 'src/app/state/poker/poker.actions';
import { HubConnection } from '@aspnet/signalr';
import * as signalR from '@aspnet/signalr';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {
  pokerSession: PokerSession;
  isLoading: boolean = true;
  errorMessage: string;
  createForm: FormGroup;
  pokerSessionId: string;

  private pokerSessionHubConnection: HubConnection | undefined;

  constructor(private store: Store<AppState>, private fb: FormBuilder) {
    const self = this;
    this.store
      .select((state) => state.pokerState.currentSession)
      .subscribe((value) => {
        self.pokerSession = value;
        if (self.pokerSession !== null) {
          if (self.pokerSession.id !== self.pokerSessionId) {
            self.registerSignalRListener();
          }
          self.pokerSessionId = self.pokerSession.id;
        }
      });
    this.store
      .select((state) => state.pokerState.isLoading)
      .subscribe((value) => (self.isLoading = value));
    this.store
      .select((state) => state.pokerState.lastKnownError)
      .subscribe((value) => (self.errorMessage = value));
  }

  initializeForm() {
    const firstName = localStorage.getItem('firstName');
    const lastName = localStorage.getItem('lastName');
    this.createForm = this.fb.group({
      firstName: [firstName, [Validators.required]],
      lastName: [lastName],
      sessionName: ['', [Validators.required]],
      controlType: ['shared'],
      startType: ['automatically']
    });
  }

  registerSignalRListener() {
    const self = this;

    this.pokerSessionHubConnection = new signalR.HubConnectionBuilder()
      .withUrl('http://localhost:54403/pokersession')
      .configureLogging(signalR.LogLevel.Information)
      .build();

    this.pokerSessionHubConnection
      .start()
      .then(() => {
        // self.pokerSessionHubConnection.on(
        //   'notificationReceived',
        //   (headline: string, message: string) => {
        //     console.log(`Data received over SignalR: ${headline}`);
        //     self.toastr.success(message, headline);
        //   }
        // );

        self.pokerSessionHubConnection.invoke(
          'RegisterParticipant',
          self.pokerSession.sessionCode
        );
      })
      .catch((err) => console.error(err.toString()));
  }

  submitCreate() {
    const createRequest = new PokerSessionCreateRequest(this.createForm.value);
    console.log(createRequest);
    this.store.dispatch(new CreateSession(createRequest));
  }

  ngOnInit() {
    this.initializeForm();
  }
}
