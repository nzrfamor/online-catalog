import { Component, Input, NgIterable } from '@angular/core'
import { Worker } from 'src/app/models/worker.model';
import { WorkersService } from 'src/app/services/workers.service';

@Component({
  selector: 'app-table-builder',
  template: `
      <table class="table table-bordered border-dark">
          <thead class="table-dark">
              <tr>
                  <th>Id</th>
                  <th>Name</th>
                  <th>Surname</th>
                  <th>Position</th>
                  <th>Date of Hire</th>
                  <th>Salary</th>
                  <th>Subordinates Count</th>
              </tr>
          </thead>
          <tbody *ngFor="let worker of workers">
              <tr >
                  <td>{{worker.id}}</td>
                  <td>{{worker.name}}</td>
                  <td>{{worker.surname}}</td>
                  <td>{{worker.position}}</td>
                  <td>{{worker.hireDate.substring(0,10)}}</td>
                  <td>{{worker.salary}}</td>
                  <td>{{worker.subordinatesIds.length}}
                      <div *ngIf="worker.subordinatesIds.length > 0 && worker.subordinates == undefined">
                        <button (click)="getCompileSubordinates(worker)" type="button" class="btn btn-primary btn-sm">Show Subordinates</button>
                      </div>
                  </td>
              </tr>
              <tr *ngIf="worker.subordinates && worker.subordinates.length > 0">
                  <td colspan="100%">
                    <app-table-builder [workers]="worker.subordinates"></app-table-builder>
                  </td>
                </tr>
          </tbody>
    </table>
    `
})

export class TableBuilderComponent{
  @Input() workers: Worker[] = [];
  
  
  constructor(private workersService: WorkersService) { }

  getCompileSubordinates(leaderAsWorker: Worker){
    this.workersService.getAllWorkersByLeaderId(leaderAsWorker.workerAsLeaderId as number)
    .subscribe({
      next: (subordinates) => {
        leaderAsWorker.subordinates = subordinates;
        console.log(subordinates);
      },
      error: (response) => {
        console.log(response);
      }})
    }
}
