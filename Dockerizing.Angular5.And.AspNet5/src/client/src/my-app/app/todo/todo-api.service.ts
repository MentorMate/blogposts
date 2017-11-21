import { retry } from 'rxjs/operator/retry';
import { Inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { Observable } from 'rxjs/Observable';

import 'rxjs/add/operator/map';
import 'rxjs/add/operator/share';

import { API_URL } from '../shared/tokens/api.url.token';
import { ToDo, ToDoStatus } from './todo.model';

@Injectable()
export class TodoApiService {
  private url: string;

  constructor(
    @Inject(API_URL)
    private baseUrl: string,
    private http: HttpClient
  ) {
    this.url = baseUrl + 'api/todos';
  }

  public get(): Observable<ToDo[]> {
    return this.http
      .get(this.url)
      .map((res: any) => res as ToDo[])
      .share();
  }

  public remove(id: number): Observable<any> {
    return this.http
      .delete(this.url + '/' + id.toString());
  }

  public add(title: string): Observable<ToDo> {
    return this.http
      .post(this.url, { title, status: ToDoStatus.Open })
      .map((res: any) => res as ToDo);
  }

  public update(id: number, status: ToDoStatus): Observable<ToDo> {
    return this.http
      .put(this.url + '/' + id.toString(), { status })
      .map((res: any) => res as ToDo);
  }
}
