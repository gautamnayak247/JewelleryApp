import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { CoreModule } from './core/core.module';
import { LoginModule } from './Modules/login/login.module';
import { EstimateModule } from './Modules/estimate/estimate.module';

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    LoginModule,
    CoreModule,
    EstimateModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
