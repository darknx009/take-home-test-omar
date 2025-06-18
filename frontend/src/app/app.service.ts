// src/app/data.service.ts
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class DataService {
  private apiUrl = 'http://localhost:5050/loans'; 

  constructor(private http: HttpClient) { }

  getLoans(): Observable<any> {
    return this.http.get(this.apiUrl);
  }

  getSpecificData(id: number): Observable<any> {
    return this.http.get(`${this.apiUrl}`);
  }
}