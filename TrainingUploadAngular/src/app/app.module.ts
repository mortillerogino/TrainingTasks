import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { UploadFilesComponent } from './upload-files/upload-files.component';
import { UploadFilesDragComponent } from './upload-files/upload-files-drag/upload-files-drag.component';
import { UploadFilesListComponent } from './upload-files/upload-files-list/upload-files-list.component';
import { UploadFileService } from './shared/upload-file.service';
import { HttpClientModule } from "@angular/common/http";
import { UploadedFileListComponent } from './upload-files/uploaded-file-list/uploaded-file-list.component';
import {NgbModule} from '@ng-bootstrap/ng-bootstrap';
import { CommonModule } from '@angular/common';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ToastrModule } from 'ngx-toastr';

export class ServiceNameService {
  
  constructor() { }
}

@NgModule({
  declarations: [
    AppComponent,
    UploadFilesComponent,
    UploadFilesDragComponent,
    UploadFilesListComponent,
    UploadedFileListComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    NgbModule,
    CommonModule,
    BrowserAnimationsModule, // required animations module
    ToastrModule.forRoot() 
  ],
  providers: [UploadFileService],
  bootstrap: [AppComponent]
})
export class AppModule { }
