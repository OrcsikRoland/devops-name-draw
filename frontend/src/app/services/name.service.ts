import { Injectable } from '@angular/core';
import { NameItem } from '../models/name-item';

@Injectable({
  providedIn: 'root'
})
export class NameService {

  private names: NameItem[] = [
    { id: 1, value: 'Anna', createdAt: new Date().toISOString() },
    { id: 2, value: 'Bence', createdAt: new Date().toISOString() },
    { id: 3, value: 'Zoé', createdAt: new Date().toISOString() },
  ];

  private nextId = 4;

  getAll(): NameItem[] {
    return [...this.names].sort((a, b) => b.id - a.id);
  }

  add(value: string): void {
    const v = value.trim();
    if (!v) throw new Error('A név nem lehet üres.');
    if (v.length > 50) throw new Error('Max 50 karakter lehet.');

    const item: NameItem = {
      id: this.nextId++,
      value: v,
      createdAt: new Date().toISOString()
    };

    this.names = [item, ...this.names];
  }

  remove(id: number): void {
    this.names = this.names.filter(x => x.id !== id);
  }

  random(): NameItem | null {
    if (this.names.length === 0) return null;
    const idx = Math.floor(Math.random() * this.names.length);
    return this.names[idx];
  }

  clear(): void {
    this.names = [];
  }
}
