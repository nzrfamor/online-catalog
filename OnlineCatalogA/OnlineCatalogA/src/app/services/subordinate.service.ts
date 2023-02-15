import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';
import { Subordinate } from 'src/app/models/subordinate.model';

@Injectable({
  providedIn: 'root'
})
export class SubordinateService {

  baseApiUrl: string = environment.baseApiUrl;
  constructor(private http: HttpClient) { }

  updateSubordinate(id: number, editWorkerAsSubordinateRequest: Subordinate): Observable<Subordinate> {
    return this.http.put<Subordinate>(this.baseApiUrl + "/api/Subordinates/" + id, editWorkerAsSubordinateRequest);
  }
}
