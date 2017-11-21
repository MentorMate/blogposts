import { retry } from 'rxjs/operators';
import { Component, Input, EventEmitter, Output } from '@angular/core';
import { ToDo, ToDoStatus } from '../todo.model';

@Component({
    selector: 'todo-item',
    template: `
      <checkbox [checked]="isDone" (change)="onChange($event)"></checkbox>
      <span [class.done]="isDone">{{todo.title}}</span>
      <b [hidden]="isDone" (click)="onRemove()">X</b>`,
    styles: [`
      :host {
        width: 300px;
        display: flex;
      }

      :host > * { margin:5px; height: 20px; }

      span { flex: 1 1 auto; }
      span.done { text-decoration: line-through; }

      b {
        cursor: pointer;
        padding: 0 3px;
      }
    `]
  })
export class ToDoItemComponent {
  @Input() public todo: ToDo;
  @Output() public remove: EventEmitter<ToDo> = new EventEmitter<ToDo>();
  @Output() public check: EventEmitter<ToDo> = new EventEmitter<ToDo>();

  get isDone() { return this.todo.status === ToDoStatus.Done; }

  public onChange(checked) {
    this.check.emit(this.todo);
  }

  public onRemove() {
    this.remove.emit(this.todo);
  }
}
