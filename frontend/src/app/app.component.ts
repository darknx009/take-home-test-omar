import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { DataService } from './app.service'; 

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [CommonModule, MatTableModule, MatButtonModule],
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})
export class AppComponent implements OnInit{
  title = 'my-angular-app';
  apiData: any;
  errorMessage: string | null = null;
  credentials = { username: 'admin', password: 'password' };

  constructor(private dataService: DataService) { } // Inject the service

  ngOnInit(): void {
    console.log('AppComponent initialized. Calling API...');

    this.dataService.getAuthorize(this.credentials).subscribe(() => {
      this.dataService.getLoans().subscribe({
        next: (data: any) => {
          this.apiData = data;
          console.log('API Data received:', this.apiData);
        },
        error: (error: any) => {
          this.errorMessage = 'Failed to load data. Please try again later.';
          console.error('Error fetching data:', error);
        },
        complete: () => {
          console.log('API call completed.');
        }
      });
    });

  }
  displayedColumns: string[] = [
    'loanAmount',
    'currentBalance',
    'customerName',
    'status',
  ];
}