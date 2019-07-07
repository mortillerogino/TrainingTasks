import { Component, OnInit } from '@angular/core';
import { UploadFileService } from 'src/app/shared/upload-file.service';
import { ToastrService } from 'ngx-toastr';
import { UploadFile } from 'src/app/shared/upload-file.model';

@Component({
  selector: 'app-uploaded-file-list',
  templateUrl: './uploaded-file-list.component.html',
  styles: []
})
export class UploadedFileListComponent implements OnInit {

  constructor(private service: UploadFileService,
    private toastr:ToastrService) { }

  ngOnInit() {
    this.service.refreshList();
  }

  onDelete(id) {
    if (confirm("Are you sure you want to delete this file?")) {
      this.service.deleteFile(id)
      .subscribe(res => {
        this.service.refreshList();
        var data = res as UploadFile;
        
        console.log(data);
        var index = this.service.filesUploaded.indexOf(data)
        this.service.filesUploaded.splice(index, 1);


        this.toastr.info('Successfully deleted file ' + data.Name, 'File Drag Uploader');
      });
    }
    
  }

}
