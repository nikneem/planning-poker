import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HomeComponent } from './home/home.component';
import { RouterModule } from '@angular/router';
import { ScrollbarModule } from 'ngx-scrollbar';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { SharedcomponentsModule } from '../sharedcomponents/sharedcomponents.module';
import { PokerSessionService } from '../services/poker.service';
import { EffectsModule } from '@ngrx/effects';
import { PokerEffects } from '../state/poker/poker.effects';

@NgModule({
  imports: [
    CommonModule,
    RouterModule,
    ScrollbarModule,
    ReactiveFormsModule,
    SharedcomponentsModule
  ],
  declarations: [HomeComponent],
  providers: [PokerSessionService]
})
export class PokerModule {}
