import { AdminEditComponent } from './admin-edit/admin-edit.component';
import { AdminViewComponent } from './admin-view/admin-view.component';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { DashboardComponent } from './dashboard/dashboard.component';


const routes: Routes = [
  { path: '', redirectTo: '/adminView', pathMatch: 'full' },
  { path: 'dashboard', component: DashboardComponent },
  { path: 'adminView', component: AdminViewComponent },
  {path:'adminEdit',component:AdminEditComponent},
  {path:'adminEdit/:id',component:AdminEditComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
