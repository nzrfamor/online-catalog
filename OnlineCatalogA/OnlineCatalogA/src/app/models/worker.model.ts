import { Data } from "@angular/router";

export interface Worker{
    id: number,
    name: string,
    surname: string,
    position: string,
    hireDate: string,
    salary: number,
    hierarchyLevel: number,
    leaderId?: number,
    workerAsLeaderId?: number,
    workerAsSubordinateId?: number,
    subordinatesIds: number[],
    subordinates: Worker[]
}