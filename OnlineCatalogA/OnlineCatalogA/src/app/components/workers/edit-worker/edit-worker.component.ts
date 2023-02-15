import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Subordinate } from 'src/app/models/subordinate.model';
import { Worker } from 'src/app/models/worker.model';
import { SubordinateService } from 'src/app/services/subordinate.service';
import { WorkersService } from 'src/app/services/workers.service';

@Component({
  selector: 'app-edit-worker',
  templateUrl: './edit-worker.component.html',
  styleUrls: ['./edit-worker.component.css']
})
export class EditWorkerComponent {

  editWorkerRequest: Worker = {
    id: 0,
    name: "",
    surname: "",
    position: "",
    hireDate: "",
    salary: 0,
    hierarchyLevel: 0,
    subordinatesIds: [],
    subordinates: []
  };

  editWorkerAsSubordinateRequest: Subordinate = {
    id: 0,
    workerId: 0,
    leaderId: 0
  };

  workers: Worker[] = []
  
  constructor(private workersService: WorkersService, private subordinateService: SubordinateService, 
    private route: ActivatedRoute, private router: Router) { 

  }

  
  ngOnInit(): void {
    this.route.paramMap.subscribe({
      next: (params) => {
        const id = Number(params.get('id'));

        if(id){
          this.workersService.getWorkerById(id)
          .subscribe({
            next: (worker) => {
              this.editWorkerRequest = worker;
              this.editWorkerRequest.hireDate = worker.hireDate.slice(0,10);

              if(worker.workerAsSubordinateId){
                this.workersService.getWorkersAsPossibleLeadersBySubordinatesId(worker.workerAsSubordinateId)
                .subscribe({
                  next: (workers) => {
                  this.workers = workers;
                  console.log(workers);
                },
                error: (response) => {
                  console.log(response);
                }
              })
              this.editWorkerAsSubordinateRequest.workerId = this.editWorkerRequest.id;
              this.editWorkerAsSubordinateRequest.id = this.editWorkerRequest.workerAsSubordinateId as number;
             }

              console.log(this.editWorkerRequest);
            },
            error: (response) => {
              console.log(response);
            }
          })
          
        }

      }
    })
  }

  updateAllData(){
    this.workersService.updateWorker(this.editWorkerRequest.id, this.editWorkerRequest)
    .subscribe({
      next: (worker)=>{
        this.router.navigate(['workers']);
      }
    })
    if(this.editWorkerRequest.leaderId){
      this.subordinateService.updateSubordinate(this.editWorkerAsSubordinateRequest.id, this.editWorkerAsSubordinateRequest)
      .subscribe({
        next: (subordinate)=>{;
        }
      })
    }
  }
}
