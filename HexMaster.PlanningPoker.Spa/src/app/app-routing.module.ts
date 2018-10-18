import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LayoutComponent } from './user-interface/layout/layout.component';
import { HomeComponent } from './poker/home/home.component';
import { LandingComponent } from './public/landing/landing.component';
import { CreateComponent } from './refinement/create/create.component';
import { JoinComponent } from './refinement/join/join.component';
import { PbiComponent } from './refinement/pbi/pbi.component';

const routes: Routes = [
  {
    path: '',
    component: LandingComponent,
    pathMatch: 'full'
  },
  {
    path: 'poker',
    component: LayoutComponent,
    children: [
      {
        path: 'home/:sessionId/:participantId',
        component: HomeComponent
      }
    ]
  },
  {
    path: 'refinement',
    component: LayoutComponent,
    children: [
      {
        path: 'create',
        component: CreateComponent
      },
      {
        path: 'join',
        component: JoinComponent
      },
      {
        path: 'pbi/:id',
        component: PbiComponent
      }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {}
