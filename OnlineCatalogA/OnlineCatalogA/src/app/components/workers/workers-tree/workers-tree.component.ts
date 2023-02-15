import { Component, OnInit } from '@angular/core';
import { Worker } from 'src/app/models/worker.model';
import { WorkersService } from 'src/app/services/workers.service';

@Component({
  selector: 'app-workers-tree',
  templateUrl: './workers-tree.component.html',
  styleUrls: ['./workers-tree.component.css']
})

export class WorkersTreeComponent implements OnInit {
 
  firstWorker: Worker = {
    id: 0,
    name: "",
    surname: "",
    position: "",
    hireDate: "",
    salary: 0,
    hierarchyLevel: 0,
    leaderId: 0,
    workerAsLeaderId: 0,
    workerAsSubordinateId: 0,
    subordinatesIds: [],
    subordinates: []
  };

  workers: Worker[] = []

  constructor(private workersService: WorkersService) { }

  ngOnInit(): void {
    this.workersService.getWorkerById(1)
    .subscribe({
      next: (firstWorker) => {
        this.firstWorker = firstWorker;
        console.log(this.firstWorker);
        this.workers.push(this.firstWorker);
      },
      error: (response) => {
        console.log(response);
      }})

    this.workersService.getAllWorkersByLeaderId(1)
    .subscribe({
      next: (workers) => {
        this.firstWorker.subordinates = workers;
        console.log(workers);
      },
      error: (response) => {
        console.log(response);
      }})
  }

  
  getSubordinates(leaderAsWorker: Worker){

    this.workersService.getAllWorkersByLeaderId(leaderAsWorker.workerAsLeaderId as number)
    .subscribe({
      next: (workers) => {
        leaderAsWorker.subordinates = workers;
        console.log(workers);
      },
      error: (response) => {
        console.log(response);
      }})
  }

  getCompileSubordinates(leaderAsWorker: Worker){
    this.workersService.getAllWorkersByLeaderId(leaderAsWorker.leaderId as number)
    .subscribe({
      next: (workers) => {
        leaderAsWorker.subordinates = workers;
        console.log(workers);
      },
      error: (response) => {
        console.log(response);
      }})
    }

}

