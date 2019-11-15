import { FileModel } from 'src/app/models/File';
import { Component, OnInit, ViewChild, ElementRef, Input,Output,EventEmitter, AfterContentInit, AfterViewInit } from '@angular/core';
import { faSearch } from "@fortawesome/free-solid-svg-icons";

@Component({
  selector: 'app-file-edit',
  templateUrl: './file-edit.component.html',
  styleUrls: ['./file-edit.component.scss']
})
export class FileEditComponent {

  @Input() fileName:string="";
  @Output() changeFile = new EventEmitter<File>();

  fileToUpload: File = null;
  faSearch = faSearch;


  onFileChange(files: FileList) {
    this.fileName = Array.from(files)
      .map(f => f.name)
      .join(", ");
    this.fileToUpload = files.item(0);
    this.changeFile.emit(this.fileToUpload);
  }




}
