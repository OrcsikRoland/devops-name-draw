import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { NameItem } from '../models/name-item';
import { ConfigService } from '../config.service';

@Injectable({
  providedIn: 'root'
})
export class NameApiService {

    constructor(private http: HttpClient, private configService: ConfigService) {}

      /** GET /api/Names */
  getAll(): Observable<NameItem[]> {
    return this.http.get<NameItem[]>(
      `${this.configService.cfg.backendUrl}/api/Names`
    );
  }

  /** POST /api/Names */
  add(value: string): Observable<void> {
    return this.http.post<void>(
      `${this.configService.cfg.backendUrl}/api/Names`,
      { value }
    );
  }

  /** DELETE /api/Names/{id} */
  remove(id: number): Observable<void> {
    return this.http.delete<void>(
      `${this.configService.cfg.backendUrl}/api/Names/${id}`
    );
  }

  /** GET /api/Names/random */
  random(): Observable<NameItem> {
    return this.http.get<NameItem>(
      `${this.configService.cfg.backendUrl}/api/Names/random`
    );
  }
}
