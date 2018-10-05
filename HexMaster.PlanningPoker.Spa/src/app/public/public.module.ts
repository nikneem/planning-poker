import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LandingComponent } from './landing/landing.component';
import { RouterModule } from '@angular/router';
import { CallbackComponent } from './callback/callback.component';

@NgModule({
  imports: [CommonModule, RouterModule],
  declarations: [LandingComponent, CallbackComponent]
})
export class PublicModule {}
