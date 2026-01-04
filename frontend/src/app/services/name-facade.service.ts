import { Injectable } from '@angular/core';

import { Observable, of, throwError } from 'rxjs';
import { NameItem } from '../models/name-item';
import { NameApiService } from './name-api.service';

@Injectable({
  providedIn: 'root'
})
export class NameFacadeService {
  constructor(private api: NameApiService) {}

  getAll(): Observable<NameItem[]> {
    return this.api.getAll();
  }

  add(value: string): Observable<void> {
    return this.api.add(value);
  }

  remove(id: number): Observable<void> {
    return this.api.remove(id);
  }

  random(): Observable<NameItem> {
    return this.api.random();
  }


}
