import { Component, OnInit } from '@angular/core';
import { UploadFileService } from 'src/app/shared/upload-file.service';
import { HttpEventType } from '@angular/common/http';
import { UploadFile } from 'src/app/shared/upload-file.model';
import { ServiceNameService } from 'src/app/app.module';
import { AngularWaitBarrier } from 'blocking-proxy/built/lib/angular_wait_barrier';
import { ToastrService } from 'ngx-toastr';

@Component({
selector: 'app-upload-files-drag',
  templateUrl: './upload-files-drag.component.html',
  styles: []
})
export class UploadFilesDragComponent implements OnInit {

  i: number;

  constructor(private service:UploadFileService, 
    private toastr:ToastrService) { }

  ngOnInit() {
  }

  onNewFile($event)
  {
    if ($event.target.value === "")
    {
       return;
    }

    for (let file of $event.target.files)
    {
      var dataToUpload = new UploadFile();
      
      dataToUpload.Name = file.name;
      dataToUpload.Progress = 0
      dataToUpload.FormData = new FormData();
      dataToUpload.FormData.append(file.name, file);


      if (file.size/1024/1024 > 2) {
        this.toastr.error('Allowable upload size per file must not be more than 2 MB', 'File Drag Uploader');
        continue;
      }

      var duplicateName = this.service.checkIfDuplicate(file);
      console.log("duplicate is " + duplicateName);

      if (duplicateName)
      {
         this.toastr.error('Duplicate Name for ' + file.name, 'File Drag Uploader');
      }
      else
      {
        this.service.uploadFile(dataToUpload);
      }
     
    }
    
    $event.target.value = "";
    
  }

}
