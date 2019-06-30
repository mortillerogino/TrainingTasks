import { Component, OnInit } from '@angular/core';
import { UploadFileService } from 'src/app/shared/upload-file.service';

@Component({
  selector: 'app-upload-files-list',
  templateUrl: './upload-files-list.component.html',
  styles: []
})
export class UploadFilesListComponent implements OnInit {

  constructor(private service: UploadFileService) { }

  ngOnInit() {
  }

}
