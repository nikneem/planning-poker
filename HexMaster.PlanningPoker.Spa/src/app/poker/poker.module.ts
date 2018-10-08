import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HomeComponent } from './home/home.component';
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
  declarations: [HomeComponent]
})
export class PokerModule {}
