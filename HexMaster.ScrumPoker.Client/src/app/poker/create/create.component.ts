import { Component, OnInit } from '@angular/core';
import {
  FormBuilder,
  FormGroup,
  FormControl,
  FormArray,
  Validators
} from '@angular/forms';
import Refinement from '../../models/refinement.dto';
import { Store } from '@ngrx/store';
import { AppState } from '../../state/app.state';
import { CreateRefinement } from '../../state/refinement/refinement.actions';
@Component({
  selector: 'app-create',
  templateUrl: './create.component.html',
  styleUrls: ['./create.component.scss']
})
export class CreateComponent implements OnInit {
  public refinementForm: FormGroup;
  invitees: FormArray;
  productBacklogItems: FormArray;

  public isLoading: boolean;

  constructor(private fb: FormBuilder, private store: Store<AppState>) {
    const self = this;
    this.ConstructForm();
    this.store
      .select((state) => state.refinementState.isLoading)
      .subscribe((value) => (self.isLoading = value));
  }

  ConstructForm() {
    this.refinementForm = this.fb.group({
      name: ['', [Validators.required, Validators.minLength(4)]],
      invitees: this.fb.array([this.CreateInvitee()]),
      productBacklogItems: this.fb.array([this.CreatePbi()])
    });
  }
  CreateInvitee(): FormGroup {
    return this.fb.group({
      email: ['', [Validators.required, Validators.minLength(4)]]
    });
  }
  AddInvitee() {
    this.invitees = this.refinementForm.get('invitees') as FormArray;
    this.invitees.push(this.CreateInvitee());
  }
  RemoveInvitee(index: number) {
    this.invitees = this.refinementForm.get('invitees') as FormArray;
    this.invitees.removeAt(index);
  }

  CreatePbi(): FormGroup {
    return this.fb.group({
      title: ['', [Validators.required, Validators.minLength(4)]],
      description: [''],
      url: ['']
    });
  }
  AddPbi() {
    this.productBacklogItems = this.refinementForm.get(
      'productBacklogItems'
    ) as FormArray;
    this.productBacklogItems.push(this.CreatePbi());
  }
  RemovePbi(index: number) {
    this.productBacklogItems = this.refinementForm.get(
      'productBacklogItems'
    ) as FormArray;
    this.productBacklogItems.removeAt(index);
  }

  SubmitCreate() {
    const refinement = new Refinement(this.refinementForm.value);
    console.log(refinement);
    this.store.dispatch(new CreateRefinement(refinement));
  }
  get name() {
    return this.refinementForm.get('name');
  }

  ngOnInit() {}
}
