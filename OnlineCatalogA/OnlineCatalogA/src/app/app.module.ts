import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import {MatSortModule} from '@angular/material/sort';


import {MatTableModule} from '@angular/material/table';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { WorkersListComponent } from './components/workers/workers-list/workers-list.component';
import { WorkersTreeComponent } from './components/workers/workers-tree/workers-tree.component';
import { HttpClientModule } from '@angular/common/http';
import { TableBuilderComponent } from './components/table-builder/table-builder.component';
import { EditWorkerComponent } from './components/workers/edit-worker/edit-worker.component';
import { FormsModule } from '@angular/forms';


@NgModule({
  declarations: [
    AppComponent,
    WorkersListComponent,
    WorkersTreeComponent,
    TableBuilderComponent,
    EditWorkerComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    MatSortModule,
    MatTableModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
