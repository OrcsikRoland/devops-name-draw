import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment.development';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { NameItem } from '../models/name-item';
import { ConfigService } from '../config.service';

@Injectable({
  providedIn: 'root'
})
export class NameApiService {

    constructor(private http: HttpClient, private configService: ConfigService) {}

    getAll(): Observable<NameItem[]> {
      return this.http.get<NameItem[]>(this.configService.cfg.backendUrl);
    }

    add(value: string): Observable<void> {
      return this.http.post<void>(this.configService.cfg.backendUrl, { value });
    }

    remove(id: number): Observable<void> {
      return this.http.delete<void>(`${this.configService.cfg.backendUrl}/${id}`);
    }

    random(): Observable<NameItem> {
      return this.http.get<NameItem>(`${this.configService.cfg.backendUrl}/random`);
  }
}
