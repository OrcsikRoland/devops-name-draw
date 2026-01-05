import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { NameItem } from '../models/name-item';

import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class NameApiService {

    constructor(private http: HttpClient, ) {}

      /** GET /api/Names */
  getAll(): Observable<NameItem[]> {
    return this.http.get<NameItem[]>(
      `${environment.backendUrl}/api/Names`
    );
  }

  /** POST /api/Names */
  add(value: string): Observable<void> {
    return this.http.post<void>(
      `${environment.backendUrl}/api/Names`,
      { value }
    );
  }

  /** DELETE /api/Names/{id} */
  remove(id: number): Observable<void> {
    return this.http.delete<void>(
      `${environment.backendUrl}/api/Names/${id}`
    );
  }

  /** GET /api/Names/random */
  random(): Observable<NameItem> {
    return this.http.get<NameItem>(
      `${environment.backendUrl}/api/Names/random`
    );
  }
}
