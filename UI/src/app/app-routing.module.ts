import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { EstimateComponent } from './Modules/estimate/component/estimate.component';
import { LoginComponent } from './Modules/login/component/login.component';


const routes: Routes = [
  {
    path: 'login', component: LoginComponent
  },
  {
    path: 'estimate', component: EstimateComponent
  },
  {
    path: '', redirectTo: 'login', pathMatch: 'full'
  }];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
