import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { WorkersListComponent} from './components/workers/workers-list/workers-list.component'
import { WorkersTreeComponent} from './components/workers/workers-tree/workers-tree.component'
import { EditWorkerComponent} from './components/workers/edit-worker/edit-worker.component'

const routes: Routes = [
  {
    path: '',
    component: WorkersListComponent
  },
  {
    path: 'workers',
    component: WorkersListComponent
  },
  {
    path: 'workers-tree',
    component: WorkersTreeComponent
  },
  {
    path: 'workers/edit/:id',
    component: EditWorkerComponent
  }
];


@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
