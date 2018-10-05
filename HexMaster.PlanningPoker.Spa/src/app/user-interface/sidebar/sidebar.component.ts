import { Component, OnInit } from '@angular/core';
import { Store } from '@ngrx/store';
import { AppState } from '../../state/app.state';
import Refinement from '../../models/refinement.dto';

@Component({
  selector: 'app-sidebar',
  templateUrl: './sidebar.component.html',
  styleUrls: ['./sidebar.component.scss']
})
export class SidebarComponent implements OnInit {
  public refinement: Refinement;

  constructor(private store: Store<AppState>) {
    const self = this;
    this.store
      .select((state) => state.refinementState.currentRefinement)
      .subscribe((value) => (self.refinement = value));
  }

  ngOnInit() {}
}
