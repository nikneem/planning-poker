import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { JoinComponent } from './join/join.component';
import { CreateComponent } from './create/create.component';
import { PbiComponent } from './pbi/pbi.component';
import { RouterModule } from '@angular/router';
import { ScrollbarModule } from 'ngx-scrollbar';
import { ReactiveFormsModule } from '@angular/forms';
import { SharedcomponentsModule } from '../sharedcomponents/sharedcomponents.module';

@NgModule({
  imports: [
    CommonModule,
    RouterModule,
    ScrollbarModule,
    ReactiveFormsModule,
    SharedcomponentsModule
  ],
  declarations: [JoinComponent, CreateComponent, PbiComponent]
})
export class RefinementModule {}
