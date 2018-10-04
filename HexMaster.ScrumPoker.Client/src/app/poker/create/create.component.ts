import { Component, OnInit } from '@angular/core';
import {
  FormBuilder,
  FormGroup,
  FormControl,
  FormArray,
  Validators
} from '@angular/forms';
@Component({
  selector: 'app-create',
  templateUrl: './create.component.html',
  styleUrls: ['./create.component.scss']
})
export class CreateComponent implements OnInit {
  public refinementForm: FormGroup;
  invitees: FormArray;
  pbis: FormArray;

  constructor(private fb: FormBuilder) {
    this.ConstructForm();
  }

  ConstructForm() {
    this.refinementForm = this.fb.group({
      name: ['', [Validators.required, Validators.minLength(4)]],
      invitees: this.fb.array([this.CreateInvitee()]),
      pbis: this.fb.array([this.CreatePbi()])
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
    this.pbis = this.refinementForm.get('pbis') as FormArray;
    this.pbis.push(this.CreatePbi());
  }
  RemovePbi(index: number) {
    this.pbis = this.refinementForm.get('pbis') as FormArray;
    this.pbis.removeAt(index);
  }

  SubmitCreate() {
    console.log(this.refinementForm.value);
  }
  get name() {
    return this.refinementForm.get('name');
  }

  ngOnInit() {}
}
