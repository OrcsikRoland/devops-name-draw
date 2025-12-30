import { Component } from '@angular/core';
import { NameItem } from '../../models/name-item';
import { NameService } from '../../services/name.service';

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

  constructor(private nameService: NameService) {}

  ngOnInit(): void {
    this.load();
  }

  load(): void {
    this.names = this.nameService.getAll();
  }

  add(): void {
    this.error = null;
    try {
      this.nameService.add(this.newName);
      this.newName = '';
      this.load();
    } catch (e: any) {
      this.error = e?.message ?? 'Hiba történt.';
    }
  }

  remove(id: number): void {
    this.nameService.remove(id);
    this.load();
  }

  draw(): void {
    this.picked = this.nameService.random();
  }

  clear(): void {
    this.nameService.clear();
    this.picked = null;
    this.load();
  }
}
