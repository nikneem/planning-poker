import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';

import { UserInterfaceModule } from './user-interface/user-interface.module';
import { PokerModule } from './poker/poker.module';
import { PublicModule } from './public/public.module';
import { AuthService } from './services/auth.service';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { AuthInterceptor } from './shared/interceptors/auth.interceptor';
import { environment } from '../environments/environment';

import { StoreModule } from '@ngrx/store';
import { storeFreeze } from 'ngrx-store-freeze';
import { EffectsModule } from '@ngrx/effects';
import { StoreDevtoolsModule } from '@ngrx/store-devtools';
import { INITIAL_APPSTORE, reducers } from './state/app.state';
import { UserEffects } from './state/user/user.effects';
import { RefinementEffects } from './state/refinement/refinement.effects';
import { SharedcomponentsModule } from './sharedcomponents/sharedcomponents.module';
import { RefinementModule } from './refinement/refinement.module';

let metaReducers = [];
if (environment.production === false) {
  metaReducers = [storeFreeze];
}

@NgModule({
  declarations: [AppComponent],
  imports: [
    BrowserModule,
    HttpClientModule,
    ReactiveFormsModule,
    AppRoutingModule,
    StoreModule.forRoot(reducers, {
      metaReducers: metaReducers,
      initialState: INITIAL_APPSTORE
    }),
    StoreDevtoolsModule.instrument({ maxAge: 5 }),
    EffectsModule.forRoot([UserEffects, RefinementEffects]),
    UserInterfaceModule,
    PokerModule,
    PublicModule,
    SharedcomponentsModule,
    RefinementModule
  ],
  providers: [
    AuthService,
    { provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true }
  ],
  bootstrap: [AppComponent]
})
export class AppModule {}
