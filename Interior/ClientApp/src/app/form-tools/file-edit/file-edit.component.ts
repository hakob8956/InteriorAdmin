import { FileType } from './../../models/Enums';
import { Component, OnInit, ViewChild, ElementRef, Input,Output,EventEmitter, AfterContentInit, AfterViewInit } from '@angular/core';
import { faSearch } from "@fortawesome/free-solid-svg-icons";

@Component({
  selector: 'app-file-edit',
  templateUrl: './file-edit.component.html',
  styleUrls: ['./file-edit.component.scss']
})
export class FileEditComponent {


  @Input() fileName:string="";
  @Input() fileType:FileType;
  @Output() changeFile = new EventEmitter();


  fileToUpload: File = null;
  faSearch = faSearch;


  onFileChange(files: FileList) {
    this.fileName = Array.from(files)
      .map(f => f.name)
      .join(", ");
    this.fileToUpload = files.item(0);
    this.changeFile.emit({file:this.fileToUpload,fileType:this.fileType});
  }




}
