import { Injectable } from '@angular/core';
import { Config } from './models/config';
import { HttpClient } from '@angular/common/http';
import { firstValueFrom, tap } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ConfigService {

  public cfg: Config = new Config()

  constructor(private http: HttpClient) { }

  load(): Promise<void> {
    return firstValueFrom(
      this.http.get<Config>('config.json', { headers: { 'Cache-Control': 'no-cache' } })
        .pipe(tap(t => {
          console.log('CONFIG LOADED:', t);
          this.cfg = t;
        }))
    ).then(() => {});
  }
}
