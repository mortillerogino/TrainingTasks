import { Component, OnInit } from '@angular/core';
import { UploadFileService } from 'src/app/shared/upload-file.service';

@Component({
  selector: 'app-uploaded-file-list',
  templateUrl: './uploaded-file-list.component.html',
  styles: []
})
export class UploadedFileListComponent implements OnInit {

  constructor(private service: UploadFileService) { }

  ngOnInit() {
    this.service.refreshList();
  }

  onDelete(id) {
    if (confirm("Are you sure you want to delete this file?")) {
      this.service.deleteFile(id)
      .subscribe(res => {
        console.log(res);
        this.service.refreshList()
      });
    }
    
  }

}
