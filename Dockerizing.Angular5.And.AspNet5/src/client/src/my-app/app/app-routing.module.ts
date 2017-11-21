import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';

import { AppComponent } from './shared/containers/app.component';
import { NotFoundComponent } from './shared/containers/not-found.component';

export const appRoutes: Routes = [
  {
    path: '',
    component: AppComponent,
    canActivateChild: [],
    canLoad: [],
    canActivate: [],
    children: [
      {
        path: '',
        redirectTo: 'todo',
        pathMatch: 'full'
      },
      {
        path: 'todo',
        loadChildren: './todo/todo.module#ToDoModule'
      },
      { path: '**', component: NotFoundComponent },
    ]
  }
];

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    RouterModule.forRoot(
      appRoutes,
      // { enableTracing: true } // <-- debugging purposes only
    )
  ],
  exports: [
    RouterModule
  ],
  providers: [],
})
export class AppRoutingModule {}

