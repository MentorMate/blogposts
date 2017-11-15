import { Component, Input, Output, HostBinding, EventEmitter, HostListener } from '@angular/core';

@Component({
    selector: 'checkbox',
    template: ``,
    styles: [`
      :host {
        cursor: pointer;
        width: 20px;
        height: 20px;
        background: #eee;
        border:1px solid #ddd;
      }

      :host.checked:after {
        position: absolute;
        opacity: 0.2;
        content: '';
        width: 9px;
        height: 5px;
        background: transparent;
        border: 3px solid #333;
        border-top: none;
        border-right: none;
        margin: 4px 0 0 4px;
        transform: rotate(-45deg);
      }
    `]
  })
export class CheckboxComponent {
  @Input() public checked = false;
  @Output() public change: EventEmitter<boolean> = new EventEmitter<boolean>();
  @HostBinding('class.checked') get isChecked () { return this.checked; }
  @HostListener('click') public onClick() {
    this.checked = !this.checked;
    this.change.emit(this.checked);
  }
}
