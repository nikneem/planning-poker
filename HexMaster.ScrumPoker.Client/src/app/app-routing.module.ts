import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LayoutComponent } from './user-interface/layout/layout.component';
import { HomeComponent } from './poker/home/home.component';
import { LandingComponent } from './public/landing/landing.component';
import { CreateComponent } from './poker/create/create.component';
import { JoinComponent } from './poker/join/join.component';
import { PbiComponent } from './poker/pbi/pbi.component';

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
      },
      {
        path: 'home',
        component: HomeComponent
      }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {}
