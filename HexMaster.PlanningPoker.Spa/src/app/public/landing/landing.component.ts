import { Component, OnInit } from '@angular/core';
import { Store } from '@ngrx/store';
import { AppState } from '../../state/app.state';
import { GetUserProfile } from '../../state/user/user.actions';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import {
  PokerSessionJoinRequest,
  PokerSessionCreateRequest
} from 'src/app/models/poker.dto';
import { JoinSession, CreateSession } from 'src/app/state/poker/poker.actions';
import { Router } from '@angular/router';
@Component({
  selector: 'app-landing',
  templateUrl: './landing.component.html',
  styleUrls: ['./landing.component.scss']
})
export class LandingComponent implements OnInit {
  joinForm: FormGroup;
  createForm: FormGroup;
  firstName: string;
  constructor(
    private router: Router,
    private store: Store<AppState>,
    private fb: FormBuilder
  ) {
    const self = this;

    this.store
      .select((str) => str.pokerState.currentSession)
      .filter((val) => val != null)
      .subscribe((val) => {
        debugger;
        self.router.navigate([`/poker/home/${val.id}/${val.me.id}`]);
      });
  }

  initializeForm() {
    this.firstName = localStorage.getItem('firstName');
    const lastName = localStorage.getItem('lastName');
    this.joinForm = this.fb.group({
      firstName: [this.firstName, [Validators.required]],
      lastName: [lastName],
      sessionCode: [
        '',
        [Validators.required, Validators.minLength(8), Validators.maxLength(8)]
      ]
    });
    this.createForm = this.fb.group({
      firstName: [this.firstName, [Validators.required]],
      lastName: [lastName],
      sessionName: ['', [Validators.required]],
      controlType: ['shared'],
      startType: ['automatically']
    });
  }

  submitJoin() {
    const joinSessionDto = new PokerSessionJoinRequest(this.joinForm.value);
    localStorage.setItem('firstName', joinSessionDto.firstName);
    localStorage.setItem('lastName', joinSessionDto.lastName);
    this.store.dispatch(new JoinSession(joinSessionDto));
    this.router.navigate(['/poker/home']);
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
