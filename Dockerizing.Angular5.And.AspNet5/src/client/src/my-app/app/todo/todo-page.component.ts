import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs/Observable';

import { ToDo, ToDoStatus } from './todo.model';
import { TodoApiService } from './todo-api.service';

@Component({
  selector: 'todo-page',
  template: `
    <h1>ToDo List</h1>
    <header>
      <input #title />
      <button (click)="onAdd(title.value)">Add</button>
    </header>
    <h4>Tasks</h4>
    <todo-list [items]="filter(true)" (remove)="onRemove($event)" (check)="onChange($event)"></todo-list>
    <h5>Complated tasks</h5>
    <todo-list [items]="filter(false)" (remove)="onRemove($event)" (check)="onChange($event)"></todo-list>
  `,
  styles: [`
    :host {
      display: flex;
      padding: 10px 200px;
      flex-direction: column;
    }
  `]
})
export class ToDoPageComponent implements OnInit {
  public todos: ToDo[] = [];

  constructor(private service: TodoApiService) {}

  public ngOnInit() {
    this.service
      .get()
      .subscribe(items => this.todos = items);
  }

  public onAdd(title: string) {
    this.service
      .add(title)
      .subscribe(newItem => this.todos.push(newItem));
  }

  public onRemove(todo: ToDo) {
    this.service
      .remove(todo.id)
      .subscribe(nothing => {
        const indexOf = this.todos.indexOf(todo);
        this.todos.splice(indexOf, 1);
      });
  }

  public onChange(todo: ToDo) {
    const updatedStatus = todo.status === ToDoStatus.Open ? ToDoStatus.Done : ToDoStatus.Open;
    this.service
      .update(todo.id, updatedStatus)
      .subscribe(updatedToDo => {
        const indexOf = this.todos.indexOf(todo);
        this.todos[indexOf] = updatedToDo;
      });
  }

  public filter = (open: boolean) => open ?
    this.todos.filter(it => it.status === ToDoStatus.Open) :
    this.todos.filter(it => it.status === ToDoStatus.Done)
}
