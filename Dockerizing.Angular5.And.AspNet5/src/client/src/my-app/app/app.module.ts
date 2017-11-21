import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { CommonModule } from '@angular/common';

import { AppRoutingModule } from './app-routing.module';
import { environment } from '../environments/environment';
import { API_URL } from './shared/tokens/api.url.token';

import { AppComponent } from './shared/containers/app.component';
import { NotFoundComponent } from './shared/containers/not-found.component';

@NgModule({
  declarations: [
    AppComponent,
    NotFoundComponent
  ],
  imports: [
    HttpClientModule,
    CommonModule,
    BrowserModule,

    AppRoutingModule,
  ],
  providers: [{ provide: API_URL, useValue: environment.apiPath }],
  bootstrap: [AppComponent]
})
export class AppModule { }
