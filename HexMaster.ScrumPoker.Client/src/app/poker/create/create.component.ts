import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, FormControl, FormArray } from '@angular/forms';
@Component({
  selector: 'app-create',
  templateUrl: './create.component.html',
  styleUrls: ['./create.component.scss']
})
export class CreateComponent implements OnInit {
  public refinementForm: FormGroup;
  invitees: FormArray;
  constructor(private fb: FormBuilder) {
    this.ConstructForm();
  }

  ConstructForm() {
    this.refinementForm = this.fb.group({
      name: [''],
      invitees: this.fb.array([this.CreateInvitee()]),
      pbis: this.fb.array([])
    });
  }
  CreateInvitee(): FormGroup {
    return this.fb.group({
      email: ['']
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
      title: [''],
      description: [''],
      url: ['']
    });
  }

  SubmitCreate() {
    console.log(this.refinementForm.value);
  }

  ngOnInit() {}
}
