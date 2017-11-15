export enum ToDoStatus {
  Open,
  Done
}

export class ToDo {
  constructor(
    public id: number,
    public title: string,
    public status: ToDoStatus
  ) {}
}
