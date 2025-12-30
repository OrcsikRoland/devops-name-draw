import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment.development';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { NameItem } from '../models/name-item';

@Injectable({
  providedIn: 'root'
})
export class NameApiService {

  private baseUrl = `${environment.apiBaseUrl}/names`;

    constructor(private http: HttpClient) {}

    getAll(): Observable<NameItem[]> {
      return this.http.get<NameItem[]>(this.baseUrl);
    }

    add(value: string): Observable<void> {
      return this.http.post<void>(this.baseUrl, { value });
    }

    remove(id: number): Observable<void> {
      return this.http.delete<void>(`${this.baseUrl}/${id}`);
    }

    random(): Observable<NameItem> {
      return this.http.get<NameItem>(`${this.baseUrl}/random`);
  }
}
