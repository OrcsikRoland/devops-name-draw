import { Component } from '@angular/core';
import { NameItem } from '../../models/name-item';
import { NameService } from '../../services/name.service';
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
      next: (res) => this.names = res,
      error: () => this.error = 'Nem sikerült betölteni a neveket.'
    });
  }

  add(): void {
    this.error = null;
    this.facade.add(this.newName).subscribe({
      next: () => {
        this.newName = '';
        this.load();
      },
      error: (e) => this.error = e?.message ?? 'Hiba történt.'
    });
  }

  remove(id: number): void {
    this.facade.remove(id).subscribe(() => this.load());
  }

  draw(): void {
    this.facade.random().subscribe(x => this.picked = x);
  }

  clear(): void {
    this.facade.clear().subscribe(() => {
      this.picked = null;
      this.load();
    });
  }
}
