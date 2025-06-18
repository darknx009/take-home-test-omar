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

  constructor(private dataService: DataService) { } // Inject the service

  ngOnInit(): void {
    console.log('AppComponent initialized. Calling API...');
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
  }
  displayedColumns: string[] = [
    'loanAmount',
    'currentBalance',
    'customerName',
    'status',
  ];
}
 
  
  /*
  loans = this.loanList; */
  // loans = [
  //   {
  //     loanAmount: 25000.00,
  //     currentBalance: 18750.00,
  //     applicant: 'John Doe',
  //     status: 'active',
  //   },
  //   {
  //     loanAmount: 15000.00,
  //     currentBalance: 0,
  //     applicant: 'Jane Smith',
  //     status: 'paid',
  //   },
  //   {
  //     loanAmount: 50000.00,
  //     currentBalance: 32500.00,
  //     applicant: 'Robert Johnson',
  //     status: 'active',
  //   },
  // ];
  
//}
