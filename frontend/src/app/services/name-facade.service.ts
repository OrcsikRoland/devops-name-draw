import { Injectable } from '@angular/core';
import { NameService } from './name.service';
import { NameApiService } from './name-api.service';
import { Observable, of, throwError } from 'rxjs';
import { NameItem } from '../models/name-item';
import { environment } from '../../environments/environment.development';

@Injectable({
  providedIn: 'root'
})
export class NameFacadeService {
constructor(
    private mock: NameService,
    private api: NameApiService
  ) {}

  getAll(): Observable<NameItem[]> {
    return environment.useMock ? of(this.mock.getAll()) : this.api.getAll();
  }

  add(value: string): Observable<void> {
    if (environment.useMock) {
      try {
        this.mock.add(value);
        return of(void 0);
      } catch (e: any) {
        return throwError(() => new Error(e?.message ?? 'Hiba'));
      }
    }
    return this.api.add(value);
  }

  remove(id: number): Observable<void> {
    if (environment.useMock) {
      this.mock.remove(id);
      return of(void 0);
    }
    return this.api.remove(id);
  }

  random(): Observable<NameItem | null> {
    if (environment.useMock) {
      return of(this.mock.random());
    }
    return this.api.random();
  }

  clear(): Observable<void> {
    if (environment.useMock) {
      this.mock.clear();
      return of(void 0);
    }
    // API-nál később lehet külön endpoint, most nem kell
    return of(void 0);
  }
}
