import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';
import { Worker } from 'src/app/models/worker.model';

@Injectable({
  providedIn: 'root'
})
export class WorkersService {

  baseApiUrl: string = environment.baseApiUrl;
  constructor(private http: HttpClient) { }

  getAllWorkers(): Observable<Worker[]>{
    return this.http.get<Worker[]>(this.baseApiUrl + "/api/Workers");
  }

  getAllWorkersByLeaderId(id: number): Observable<Worker[]>{
    return this.http.get<Worker[]>(this.baseApiUrl + "/api/Workers/leader/" + id);
  }

  getWorkerById(id: number): Observable<Worker>{
    return this.http.get<Worker>(this.baseApiUrl + "/api/Workers/" + id);
  }

  getWorkersAsPossibleLeadersBySubordinatesId(id: number): Observable<Worker[]>{
    return this.http.get<Worker[]>(this.baseApiUrl + "/api/Workers/leaders/subordinate/" + id);
  }
  
  updateWorker(id: number, editWorkerRequest: Worker): Observable<Worker> {
    return this.http.put<Worker>(this.baseApiUrl + "/api/Workers/" + id, editWorkerRequest);
  }
}

