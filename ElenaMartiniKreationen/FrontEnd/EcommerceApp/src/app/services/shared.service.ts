// shared.service.ts
import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SharedService {
  private filterSubject = new BehaviorSubject<string>(''); 
  currentFilter$ = this.filterSubject.asObservable();

  updateFilter(filter: string) {
    this.filterSubject.next(filter);
  }
}
