import { Component, OnInit, HostListener, Injectable } from '@angular/core';
import { Store } from '@ngrx/store';
import { AppState } from 'src/app/state/app.state';
import {
  PokerSession,
  PokerSessionCreateRequest,
  Estimation,
  PokerSessionLeaveRequest,
  Participant
} from 'src/app/models/poker.dto';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import {
  CreateSession,
  RestoreSession,
  LiveParticipantAdded,
  LiveParticipantLeft,
  LiveParticipantEstimated,
  LiveSessionStarted,
  LiveSessionReset,
  DoParticipantEstimate,
  DoStartSession,
  DoRemoveParticipant,
  LiveSessionReveal,
  DoResetSession
} from 'src/app/state/poker/poker.actions';
import { HubConnection } from '@aspnet/signalr';
import * as signalR from '@aspnet/signalr';
import { CanDeactivate, Router, ActivatedRoute } from '@angular/router';
import { environment } from 'src/environments/environment';

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
  canEdit: boolean;
  isStarted: boolean;
  gotKicked: boolean;
  isRevealed: boolean;
  showMetrics: boolean;

  private pokerSessionHubConnection: HubConnection | undefined;

  constructor(
    private store: Store<AppState>,
    private fb: FormBuilder,
    private route: ActivatedRoute
  ) {
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
          self.canEdit = self.pokerSession.me.canEdit;
          self.isStarted = self.pokerSession.isStarted;
        }
      });
    this.store
      .select((state) => state.pokerState.isLoading)
      .subscribe((value) => (self.isLoading = value));
    this.store
      .select((state) => state.pokerState.lastKnownError)
      .subscribe((value) => (self.errorMessage = value));
    this.store
      .select((state) => state.pokerState.gotKicked)
      .subscribe((value) => (self.gotKicked = value));
    this.store
      .select((state) => state.pokerState.revealed)
      .subscribe((value) => (self.isRevealed = value));
  }
  @HostListener('window:beforeunload', ['$event'])
  unloadNotification($event: any) {
    this.unregisterSignalRListener();
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

  startCurrentSession() {
    this.store.dispatch(new DoStartSession(this.pokerSessionId));
  }
  participantEstimate(estimation: number) {
    const model = new Estimation({
      sessionId: this.pokerSessionId,
      participantId: this.pokerSession.me.id,
      estimation: estimation
    });
    this.store.dispatch(new DoParticipantEstimate(model));
  }
  kickParticipant(id: string) {
    const model = new PokerSessionLeaveRequest({
      sessionId: this.pokerSessionId,
      participantId: id
    });
    this.store.dispatch(new DoRemoveParticipant(model));
  }
  resetRound() {
    this.store.dispatch(new DoResetSession(this.pokerSessionId));
  }

  registerSignalRListener() {
    const self = this;

    this.pokerSessionHubConnection = new signalR.HubConnectionBuilder()
      .withUrl(`${environment.liveApi}/pokersession`)
      .configureLogging(signalR.LogLevel.Information)
      .build();

    this.pokerSessionHubConnection
      .start()
      .then(() => {
        self.pokerSessionHubConnection.on(
          'participantJoined',
          (id: string, name: string) => {
            console.log(`New participant joined: ${name}`);
            self.store.dispatch(new LiveParticipantAdded(id, name));
          }
        );
        self.pokerSessionHubConnection.on('participantLeft', (id: string) => {
          console.log(`Participant left: ${id}`);
          self.store.dispatch(new LiveParticipantLeft(id));
        });

        self.pokerSessionHubConnection.on(
          'participantEstimation',
          (participantId: string, estimation: number) => {
            self.store.dispatch(
              new LiveParticipantEstimated(participantId, estimation)
            );
          }
        );
        self.pokerSessionHubConnection.on('start', () => {
          self.store.dispatch(new LiveSessionStarted());
        });
        self.pokerSessionHubConnection.on('reset', () => {
          self.store.dispatch(new LiveSessionReset());
        });
        self.pokerSessionHubConnection.on('reveal', () => {
          self.store.dispatch(new LiveSessionReveal());
        });

        self.pokerSessionHubConnection.invoke(
          'RegisterParticipant',
          self.pokerSession.id
        );
      })
      .catch((err) => console.error(err.toString()));
  }
  unregisterSignalRListener() {
    if (this.pokerSessionHubConnection) {
      this.pokerSessionHubConnection.stop();
      this.pokerSessionHubConnection = null;
    }
  }

  submitCreate() {
    const createRequest = new PokerSessionCreateRequest(this.createForm.value);
    console.log(createRequest);
    this.store.dispatch(new CreateSession(createRequest));
  }

  getCardName(p: Participant): string {
    if (p.estimation > 0 && p.estimation < 1) {
      return 'half';
    }
    return p.estimation.toString();
  }

  ngOnInit() {
    this.initializeForm();
    const sessionId = this.route.snapshot.params.sessionId;
    const participantId = this.route.snapshot.params.participantId;
    if (
      typeof sessionId !== 'undefined' &&
      typeof participantId !== 'undefined'
    ) {
      if (this.pokerSessionId !== sessionId) {
        this.store.dispatch(new RestoreSession(sessionId, participantId));
      }
    }
  }
}

@Injectable()
export class CanDeactivateGuard implements CanDeactivate<HomeComponent> {
  canDeactivate(component: HomeComponent): boolean {
    component.unregisterSignalRListener();
    return true;
  }
}
