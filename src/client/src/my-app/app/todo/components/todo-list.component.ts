import { Component, Input, Output, EventEmitter } from '@angular/core';
import { ToDo, ToDoStatus } from '../todo.model';

@Component({
    selector: 'todo-list',
    template: `
      <todo-item *ngFor="let item of items" [todo]="item" (remove)="remove.emit($event)" (check)="check.emit($event)"></todo-item>
    `,
    styles: [`
      :host {
        display: flex;
        flex-direction: column;
        padding: 20px 0;
      }
    `]
  })
export class ToDoListComponent {
  @Input() public items: ToDo[];
  @Output() public remove: EventEmitter<ToDo> = new EventEmitter<ToDo>();
  @Output() public check: EventEmitter<ToDo> = new EventEmitter<ToDo>();
}
