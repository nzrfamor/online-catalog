import { Component, OnInit } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { empty } from 'rxjs';
import { Worker } from 'src/app/models/worker.model';
import { WorkersService } from 'src/app/services/workers.service';

@Component({
  selector: 'app-workers-list',
  templateUrl: './workers-list.component.html',
  styleUrls: ['./workers-list.component.css']
})
export class WorkersListComponent implements OnInit {

  workers: Worker[] = [];

  constructor(private workersService: WorkersService) { }

  ngOnInit(): void {
    this.workersService.getAllWorkers()
    .subscribe({
      next: (workers) => {
        this.workers = workers;
        console.log(this.workers);
      },
      error: (response) => {
        console.log(response);
      }
    })
  }
}
