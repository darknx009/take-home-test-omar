// src/app/data.service.ts
import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { map, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class DataService {
  //private apiUrl = 'https://localhost:5001/loans'; 
  //private apiLoginUrl = 'https://localhost:5001/auth';
  private apiUrl = 'http://localhost:5050/loans'; 
  private apiLoginUrl = 'http://localhost:5050/auth';
  private jwtToken = "";
  
  constructor(private http: HttpClient) { }

  getAuthorize(credentials: { username: string; password: string }): Observable<any> {
    return this.http.post<any>(this.apiLoginUrl + '/login', credentials)
      .pipe(
        map(response => {
          this.jwtToken = response.token;          
        })
      );
  }

  getLoans(): Observable<any> {
    /*const headers = this.jwtToken?.length >= 0
      ? new HttpHeaders({ 'Content-Type': 'application/json', 'Authorization' : `Bearer ${this.jwtToken}` })
      : undefined;
      if (this.jwtToken) {
      const httpHeaders = new HttpHeaders({
        'Content-Type': 'application/json',
        'Authorization' : `Bearer ${this.jwtToken}`
      });
    */
      //const httpHeaders = new HttpHeaders({ 'Content-Type': 'application/json', 'Authorization' : `Bearer ${this.jwtToken}` });
    console.log('jwtToken:', this.jwtToken);
    /*
    const httpHeaders: HttpHeaders = new HttpHeaders({
        'Content-Type': 'application/json',
        'Authorization': `Bearer ${this.jwtToken}`
    });*/
    const httpHeaders= new HttpHeaders()
    .set('content-type', 'application/json')
    .set('Authorization', `Bearer ${this.jwtToken}`);
    /*const httpHeaders = new HttpHeaders({
        'Content-Type': 'application/json',
        'Authorization' : `Bearer ${this.jwtToken}`
    });*/
    console.log('httpHeaders created:', httpHeaders);
    return this.http.get(this.apiUrl, {headers: httpHeaders});
  }

}