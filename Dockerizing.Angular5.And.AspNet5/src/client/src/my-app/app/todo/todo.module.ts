import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';

import { ToDoPageComponent } from './todo-page.component';
import { TodoApiService } from './todo-api.service';
import { ToDoListComponent } from './components/todo-list.component';
import { ToDoItemComponent } from './components/todo-item.component';
import { CheckboxComponent } from '../shared/components/checkbox.component';

export const routes: Routes = [{
  path: '',
  component: ToDoPageComponent
}];

@NgModule({
  declarations: [
    CheckboxComponent,
    ToDoPageComponent,
    ToDoListComponent,
    ToDoItemComponent
  ],
  imports: [
    CommonModule,
    RouterModule.forChild(routes)
  ],
  exports: [],
  providers: [TodoApiService],
})
export class ToDoModule { }
