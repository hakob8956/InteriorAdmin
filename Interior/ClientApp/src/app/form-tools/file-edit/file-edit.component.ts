import { FileModel } from 'src/app/models/File';
import { Component, OnInit, ViewChild, ElementRef, Input,Output,EventEmitter } from '@angular/core';

@Component({
  selector: 'app-file-edit',
  templateUrl: './file-edit.component.html',
  styleUrls: ['./file-edit.component.scss']
})
export class FileEditComponent implements OnInit {

  @Input() fileName:string="";
  @Output() changeFile = new EventEmitter<File>();
  @ViewChild("labelImport")
  labelImport: ElementRef;
  fileToUpload: File = null;

  ngOnInit() {

  }
  onFileChange(files: FileList) {
    this.labelImport.nativeElement.innerText = Array.from(files)
      .map(f => f.name)
      .join(", ");
    this.fileToUpload = files.item(0);
    this.changeFile.emit(this.fileToUpload);
  }

}
