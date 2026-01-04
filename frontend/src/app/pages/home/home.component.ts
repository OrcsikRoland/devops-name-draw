import { Component } from '@angular/core';
import { NameItem } from '../../models/name-item';

import { NameFacadeService } from '../../services/name-facade.service';

@Component({
  selector: 'app-home',
  standalone: false,
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss'
})
export class HomeComponent {
  names: NameItem[] = [];
  newName = '';
  picked: NameItem | null = null;
  error: string | null = null;

  constructor(private facade: NameFacadeService) {}

  ngOnInit(): void {
    this.load();
  }

      load(): void {
    this.facade.getAll().subscribe({
      next: (res) => {
        this.names = res;
        this.error = null;
      },
      error: (e) => {
        console.log('LOAD ERROR', e);
        this.error = e?.error?.message ?? 'Nem sikerült betölteni a neveket.';
      }
    });
  }
   add(): void {
    this.error = null;

    const value = this.newName.trim();
    if (!value) {
      this.error = 'Adj meg egy nevet.';
      return;
    }

    this.facade.add(value).subscribe({
      next: () => {
        this.newName = '';
        this.load();
      },
      error: (e) => {
        console.log('ADD ERROR', e); // ideiglenes debug
        this.error =
          e?.error?.message ??
          e?.message ??
          'Hiba történt.';
      }
    });
  }
  remove(id: number): void {
    this.facade.remove(id).subscribe(() => this.load());
  }
  draw(): void {
    this.facade.random().subscribe(x => this.picked = x);
  }
}
