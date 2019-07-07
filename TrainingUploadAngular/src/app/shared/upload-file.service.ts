import { Injectable } from '@angular/core';
import { HttpClient, HttpRequest, HttpEventType } from "@angular/common/http";
import { UploadFile } from './upload-file.model';

@Injectable({
  providedIn: 'root'
})
export class UploadFileService {
  readonly rootURL = "http://localhost:53099/api";
  filesUploading = [];
  filesUploaded = [];

  constructor(private http:HttpClient) { }

  postUploadFile(formData) {
    const req = new HttpRequest('POST', this.rootURL + '/UploadedFiles', formData, {
      reportProgress: true
    });
    
    return this.http.request(req);
  }

   uploadFile(uploadFile: UploadFile)
  {
      this.filesUploading.push(uploadFile);

      this.postUploadFile(uploadFile.FormData).subscribe(
      async event => {
        if (event.type === HttpEventType.UploadProgress) {
          uploadFile.Progress = Math.round((event.loaded / event.total) * 100);

          if(uploadFile.Progress == 100)
          {
            await new Promise( resolve => setTimeout(resolve, 1000) );
            this.deleteMsgFromUploadingList(uploadFile);
          }
            
        }
        else if (event.type === HttpEventType.Response) {
          var file = event.body as UploadFile;

          if (file != null)
          {
            uploadFile.Id = file.Id;
            this.filesUploaded.push(uploadFile);
          }
        }
      },
      err => {
        console.log(err);
      })
  }

  refreshList(){
     this.http.get(this.rootURL + '/UploadedFiles')
    .toPromise()
    .then(res => this.filesUploaded = res as UploadFile[])
  }

  deleteMsgFromUploadingList(uploadedFile: UploadFile) {
    const index: number = this.filesUploading.indexOf(uploadedFile);
    if (index !== -1) {
      this.filesUploading.splice(index, 1);
    }        
  }

  deleteFile(id) {
    
    return this.http.delete(this.rootURL + '/UploadedFiles/' + id)

  }

  checkIfDuplicate(file)
  {
      var retVal = false;
      for(var i = 0; i < this.filesUploading.length; i++)
      {
        if ((this.filesUploading[i] as UploadFile).Name == file.name)
        {
          retVal = true;
          break;
        }
      }


      for(var i = 0; i < this.filesUploaded.length; i++)
      {
        if ((this.filesUploaded[i] as UploadFile).Name == file.name)
        {
          retVal = true;
          break;
        }
      }
      return retVal;
  }
}
